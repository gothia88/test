namespace ViewModels.Account
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/08/15
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ResetPasswordViewModel : System.Object
	{
		public ResetPasswordViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.EmailAddress)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Email,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForEmail)]
		public string EmailAddress { get; set; }
		// **********
	}
}
