namespace ViewModels.Areas.Administrator.Programmer
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class BackupDatabaseViewModel : System.Object
	{
		public BackupDatabaseViewModel()
		{
			INIT = true;
			CHECKSUM = true;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 255,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string FileName { get; set; }
		// **********

		// **********
		/// <summary>
		///
		/// </summary>
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 128,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string NAME { get; set; }
		// **********

		// **********
		/// <summary>
		///
		/// </summary>
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 128,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string MEDIANAME { get; set; }
		// **********

		// **********
		/// <summary>
		///
		/// </summary>
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 255,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string MEDIADESCRIPTION { get; set; }
		// **********

		// **********
		/// <summary>
		/// INIT  - [NOINIT]
		/// </summary>
		public bool INIT { get; set; }
		// **********

		// **********
		/// <summary>
		/// SKIP - [NOSKIP]
		/// </summary>
		public bool SKIP { get; set; }
		// **********

		// **********
		/// <summary>
		/// FORMAT - [NOFORMAT]
		/// </summary>
		public bool FORMAT { get; set; }
		// **********

		// **********
		/// <summary>
		/// CHECKSUM - [NO-CHECKSUM]
		/// </summary>
		public bool CHECKSUM { get; set; }
		// **********

		// **********
		/// <summary>
		/// CONTINUE_AFTER_ERROR - [STOP_ON_ERROR]
		/// </summary>
		public bool CONTINUE_AFTER_ERROR { get; set; }
		// **********
	}
}
