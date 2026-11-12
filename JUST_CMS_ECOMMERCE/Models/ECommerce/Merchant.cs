namespace Models.ECommerce
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/02
	/// Changed In Version: 1.9.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.ComplexType]
	public class Merchant : System.Object
	{
		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/02
		/// Changed In Version: 1.9.0
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public Merchant()
		{
			Name = "Payline.ir";

			ApiName = "api";

			// واقعی
			//ApiValue = string.Empty;
			//FirstSendUrl = "http://payline.ir/payment/gateway-send";
			//SecondSendUrl = "http://payline.ir/payment/gateway-";
			//GetUrl = "http://payline.ir/payment/gateway-result-second";

			// تستی
			ApiValue = "adxcv-zzadq-polkjsad-opp13opoz-1sdf455aadzmck1244567";
			FirstSendUrl = "http://payline.ir/payment-test/gateway-send";
			SecondSendUrl = "http://payline.ir/payment-test/gateway-";
			GetUrl = "http://payline.ir/payment-test/gateway-result-second";

			GetMethod = "POST";
			FirstSendMethod = "POST";

			AmountName = "amount";
			RedirectName = "redirect";

			IdentityGetName = "id_get";
			TransactionIdentityName = "trans_id";

			RedirectValue = "http://www.IranContent.ir/ECommerce/Transaction/CheckOnlinePayment";
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "First Send Url")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 255,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string FirstSendUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "First Send Method")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 10,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string FirstSendMethod { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Api Name")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string ApiName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Api Value")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string ApiValue { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Amount Name")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string AmountName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Redirect Name")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string RedirectName { get; set; }
		// **********

		// **********
		/// <summary>
		/// بايد مسير کامل (همراه نشانی پايگاه خودمان) نوشته نوشته شود
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Redirect Value")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 255,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string RedirectValue { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Second Send Url")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 255,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string SecondSendUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Get Url")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 255,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string GetUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Get Method")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 10,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string GetMethod { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Transaction Identity Name")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string TransactionIdentityName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = "Identity Get Name")]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string IdentityGetName { get; set; }
		// **********
	}
}
