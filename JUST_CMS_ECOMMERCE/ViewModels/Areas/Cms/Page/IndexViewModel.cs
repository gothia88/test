namespace ViewModels.Areas.Cms.Page
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class IndexViewModel : System.Object, Models.IBaseExtendedEntity
	{
		public IndexViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.General),
			Name = Resources.Models.Cms.Strings.GeneralKeys.IsDraft)]
		public bool IsDraft { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.IsActive)]
		public bool IsActive { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.IsSystem)]
		public bool IsSystem { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.IsDeleted)]
		public bool IsDeleted { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.IsVerified)]
		public bool IsVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Title)]
		public string Title { get; set; }
		// **********

		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Models.Cms.Page),
		//	Name = Resources.Models.Cms.Strings.PageKeys.UserCount)]
		//public int UserCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Page),
			Name = Resources.Models.Cms.Strings.PageKeys.GroupCount)]
		public int GroupCount { get; set; }
		// **********

		// **********
		// **********
		// **********
		public System.Guid? LayoutId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Layout),
			Name = Resources.Models.Cms.Strings.LayoutKeys.EntityName)]
		public string LayoutName { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		public System.Guid? AccessTypeId { get; set; }
		// **********

		// **********
		public int? AccessTypeCode { get; set; }
		// **********

		// **********
		public Models.Enums.AccessTypes AccessTypeEnum
		{
			get
			{
				if (AccessTypeCode.HasValue == false)
				{
					return (Models.Enums.AccessTypes.Public);
				}
				else
				{
					return ((Models.Enums.AccessTypes)AccessTypeCode.Value);
				}
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.AccessType),
			Name = Resources.Models.Strings.AccessTypeKeys.EntityName)]
		public string DisplayAccessType { get; set; }
		// **********
		// **********
		// **********
	}
}
