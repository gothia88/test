namespace ViewModels.Areas.Administrator.UserLoginLog
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/04/25
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetUserLoginLogsViewModel : System.Object
	{
		public GetUserLoginLogsViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.IsHidden)]
		public bool IsHidden { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.IP)]
		public string IP { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.LoginDateTime)]
		public Models.ComplexTypes.CustomDateTime LoginDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.LogoutDateTime)]
		public Models.ComplexTypes.CustomDateTime LogoutDateTime { get; set; }
		// **********
	}
}
