namespace ViewModels.Account
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/12/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ChangePasswordViewModel : System.Object
	{
		public ChangePasswordViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Account.ChangePasswordViewModel),
			Name = Resources.ViewModels.Account.Strings.ChangePasswordViewModelKeys.CurrentPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Password,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string CurrentPassword { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Account.ChangePasswordViewModel),
			Name = Resources.ViewModels.Account.Strings.ChangePasswordViewModelKeys.NewPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Password,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string NewPassword { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Account.ChangePasswordViewModel),
			Name = Resources.ViewModels.Account.Strings.ChangePasswordViewModelKeys.ConfirmNewPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Compare
			(otherProperty: "NewPassword",
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.Confirm)]
		public string ConfirmNewPassword { get; set; }
		// **********
	}
}
