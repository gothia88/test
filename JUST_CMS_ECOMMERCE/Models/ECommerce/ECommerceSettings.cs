namespace Models.ECommerce
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("ECommerceSettings", Schema = Constants.SCHEMA_ECOMMERCE)]
	public class ECommerceSettings : BaseEntity
	{
		#region Configuration
		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ECommerceSettings>
		{
			public Configuration()
			{
				Property(current => current.SiteCommissionRateForCreditTransfer)
					.HasPrecision(precision: 5, scale: 2);

				switch (Models.Utility.DatabaseProvider)
				{
					case Models.Enums.DatabaseProviders.Oracle:
					{
						break;
					}

					case Models.Enums.DatabaseProviders.SqlServer:
					{
						break;
					}

					case Models.Enums.DatabaseProviders.SqlServerCompactEdition:
					{
						break;
					}
				}
			}
		}
		#endregion /Configuration

		public ECommerceSettings()
		{
			Merchant = new ECommerce.Merchant();

			IsCreditTransferEnabled = false;
			SiteCommissionRateForCreditTransfer = 10;

			CultureLcid =
				System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
		}

		// **********
		public ECommerce.Merchant Merchant { get; set; }
		// **********

		// **********
		public bool IsRandomDataGenerated { get; set; }
		// **********

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
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Settings),
			Name = Resources.Models.ECommerce.Strings.SettingsKeys.IsPlansServiceEnabled)]
		public bool IsPlansServiceEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Settings),
			Name = Resources.Models.ECommerce.Strings.SettingsKeys.IsOnlinePaymentEnabled)]
		public bool IsOnlinePaymentEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Settings),
			Name = Resources.Models.ECommerce.Strings.SettingsKeys.IsCreditTransferEnabled)]
		public bool IsCreditTransferEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Settings),
			Name = Resources.Models.ECommerce.Strings.SettingsKeys.SiteCommissionRateForCreditTransfer)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Percentage,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPercentage)]
		public decimal SiteCommissionRateForCreditTransfer { get; set; }
		// **********
	}
}
