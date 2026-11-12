namespace ViewModels.Areas.ECommerce.Transaction
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetDataForUserViewModel : System.Object
	{
		public GetDataForUserViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		// **********
		// **********
		public System.Guid? PlanId { get; set; }
		// **********

		// **********
		public string PlanName { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public decimal Debit { get; set; }
		// **********

		// **********
		public string DisplayDebit
		{
			get
			{
				string strResult =
					Dtx.Text.Convert.DisplayFormattedNumber(Debit);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public decimal Credit { get; set; }
		// **********

		// **********
		public string DisplayCredit
		{
			get
			{
				string strResult =
					Dtx.Text.Convert.DisplayFormattedNumber(Credit);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public decimal SiteDebit { get; set; }
		// **********

		// **********
		public string DisplaySiteDebit
		{
			get
			{
				string strResult =
					Dtx.Text.Convert.DisplayFormattedNumber(SiteDebit);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public decimal SiteCredit { get; set; }
		// **********

		// **********
		public string DisplaySiteCredit
		{
			get
			{
				string strResult =
					Dtx.Text.Convert.DisplayFormattedNumber(SiteCredit);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		public string Description { get; set; }
		// **********

		// **********
		public bool IsPending { get; set; }
		// **********

		// **********
		public bool IsPaidOnline { get; set; }
		// **********

		// **********
		public string MerchantIdentityGet { get; set; }
		// **********

		// **********
		public string MerchantTransactionIdentity { get; set; }
		// **********

		// **********
		public Models.ComplexTypes.CustomRequiredDateTime UpdateDateTime { get; set; }
		// **********
	}
}
