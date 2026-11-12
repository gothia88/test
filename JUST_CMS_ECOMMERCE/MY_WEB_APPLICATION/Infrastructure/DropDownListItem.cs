namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/12/17
	/// Changed In Version: 1.8.2
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DropDownListItem : System.Object
	{
		public DropDownListItem()
		{
		}

		public DropDownListItem(object id, object name)
		{
			if (id == null)
			{
				Id = string.Empty;
			}
			else
			{
				Id = id.ToString().Trim();
			}

			if (name == null)
			{
				Name = string.Empty;
			}
			else
			{
				Name = name.ToString().Trim();
			}
		}

		public string Id { get; set; }

		public string Name { get; set; }
	}
}
