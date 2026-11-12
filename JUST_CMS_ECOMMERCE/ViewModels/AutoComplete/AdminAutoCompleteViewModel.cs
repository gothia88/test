namespace ViewModels.AutoComplete
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/05
	/// Changed In Version: 1.8.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AdminAutoCompleteViewModel : UserAutoCompleteViewModel
	{
		public AdminAutoCompleteViewModel()
		{
		}

		// **********
		public override string DisplayName
		{
			get
			{
				string strResult = Name;

				if (IsDeleted)
				{
					strResult =
						string.Format("{0} - {1}",
						strResult, Resources.Models.General.IsDeleted);
				}

				if (IsActive == false)
				{
					strResult =
						string.Format("{0} - {1}",
						strResult, Resources.Models.General.Inactive);
				}

				return (strResult);
			}
		}
		// **********
	}
}
