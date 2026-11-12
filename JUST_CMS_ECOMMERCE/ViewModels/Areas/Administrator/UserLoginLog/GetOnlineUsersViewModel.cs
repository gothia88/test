namespace ViewModels.Areas.Administrator.UserLoginLog
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/22
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetOnlineUsersViewModel : System.Object
	{
		public GetOnlineUsersViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public System.Guid UserId { get; set; }
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
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Gender)]
		public string Gender { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.FirstName)]
		public string FirstName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.LastName)]
		public string LastName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.EmailAddress)]
		public string EmailAddress { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.CellPhoneNumber)]
		public string CellPhoneNumber { get; set; }
		// **********
	}
}
