namespace Models.Common
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/08/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ITag
	{
		string Tags { get; set; }

		System.Collections.Generic.IList<Tag> TagList { get; set; }
	}
}
