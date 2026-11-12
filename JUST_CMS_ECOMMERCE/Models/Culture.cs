namespace Models
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "Cultures", Schema = Constants.SCHEMA_GENERAL)]
	public class Culture : BaseExtendedEntity, IDropDownList
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Culture>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.Lcid)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************

				// **************************************************
				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));

				// **************************************************
				Property(current => current.NativeName)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************

				// **************************************************
				Property(current => current.DisplayName)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************
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

		public Culture()
		{
		}

		public Culture(System.Globalization.CultureInfo cultureInfo)
		{
			Lcid = cultureInfo.LCID;
			Name = cultureInfo.Name;
			NativeName = cultureInfo.NativeName;
			DisplayName = cultureInfo.DisplayName;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.MyCulture),
			Name = Resources.Models.Strings.MyCultureKeys.Lcid)]
		public int Lcid { get; set; }
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
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.MyCulture),
			Name = Resources.Models.Strings.MyCultureKeys.NativeName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string NativeName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.MyCulture),
			Name = Resources.Models.Strings.MyCultureKeys.DisplayName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string DisplayName { get; set; }
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
		/// Version: 1.0.6
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string ListItemText
		{
			get
			{
				string strResult = DisplayName;

				return (strResult);
			}
		}
		// **********

		// **********
		/// <summary>
		/// Version: 1.0.6
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string ListItemTextForSiteResponsibles
		{
			get
			{
				string strResult = DisplayName;

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
	}
}
