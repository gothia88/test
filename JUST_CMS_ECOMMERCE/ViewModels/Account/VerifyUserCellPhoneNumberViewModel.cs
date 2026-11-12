namespace ViewModels.Account
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/05
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class VerifyUserCellPhoneNumberViewModel : System.Object
	{
		public VerifyUserCellPhoneNumberViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.CellPhoneNumberVerificationKey)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 6,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.MaxLength)]
		public string VerificationKey { get; set; }
		// **********
	}
}
