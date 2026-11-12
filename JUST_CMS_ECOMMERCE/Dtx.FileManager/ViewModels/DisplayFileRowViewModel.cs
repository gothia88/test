namespace Dtx.FileManager.ViewModels
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/10/13
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DisplayFileRowViewModel : System.Object
	{
		public DisplayFileRowViewModel
			(string rootRelativePath, System.IO.FileInfo fileInfo)
		{
			FileInfo = fileInfo;
			RootRelativePath = rootRelativePath;
		}

		public string RootRelativePath { get; set; }

		public System.IO.FileInfo FileInfo { get; set; }
	}
}
