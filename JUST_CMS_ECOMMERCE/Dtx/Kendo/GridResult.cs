namespace Dtx.Kendo
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/07/26
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GridResult<T> : System.Object
	{
		public GridResult()
		{
		}

		public int Total { get; set; }
		public System.Collections.Generic.IEnumerable<T> Data { get; set; }
	}
}
