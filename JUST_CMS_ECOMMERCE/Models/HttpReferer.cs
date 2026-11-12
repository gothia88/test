namespace Models
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/05/22
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "HttpReferers", Schema = Constants.SCHEMA_GENERAL)]
	public class HttpReferer : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<HttpReferer>
		{
			public Configuration()
			{
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

		public HttpReferer()
		{
			LastHitDateTime = new ComplexTypes.CustomRequiredDateTime();
			FirstHitDateTime = new ComplexTypes.CustomRequiredDateTime();
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.HttpReferer),
			Name = Resources.Models.Strings.HttpRefererKeys.Url)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Url { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.HttpReferer),
			Name = Resources.Models.Strings.HttpRefererKeys.DomainName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string DomainName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.HttpReferer),
			Name = Resources.Models.Strings.HttpRefererKeys.Hits)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public int Hits { get; set; }
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
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.HttpReferer),
			Name = Resources.Models.Strings.HttpRefererKeys.IsPublic)]
		public bool IsPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.HttpReferer),
			Name = Resources.Models.Strings.HttpRefererKeys.LastHitDateTime)]
		public ComplexTypes.CustomRequiredDateTime LastHitDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.HttpReferer),
			Name = Resources.Models.Strings.HttpRefererKeys.FirstHitDateTime)]
		public ComplexTypes.CustomRequiredDateTime FirstHitDateTime { get; set; }
		// **********
	}
}
