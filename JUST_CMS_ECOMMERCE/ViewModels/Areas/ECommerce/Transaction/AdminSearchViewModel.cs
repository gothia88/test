namespace ViewModels.Areas.ECommerce.Transaction
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AdminSearchViewModel : System.Object
	{
		public AdminSearchViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]
		public string Description { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.PlanId)]
		public System.Guid? PlanId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.UserId)]
		public System.Guid? UserId { get; set; }
		// **********
	}
}
