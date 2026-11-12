namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.1
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
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.CultureLcid)]
		public int? CultureLcid { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Role),
			Name = Resources.Models.Strings.RoleKeys.EntityName)]
		public System.Guid? RoleId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Plan),
			Name = Resources.Models.ECommerce.Strings.PlanKeys.EntityName)]
		public System.Guid? PlanId { get; set; }
		// **********

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
