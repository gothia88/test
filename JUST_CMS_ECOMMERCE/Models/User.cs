using System.Linq;
using System.Data.Entity;

namespace Models
{
	/// <summary>
	/// Version: 1.6.4
	/// Update Date: 1394/02/05
	/// Changed In Version: 1.9.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "Users", Schema = Constants.SCHEMA_GENERAL)]
	public class User : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<User>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.EmailAddress)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************

				// **************************************************
				Property(current => current.CellPhoneNumber)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************

				// **************************************************
				Property(current => current.EmailAddressVerificationKey)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************

				// **************************************************
				Property(current => current.CellPhoneNumberVerificationKey)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************
				// *** /Define Index(es) ****************************
				// **************************************************

				HasRequired(current => current.Role)
					.WithMany(role => role.Users)
					.HasForeignKey(current => current.RoleId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.Gender)
					.WithMany(gender => gender.Users)
					.HasForeignKey(current => current.GenderId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.Plan)
					.WithMany(plan => plan.Users)
					.HasForeignKey(current => current.PlanId)
					.WillCascadeOnDelete(false)
					;

				HasMany(current => current.Pages)
					.WithMany(page => page.Users)
					.Map(current =>
					{
						current.ToTable("PagesOfUsers", Constants.SCHEMA_CMS);
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("UserId");
						current.MapRightKey("PageId");
					});

				HasMany(current => current.Groups)
					.WithMany(group => group.Users)
					.Map(current =>
					{
						current.ToTable("UsersOfGroups");
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("UserId");
						current.MapRightKey("GroupId");
					});

				HasMany(current => current.ProjectActions)
					.WithMany(projectAction => projectAction.Users)
					.Map(current =>
					{
						current.ToTable("ProjectActionsOfUsers");
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("UserId");
						current.MapRightKey("ProjectActionId");
					});

				switch (Models.Utility.DatabaseProvider)
				{
					case Enums.DatabaseProviders.Oracle:
					{
						break;
					}

					case Enums.DatabaseProviders.SqlServer:
					{
						break;
					}

					case Enums.DatabaseProviders.SqlServerCompactEdition:
					{
						Property(current => current.Description)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.HomePageUrl)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.HttpReferer)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.AdministratorDescription)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		#region Static Member(s)
		public static string GetCellPhoneNumberVerificationKey()
		{
			string strResult = string.Empty;
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext = new DatabaseContext();

				User oUser = null;

				do
				{
					strResult =
						System.Guid.NewGuid().ToString().Replace("-", string.Empty)
						.ToUpper().Substring(0, 6).Replace("O", "0");

					oUser = oDatabaseContext.Users
						.Where(current =>
							string.Compare(current.CellPhoneNumberVerificationKey,
							strResult, true) == 0)
						.FirstOrDefault();
				}
				while (oUser != null);
			}
			catch
			{
			}
			finally
			{
				if (oDatabaseContext != null)
				{
					oDatabaseContext.Dispose();
					oDatabaseContext = null;
				}
			}

			return (strResult);
		}

		public static User CreateUser
			(string emailAddress, string cellPhoneNumber)
		{
			string strPassword =
				Dtx.Security.Hashing.GetSha1(System.Guid.NewGuid().ToString());

			return (CreateUser(emailAddress, cellPhoneNumber, strPassword));
		}

		public static User CreateUser
			(string emailAddress, string cellPhoneNumber, string password)
		{
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext = new DatabaseContext();

				// **************************************************
				int intCultureLcid =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				ApplicationSettings oApplicationSettings =
					oDatabaseContext.MyApplicationSettings
					.Where(current => current.CultureLcid == intCultureLcid)
					.FirstOrDefault()
					;
				// **************************************************

				User oUser = new User();

				oUser.LastName = "-----";
				oUser.FirstName = "-----";

				oUser.CultureLcid = intCultureLcid;

				oUser.CellPhoneNumber = cellPhoneNumber;

				oUser.CellPhoneNumberVerificationKey =
					GetCellPhoneNumberVerificationKey();

				oUser.Password =
					Dtx.Security.Hashing.GetSha1(password);

				oUser.EmailAddress = emailAddress.ToLower();
				oUser.EmailAddressVerificationKey = System.Guid.NewGuid();

				oUser.RoleId = Role.GetByCultureAndCode(intCultureLcid, Enums.Roles.User).Id;
				oUser.GenderId = Gender.GetByCultureAndCode(intCultureLcid, Enums.Genders.Male).Id;

				if (oApplicationSettings == null)
				{
					oUser.IsActive = false;
				}
				else
				{
					oUser.IsActive =
						oApplicationSettings.ShouldUserBeActivatedAfterRegistration;
				}

				if (Utility.IsEntityValid(oUser) == false)
				{
					return (null);
				}

				return (oUser);
			}
			catch
			{
				return (null);
			}
			finally
			{
			}
		}
		#endregion /Static Member(s)

		public User()
		{
			CellPhoneNumberVerificationKey = "xxxxxx";
			EmailAddressVerificationKey = new System.Guid();

			BirthDate = new ComplexTypes.CustomDateTime();
			AccountExpireDate = new ComplexTypes.CustomDateTime();
			LastLoginDateTime = new ComplexTypes.CustomDateTime();
			StartPlanDateTime = new ComplexTypes.CustomDateTime();
			FinishPlanDateTime = new ComplexTypes.CustomDateTime();
			ResetPasswordDateTime = new ComplexTypes.CustomDateTime();
			AccountTerminateDateTime = new ComplexTypes.CustomDateTime();
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Role)]
		public virtual Role Role { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Role)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid RoleId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Gender)]
		public virtual Gender Gender { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Gender)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid GenderId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Plan),
			Name = Resources.Models.ECommerce.Strings.PlanKeys.EntityName)]
		public virtual ECommerce.Plan Plan { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Plan),
			Name = Resources.Models.ECommerce.Strings.PlanKeys.EntityName)]
		public System.Guid? PlanId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.HomePageUrl)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Url,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Url)]
		public string HomePageUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.EmailAddress)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Email,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForEmail)]
		public string EmailAddress { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsEmailAddressPublic)]
		public bool IsEmailAddressPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsEmailAddressVerified)]
		public bool IsEmailAddressVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.EmailAddressVerificationKey)]
		public System.Guid EmailAddressVerificationKey { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Hits)]
		public int Hits { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.ProfileLevel)]
		public int ProfileLevel { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.LoginCount)]
		public int LoginCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsInBlackList)]
		public bool IsInBlackList { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsAccountExpirable)]
		public bool IsAccountExpirable { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsDirectContactable)]
		public bool IsDirectContactable { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.HasAvatar)]
		public bool HasAvatar { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsAvatarPublic)]
		public bool IsAvatarPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: Constants.FILE_EXTENSION_LENGTH,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string AvatarExtension { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.HasResume)]
		public bool HasResume { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: Constants.FILE_EXTENSION_LENGTH,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string ResumeExtension { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsProfilePublic)]
		public bool IsProfilePublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsAccountTerminated)]
		public bool IsAccountTerminated { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.CellPhoneNumber)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 14,
			MinimumLength = 14,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.StringLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.CellPhoneNumber,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForCellPhoneNumber)]
		public string CellPhoneNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsCellPhoneNumberPublic)]
		public bool IsCellPhoneNumberPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsCellPhoneNumberVerified)]
		public bool IsCellPhoneNumberVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.CellPhoneNumberVerificationKey)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 10,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string CellPhoneNumberVerificationKey { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.FirstName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string FirstName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.LastName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string LastName { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Password)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Password,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string Password { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.ResetPasswordDateTime)]
		public ComplexTypes.CustomDateTime ResetPasswordDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.ResetPasswordVerificationKey)]
		public System.Guid? ResetPasswordVerificationKey { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.BirthDate)]
		public ComplexTypes.CustomDateTime BirthDate { get; set; }
		// **********

		// **********
		public string Age
		{
			get
			{
				if (BirthDate.Value.HasValue == false)
				{
					return ("-----");
				}
				else
				{
					int intAge =
						Utility.Now.Year - BirthDate.Value.Value.Year;

					return (Dtx.Text.Convert.DigitsToUnicode(intAge));
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.IsBirthDatePublic)]
		public bool IsBirthDatePublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.NationalCode)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 10,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.NationalCode,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForNationalCode)]
		public string NationalCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.RegisterIP)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 15,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string RegisterIP { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.AccountExpireDate)]
		public ComplexTypes.CustomDateTime AccountExpireDate { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.RegisterDateTime)]
		public override ComplexTypes.CustomDateTime InsertDateTime
		{
			get
			{
				return (base.InsertDateTime);
			}
			protected internal set
			{
				base.InsertDateTime = value;
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.UpdateProfileDateTime)]
		public override ComplexTypes.CustomDateTime UpdateDateTime
		{
			get
			{
				return (base.UpdateDateTime);
			}
			protected internal set
			{
				base.UpdateDateTime = value;
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.LastLoginDateTime)]
		public ComplexTypes.CustomDateTime LastLoginDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.AccountTerminateDateTime)]
		public ComplexTypes.CustomDateTime AccountTerminateDateTime { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.CompanyName)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string CompanyName { get; set; }
		// **********

		// **********
		public string CompanyNameWithMasterDataCode
		{
			get
			{
				string strResult =
					string.Format("{0} - {1}",
					Dtx.Text.Convert.DisplayFormattedNumber(MasterDataCode), CompanyName);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.JobTitle)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string JobTitle { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.HttpReferer)]
		public string HttpReferer { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.AdministratorDescription)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string AdministratorDescription { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.BankName)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string BankName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.BankCardNumber)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string BankCardNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.BankAccountNumber)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string BankAccountNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.BankShabaNumber)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string BankShabaNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.StartPlanDateTime)]
		public ComplexTypes.CustomDateTime StartPlanDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.FinishPlanDateTime)]
		public ComplexTypes.CustomDateTime FinishPlanDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.FullName)]
		public string FullName
		{
			get
			{
				string strLastName = string.Empty;
				if (string.IsNullOrEmpty(LastName) == false)
				{
					strLastName = LastName.Trim();
				}

				string strFirstName = string.Empty;
				if (string.IsNullOrEmpty(FirstName) == false)
				{
					strFirstName = FirstName.Trim();
				}

				string strFullName =
					string.Format("{0} {1}", strFirstName, strLastName).Trim();

				if (strFullName == string.Empty)
				{
					strFullName = EmailAddress;
				}
				else
				{
					try
					{
						if (Gender != null)
						{
							strFullName =
								string.Format("{0} {1}",
								Gender.Name2, strFullName).Trim();
						}
					}
					catch
					{
					}
				}

				return (strFullName);
			}
		}
		// **********

		// **********
		/// <summary>
		/// Version: 1.6.4
		/// Update Date: 1394/02/05
		/// Changed In Version: 1.9.0
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string FullNameWithJobTitle
		{
			get
			{
				string strResult = FullName;

				if (string.IsNullOrWhiteSpace(JobTitle) == false)
				{
					strResult =
						string.Format("{0} ({1})",
						strResult, JobTitle.Trim());
				}

				return (strResult.Trim());
			}
		}
		// **********

		// **********
		/// <summary>
		/// Version: 1.6.4
		/// Update Date: 1394/02/05
		/// Changed In Version: 1.9.0
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string FullNameWithCompanyName
		{
			get
			{
				string strResult = FullName;

				if (string.IsNullOrWhiteSpace(CompanyName) == false)
				{
					strResult =
						string.Format("{0} ({1})",
						strResult, CompanyName.Trim());
				}

				return (strResult.Trim());
			}
		}
		// **********

		public virtual System.Collections.Generic.IList<Common.Tag> VerifiedTags { get; set; }



		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }

		public virtual System.Collections.Generic.IList<Message> SentMessages { get; set; }

		public virtual System.Collections.Generic.IList<Message> ReceivedMessages { get; set; }

		public virtual System.Collections.Generic.IList<UserAddress> UserAddresses { get; set; }

		public virtual System.Collections.Generic.IList<UserLoginLog> UserLoginLogs { get; set; }

		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }



		public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Page> WrittenPages { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Post> WrittenPosts { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Post> VerifiedPosts { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Page> VerifiedPages { get; set; }

		public virtual System.Collections.Generic.IList<Cms.PostComment> PostComments { get; set; }



		public virtual System.Collections.Generic.IList<ECommerce.Transaction> Transactions { get; set; }

		public virtual System.Collections.Generic.IList<ECommerce.Transaction> CreatorOrModifierTransactions { get; set; }
	}
}
