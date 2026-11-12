namespace Models.Common
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/10/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IFileContainer
	{
		// **********
		System.Guid Id { get; set; }
		// **********

		// **********
		bool HasAttachment { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.FileName)]
		string FileName { get; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.DownloadCount)]
		int DownloadCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.AttachmentLength)]
		long AttachmentLength { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Sha1OfAttachment)]
		string Sha1OfAttachment { get; set; }
		// **********
	}
}
