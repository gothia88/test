namespace ViewModels.Areas.ECommerce.Plan
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/01/22
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class IndexViewModel : System.Object, Models.IBaseExtendedEntity
	{
		public IndexViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public bool IsSystem { get; set; }
		// **********

		// **********
		public bool IsDeleted { get; set; }
		// **********

		// **********
		public bool IsVerified { get; set; }
		// **********

		// **********
		public int Ordering { get; set; }
		// **********

		// **********
		public string Name { get; set; }
		// **********

		// **********
		public decimal Price { get; set; }
		// **********

		// **********
		public int DurationInMonth { get; set; }
		// **********

		// **********
		public int UserCount { get; set; }
		// **********
	}
}
