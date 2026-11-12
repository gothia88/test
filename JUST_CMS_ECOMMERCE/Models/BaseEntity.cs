namespace Models
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/11/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.Serializable]
	public abstract class BaseEntity : System.Object
	{
		public BaseEntity()
		{
			Id = System.Guid.NewGuid();
		}

		// **********
		[System.ComponentModel.DataAnnotations.Key]

		//[System.ComponentModel.DataAnnotations.Display(Name = "شناسه")]

		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Models.General),
		//	Name = "Id")]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Id)]

		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
		public System.Guid Id { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.MasterDataCode)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]

		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
		public long? MasterDataCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.MasterDataIdentity)]

		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
		public System.Guid? MasterDataIdentity { get; set; }
		// **********
	}
}
