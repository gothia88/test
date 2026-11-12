namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.9
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public abstract class BaseLocalizedPageOrPost : Common.BaseSeo
	{
		public BaseLocalizedPageOrPost()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Layout),
			Name = Resources.Models.Cms.Strings.LayoutKeys.EntityName)]
		public virtual Layout Layout { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Layout),
			Name = Resources.Models.Cms.Strings.LayoutKeys.EntityName)]
		public System.Guid? LayoutId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.AccessType),
			Name = Resources.Models.Strings.AccessTypeKeys.EntityName)]
		public virtual AccessType AccessType { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.AccessType),
			Name = Resources.Models.Strings.AccessTypeKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid AccessTypeId { get; set; }
		// **********

		// **********
		public Enums.AccessTypes AccessTypeEnum
		{
			get
			{
				if (AccessType == null)
				{
					return (Enums.AccessTypes.Registered);
				}
				else
				{
					return ((Enums.AccessTypes)AccessType.Code);
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.General),
			Name = Resources.Models.Cms.Strings.GeneralKeys.IsDraft)]
		public bool IsDraft { get; set; }
		// **********
	}
}
