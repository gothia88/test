namespace Models.Common
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/01/04
	/// Changed In Version: 1.8.4
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public abstract class BaseComment : BaseEntity
	{
		public BaseComment()
		{
			InsertDateTime = new ComplexTypes.CustomDateTime();
		}

		// **********
		public bool IsRead { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public bool IsPrivate { get; set; }
		// **********

		// **********
		public string UserIP { get; set; }
		// **********

		// **********
		public string UserUrl { get; set; }
		// **********

		// **********
		public string UserFullName { get; set; }
		// **********

		// **********
		public string UserEmailAddress { get; set; }
		// **********

		// **********
		public string Subject { get; set; }
		// **********

		// **********
		public string Description { get; set; }
		// **********

		// **********
		public ComplexTypes.CustomDateTime InsertDateTime { get; set; }
		// **********
	}
}
