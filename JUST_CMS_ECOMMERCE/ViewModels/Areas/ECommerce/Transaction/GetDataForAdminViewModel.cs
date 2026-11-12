namespace ViewModels.Areas.ECommerce.Transaction
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GetDataForAdminViewModel : GetDataForUserViewModel
	{
		public GetDataForAdminViewModel()
		{
		}

		// **********
		// **********
		// **********
		public System.Guid? UserId { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		public string UserGender { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		public string UserLastName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		public string UserFirstName { get; set; }

		public string UserFullName
		{
			get
			{
				string strResult = string.Empty;

				if (string.IsNullOrWhiteSpace(UserGender) == false)
				{
					strResult =
						UserGender.Trim();
				}

				if (string.IsNullOrWhiteSpace(UserFirstName) == false)
				{
					strResult +=
						" " + UserFirstName.Trim();
				}

				if (string.IsNullOrWhiteSpace(UserLastName) == false)
				{
					strResult +=
						" " + UserLastName.Trim();
				}

				return (strResult.Trim());
			}
		}
		// **********
		// **********
		// **********
	}
}
