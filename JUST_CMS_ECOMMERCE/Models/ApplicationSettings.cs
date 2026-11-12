namespace Models
{
	/// <summary>
	/// Version: 1.2.5
	/// Update Date: 1394/01/19
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "ApplicationSettings", Schema = Constants.SCHEMA_GENERAL)]
	public class ApplicationSettings : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationSettings>
		{
			public Configuration()
			{
				HasOptional(current => current.MailSettings)
					.WithMany(mailSettings => mailSettings.AllOfApplicationSettings)
					.HasForeignKey(current => current.MailSettingsId)
					.WillCascadeOnDelete(false)
					;

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
						Property(current => current.ApplicationName)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.HomePageKeywords)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.HomePageCopyright)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.ApplicationVersion)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.HomePageClassification)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.ApplicationCopyrightUrl)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.ApplicationCopyrightText)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		public ApplicationSettings()
		{
			IsUserEmailVerificationRequiredForLogin = true;

			CultureLcid =
				System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.CultureId)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public System.Guid CultureId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.CultureLcid)]

		[System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 3)]
		public int CultureLcid { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.MailSettings),
			Name = Resources.Models.Strings.MailSettingsKeys.EntityName)]
		public virtual MailSettings MailSettings { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.MailSettings),
			Name = Resources.Models.Strings.MailSettingsKeys.EntityName)]
		public System.Guid? MailSettingsId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ApplicationName)]
		public string ApplicationName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ApplicationVersion)]
		public string ApplicationVersion { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ApplicationCopyrightText)]
		public string ApplicationCopyrightText { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ApplicationCopyrightUrl)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Url,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]
		public string ApplicationCopyrightUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.CheckNationalCode)]
		public bool CheckNationalCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ServerTimeZoneDifference)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Integer,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForInteger)]
		public int ServerTimeZoneDifference { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ForceUserToUpdateProfileAfterLogin)]
		public bool ForceUserToUpdateProfileAfterLogin { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.IsCaptchaImageEnabled)]
		public bool IsCaptchaImageEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.IsSmsServiceEnabled)]
		public bool IsSmsServiceEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.IsCmsEnabled)]
		public bool IsCmsEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.IsECommerceSystemEnabled)]
		public bool IsECommerceSystemEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.ShouldUserBeActivatedAfterRegistration)]
		public bool ShouldUserBeActivatedAfterRegistration { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.CanUserAttachFileInContactUsPage)]
		public bool CanUserAttachFileInContactUsPage { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ApplicationSettings),
			Name = Resources.Models.Strings.ApplicationSettingsKeys.IsUserEmailVerificationRequiredForLogin)]
		public bool IsUserEmailVerificationRequiredForLogin { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Title)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string HomePageTitle { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Keywords)]
		public string HomePageKeywords { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Classification)]
		public string HomePageClassification { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string HomePageDescription { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Copyright)]
		public string HomePageCopyright { get; set; }
		// **********
	}
}
