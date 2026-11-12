namespace ViewModels.Areas.Cms.Page
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/03/06
	/// Changed In Version: 1.9.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class EditLocalizedPageContentViewModel : System.Object
	{
		public EditLocalizedPageContentViewModel()
		{
		}

		// **********
		public string AreaName { get; set; }
		// **********

		// **********
		public string ActionName { get; set; }
		// **********

		// **********
		public string ControllerName { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string LocalizedPageContent { get; set; }
		// **********

		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/03/06
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public override string ToString()
		{
			string strResult = "/";

			if (string.IsNullOrWhiteSpace(AreaName) == false)
			{
				strResult +=
					AreaName.Trim() + "/";
			}

			if (string.IsNullOrWhiteSpace(ControllerName) == false)
			{
				strResult +=
					ControllerName.Trim() + "/";
			}

			if (string.IsNullOrWhiteSpace(ActionName) == false)
			{
				strResult +=
					ActionName.Trim() + "/";
			}

			return (strResult);
		}
	}
}
