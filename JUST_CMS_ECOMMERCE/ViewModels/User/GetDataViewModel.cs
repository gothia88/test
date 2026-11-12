namespace ViewModels.User
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/01/05
	/// Changed In Version: 1.8.5
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
		public bool HasAvatar { get; set; }
		// **********

		// **********
		public bool IsVerified { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public string GenderName { get; set; }
		// **********

		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public string FirstName { get; set; }
		// **********

		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public string LastName { get; set; }
		// **********

		// **********
		public string FullName
		{
			get
			{
				string strResult = string.Empty;

				if (string.IsNullOrWhiteSpace(GenderName) == false)
				{
					strResult =
						GenderName.Trim();
				}

				if (string.IsNullOrWhiteSpace(FirstName) == false)
				{
					strResult +=
						" " + FirstName.Trim();
				}

				if (string.IsNullOrWhiteSpace(LastName) == false)
				{
					strResult +=
						" " + LastName.Trim();
				}

				return (strResult.Trim());
			}
		}
		// **********
		// **********
		// **********

		// **********
		public string RoleName { get; set; }
		// **********
	}
}
