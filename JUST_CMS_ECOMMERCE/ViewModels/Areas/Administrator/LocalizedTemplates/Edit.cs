namespace ViewModels.Areas.Administrator.LocalizedTemplates
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/07/09
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class EditViewModel : System.Object
	{
		public EditViewModel()
		{
		}

		[System.Web.Mvc.AllowHtml]
		public string Content { get; set; }
		public string FileName { get; set; }
		public string DirectoryName { get; set; }
	}
}
