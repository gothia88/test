namespace ViewModels.Areas.ECommerce.Transaction
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class TransferCreditToAnotherUserViewModel : System.Object
	{
		public TransferCreditToAnotherUserViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.RecipientUserId)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid RecipientUserId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.AmountForTransfer)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForInteger)]
		public long Amount { get; set; }
		// **********
	}
}
