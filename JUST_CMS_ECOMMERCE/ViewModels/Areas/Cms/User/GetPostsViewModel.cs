namespace ViewModels.Areas.Cms.User
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetPostsViewModel : System.Object
	{
		public GetPostsViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		public bool IsDraft { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public bool IsVerified { get; set; }
		// **********

		// **********
		public bool IsFeatured { get; set; }
		// **********

		// **********
		public bool HasAttachment { get; set; }
		// **********

		// **********
		// **********
		// **********
		public System.Guid PostCategoryId { get; set; }
		// **********

		// **********
		public string PostCategoryName { get; set; }
		// **********
		// **********
		// **********

		// **********
		public string Title { get; set; }
		// **********

		// **********
		// **********
		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		[System.Web.Script.Serialization.ScriptIgnore]
		public int CommentCount { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		public string DisplayCommentCount
		{
			get
			{
				string strResult =
					Dtx.Text.Convert.DisplayFormattedNumber(CommentCount);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		public Models.ComplexTypes.CustomDateTime UpdateDateTime { get; set; }
		// **********
	}
}
