namespace Dtx.Kendo
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/07/26
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GridSort : System.Object
	{
		//public GridSort()
		//{
		//}

		public GridSort(string field, string direction)
		{
			Field = field;
			Direction = direction;
		}

		public string Field { get; set; }
		public string Direction { get; set; }
	}
}
