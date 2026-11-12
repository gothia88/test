namespace ViewModels.Areas.Administrator.WebDownloader
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DownloadViewModel : System.Object
	{
		public DownloadViewModel()
		{
			PocketSize = 64;
			FlushLength = 10;
			SourceTimeout = 100;
			TargetTimeout = 300;
		}

		// **********
		public string TargetPathName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.TargetFileName)]
		public System.TimeSpan? DownLoadAndSaveToFileDuration { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.Flush)]
		public bool Flush { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.FlushLength)]
		public int FlushLength { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.SourceFileUrl)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public string SourceFileUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.TargetFileName)]
		public string TargetFileName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.PocketSize)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int PocketSize { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.TargetTimeout)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int TargetTimeout { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = Resources.ViewModels.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.SourceTimeout)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int SourceTimeout { get; set; }
		// **********
	}
}
