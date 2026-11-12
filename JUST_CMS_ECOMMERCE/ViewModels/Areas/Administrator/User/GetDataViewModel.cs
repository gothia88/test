namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.4
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
		public bool IsEmailAddressVerified { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public bool IsVerified { get; set; }
		// **********

		// **********
		public bool IsDirectContactable { get; set; }
		// **********

		// **********
		public bool IsAccountTerminated { get; set; }
		// **********

		// **********
		public bool IsDeleted { get; set; }
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

		// **********
		// **********
		// **********
		public string PlanName { get; set; }
		// **********

		// **********
		public System.Guid? PlanId { get; set; }
		// **********
		// **********
		// **********

		// **********
		public bool HasAvatar { get; set; }
		// **********
	}
}
