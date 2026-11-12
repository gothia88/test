namespace ViewModels.Areas.Cms.Shared
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/10/17
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetActiveUserPostsViewModel : System.Object
	{
		public GetActiveUserPostsViewModel()
		{
			UpdateDateTime =
				new Models.ComplexTypes.CustomDateTime();
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public bool IsVerified { get; set; }
		// **********

		// **********
		public bool IsFeatured { get; set; }
		// **********

		// **********
		public string Title { get; set; }
		// **********

		// **********
		public bool HasAttachment { get; set; }
		// **********

		// **********
		public Models.ComplexTypes.CustomDateTime UpdateDateTime { get; set; }
		// **********
	}
}
