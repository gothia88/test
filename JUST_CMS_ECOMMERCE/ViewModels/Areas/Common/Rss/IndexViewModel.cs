namespace ViewModels.Areas.Common.Rss
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/10/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class IndexViewModel : System.Object
	{
		public IndexViewModel()
		{
			UpdateDateTime =
				new Models.ComplexTypes.CustomDateTime();
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public string Title { get; set; }
		// **********

		// **********
		public string Description { get; set; }
		// **********

		// **********
		public Models.ComplexTypes.CustomDateTime UpdateDateTime { get; set; }
		// **********

		// **********
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
				string strResult = string.Empty;

				if (string.IsNullOrWhiteSpace(CreatorUserGender) == false)
				{
					strResult += CreatorUserGender.Trim();
				}

				if (string.IsNullOrWhiteSpace(CreatorUserFirstName) == false)
				{
					strResult += " " + CreatorUserFirstName.Trim();
				}

				if (string.IsNullOrWhiteSpace(CreatorUserLastName) == false)
				{
					strResult += " " + CreatorUserLastName.Trim();
				}

				return (strResult.Trim());
			}
		}
		// **********

		// **********
		public string CreatorUserEmailAddress { get; set; }
		// **********
		// **********
		// **********
	}
}
