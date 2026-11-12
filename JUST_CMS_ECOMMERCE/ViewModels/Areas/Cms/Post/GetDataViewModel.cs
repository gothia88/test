namespace ViewModels.Areas.Cms.Post
{
	/// <summary>
	/// Version: 1.1.1
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetDataViewModel : System.Object
	{
		public GetDataViewModel()
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
		public bool IsDeleted { get; set; }
		// **********

		// **********
		public bool HasAttachment { get; set; }
		// **********

		// **********
		public string Title { get; set; }
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
		// **********
		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		public System.Guid? CreatorUserId { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		[System.Web.Script.Serialization.ScriptIgnore]
		public string CreatorUserGender { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		[System.Web.Script.Serialization.ScriptIgnore]
		public string CreatorUserLastName { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		[System.Web.Script.Serialization.ScriptIgnore]
		public string CreatorUserFirstName { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		public string CreatorUserFullName
		{
			get
			{
				string strResult = string.Empty;

				if (string.IsNullOrWhiteSpace(CreatorUserGender) == false)
				{
					strResult =
						CreatorUserGender.Trim();
				}

				if (string.IsNullOrWhiteSpace(CreatorUserFirstName) == false)
				{
					strResult +=
						" " + CreatorUserFirstName.Trim();
				}

				if (string.IsNullOrWhiteSpace(CreatorUserLastName) == false)
				{
					strResult +=
						" " + CreatorUserLastName.Trim();
				}

				return (strResult.Trim());
			}
		}
		// **********
		// **********
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
