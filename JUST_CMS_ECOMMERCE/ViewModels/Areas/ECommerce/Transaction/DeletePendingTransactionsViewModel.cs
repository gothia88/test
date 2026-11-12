namespace ViewModels.Areas.ECommerce.Transaction
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/03/02
	/// Changed In Version: 1.9.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DeletePendingTransactionsViewModel : System.Object
	{
		public DeletePendingTransactionsViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Hours)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int Hours { get; set; }
		// **********
	}
}
