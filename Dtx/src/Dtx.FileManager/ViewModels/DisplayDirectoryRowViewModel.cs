namespace Dtx.FileManager.ViewModels
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/10/13
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DisplayDirectoryRowViewModel : System.Object
	{
		public DisplayDirectoryRowViewModel
			(string rootRelativePath, System.IO.DirectoryInfo directoryInfo)
		{
			DirectoryInfo = directoryInfo;
			RootRelativePath = rootRelativePath;
		}

		public string RootRelativePath { get; set; }

		public System.IO.DirectoryInfo DirectoryInfo { get; set; }
	}
}
