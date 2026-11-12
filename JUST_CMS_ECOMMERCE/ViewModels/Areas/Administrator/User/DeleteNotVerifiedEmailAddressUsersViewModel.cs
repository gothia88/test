namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/03/06
	/// Changed In Version: 1.9.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DeleteNotVerifiedEmailAddressUsersViewModel : System.Object
	{
		public DeleteNotVerifiedEmailAddressUsersViewModel()
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
