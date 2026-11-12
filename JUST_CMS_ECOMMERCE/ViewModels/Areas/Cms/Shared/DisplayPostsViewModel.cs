namespace ViewModels.Areas.Cms.Shared
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/10/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DisplayPostsViewModel : System.Object
	{
		public DisplayPostsViewModel()
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
		// **********
		// **********
		public System.Guid PostCategoryId { get; set; }

		public string PostCategoryName { get; set; }
		// **********
		// **********
		// **********

		// **********
		public string Author { get; set; }
		// **********

		// **********
		// **********
		// **********
		public System.Guid? CreatorUserId { get; set; }
		// **********

		// **********
		public string CreatorUserGender { get; set; }
		// **********

		// **********
		public string CreatorUserLastName { get; set; }
		// **********

		// **********
		public string CreatorUserFirstName { get; set; }
		// **********

		// **********
		public string CreatorUserFullName
		{
			get
			{
				string strResult = CreatorUserGender.Trim();

				if (string.IsNullOrWhiteSpace(CreatorUserFirstName) == false)
				{
					strResult += " " + CreatorUserFirstName.Trim();
				}

				if (string.IsNullOrWhiteSpace(CreatorUserLastName) == false)
				{
					strResult += " " + CreatorUserLastName.Trim();
				}

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		public int Hits { get; set; }
		// **********

		// **********
		// **********
		// **********
		public int CommentCount { get; set; }
		// **********

		// **********
		public bool IsCommentingEnabled { get; set; }
		// **********
		// **********
		// **********

		// **********
		public string Title { get; set; }
		// **********

		// **********
		public string ImageUrl { get; set; }
		// **********

		// **********
		public string Introduction { get; set; }
		// **********

		// **********
		public Models.ComplexTypes.CustomDateTime UpdateDateTime { get; set; }
		// **********
	}
}
