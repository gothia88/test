using System.Linq;
using System.Data.Entity;

namespace Models
{
	/// <summary>
	/// Version: 1.0.9
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "Genders", Schema = Constants.SCHEMA_GENERAL)]
	public class Gender : BaseLocalizedEntity, IDropDownList
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Gender>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.CultureLcid)
					.HasColumnAnnotation("Index",
						new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation(new[]
						{
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Code", 1) { IsUnique = true },
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Name1", 1) { IsUnique = true },
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Name2", 1) { IsUnique = true },
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Name3", 1) { IsUnique = true },
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Name4", 1) { IsUnique = true },
						}));
				// **************************************************

				// **************************************************
				Property(current => current.Code)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Code", 2) { IsUnique = true }));

				Property(current => current.Name1)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name1", 2) { IsUnique = true }));

				Property(current => current.Name2)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name2", 2) { IsUnique = true }));

				Property(current => current.Name3)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name3", 2) { IsUnique = true }));

				Property(current => current.Name4)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name4", 2) { IsUnique = true }));
				// **************************************************
				// *** /Define Index(es) ****************************
				// **************************************************

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
						break;
					}
				}
			}
		}
		#endregion /Configuration

		public static Gender GetByCultureAndCode(int cultureLcid, Enums.Genders code)
		{
			Gender oResult = null;
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext =
					new DatabaseContext();

				oResult =
					oDatabaseContext.Genders
					.Where(current => current.Code == (int)code)
					.Where(current => current.CultureLcid == cultureLcid)
					.FirstOrDefault();
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

			return (oResult);
		}

		public Gender()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Code)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int Code { get; set; }
		// **********

		// **********
		public Enums.Genders CodeEnum
		{
			get
			{
				return ((Enums.Genders)Code);
			}
		}
		// **********
		// **********
		// **********

		// **********
		/// <summary>
		/// خانم، آقا
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Gender),
			Name = Resources.Models.Strings.GenderKeys.Name1)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 20,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name1 { get; set; }
		// **********

		// **********
		/// <summary>
		/// خانم، آقای
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Gender),
			Name = Resources.Models.Strings.GenderKeys.Name2)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 20,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name2 { get; set; }
		// **********

		// **********
		/// <summary>
		/// زن، مرد
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Gender),
			Name = Resources.Models.Strings.GenderKeys.Name3)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 20,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name3 { get; set; }
		// **********

		// **********
		/// <summary>
		/// مونث، مذکر
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Gender),
			Name = Resources.Models.Strings.GenderKeys.Name4)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 20,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name4 { get; set; }
		// **********

		// **********
		// **********
		// **********
		/// <summary>
		/// Version: 1.0.9
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string ListItemText
		{
			get
			{
				string strResult = Name1;

				return (strResult);
			}
		}
		// **********

		// **********
		/// <summary>
		/// Version: 1.0.9
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string ListItemTextForSiteResponsibles
		{
			get
			{
				string strResult = Name1;

				if (IsDeleted)
				{
					strResult =
						string.Format("{0} - {1}",
						strResult, Resources.Models.General.ListItemIsDeleted);
				}

				if (IsActive == false)
				{
					strResult =
						string.Format("{0} - {1}",
						strResult, Resources.Models.General.ListItemIsInactive);
				}

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		public virtual System.Collections.Generic.IList<User> Users { get; set; }

	}
}
