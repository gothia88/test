namespace Models
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1393/06/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.Serializable]
	public abstract class BaseLocalizedEntity : BaseExtendedEntity
	{
		public BaseLocalizedEntity()
		{
			CultureLcid =
				System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.CultureId)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public System.Guid CultureId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.CultureLcid)]

		[System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 3)]
		public int CultureLcid { get; set; }
		// **********
	}
}
