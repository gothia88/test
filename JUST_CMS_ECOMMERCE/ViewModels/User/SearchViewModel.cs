namespace ViewModels.User
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/01/05
	/// Changed In Version: 1.8.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class SearchViewModel : System.Object
	{
		public SearchViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.Gender)]
		public System.Guid? GenderId { get; set; }
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
			Name = Resources.Models.Strings.UserKeys.FirstName)]
		public string FirstName { get; set; }
		// **********
	}
}
