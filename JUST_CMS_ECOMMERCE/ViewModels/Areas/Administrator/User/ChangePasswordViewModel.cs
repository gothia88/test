namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/12/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ChangePasswordViewModel : System.Object
	{
		public ChangePasswordViewModel()
		{
		}

		// **********
		public System.Guid UserId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.User.ChangePasswordViewModel),
			Name = Resources.ViewModels.Areas.Administrator.User.Strings.ChangePasswordViewModelKeys.NewPassword)]

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
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.User.ChangePasswordViewModel),
			Name = Resources.ViewModels.Areas.Administrator.User.Strings.ChangePasswordViewModelKeys.ConfirmNewPassword)]

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
