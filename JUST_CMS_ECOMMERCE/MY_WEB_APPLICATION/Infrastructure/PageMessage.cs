namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/06/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PageMessage : System.Object
	{
		public PageMessage
			(Enums.PageMessageTyps type, string message)
		{
			Type = type;
			Message = message;
		}

		public string Message { get; set; }
		public Enums.PageMessageTyps Type { get; set; }
	}
}
