namespace ViewModels.Account
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/02/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class RegisterViewModel : System.Object
	{
		public RegisterViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel),
			Name = Resources.ViewModels.Account.Strings.RegisterViewModelKeys.EmailAddress)]

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

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.CellPhoneNumber)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 14,
			MinimumLength = 14,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.StringLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.CellPhoneNumber,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForCellPhoneNumber)]
		public string CellPhoneNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel),
			Name = Resources.ViewModels.Account.Strings.RegisterViewModelKeys.Password)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Password,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string Password { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel),
			Name = Resources.ViewModels.Account.Strings.RegisterViewModelKeys.ConfirmPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Compare
			(otherProperty: "Password",
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.Confirm)]
		public string ConfirmPassword { get; set; }
		// **********
	}
}
