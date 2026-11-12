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
		(name: "Roles", Schema = Constants.SCHEMA_GENERAL)]
	public class Role : BaseLocalizedEntity, IDropDownList
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Role>
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
								("IX_CultureLcid_Name", 1) { IsUnique = true },
						}));
				// **************************************************

				// **************************************************
				Property(current => current.Code)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Code", 2) { IsUnique = true }));

				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name", 2) { IsUnique = true }));
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
						Property(current => current.Description)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		public static Role GetByCultureAndCode(int cultureLcid, Enums.Roles code)
		{
			Role oResult = null;
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext =
					new DatabaseContext();

				oResult =
					oDatabaseContext.Roles
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

		public Role()
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
		public Enums.Roles CodeEnum
		{
			get
			{
				return ((Enums.Roles)Code);
			}
		}
		// **********

		// **********
		public bool IsSiteResponsible
		{
			get
			{
				switch (CodeEnum)
				{
					case Models.Enums.Roles.Owner:
					case Models.Enums.Roles.Programmer:
					case Models.Enums.Roles.Supervisor:
					case Models.Enums.Roles.Administrator:
					{
						return (true);
					}

					default:
					{
						return (false);
					}
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
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
				string strResult = Name;

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
				string strResult = Name;

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
