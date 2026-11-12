namespace ViewModels.AutoComplete
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/01/05
	/// Changed In Version: 1.8.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UserAutoCompleteViewModel : System.Object
	{
		public UserAutoCompleteViewModel()
		{
		}

		// **********
		public string Id { get; set; }
		// **********

		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public bool IsActive { get; set; }
		// **********

		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public bool IsDeleted { get; set; }
		// **********

		// **********
		[System.Web.Script.Serialization.ScriptIgnore]
		public string Name { get; set; }
		// **********

		// **********
		public virtual string DisplayName
		{
			get
			{
				return (Name);
			}
		}
		// **********
	}
}
