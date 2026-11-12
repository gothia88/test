namespace Models
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/06/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public abstract class BaseAreaControllerAction : BaseExtendedEntity
	{
		public BaseAreaControllerAction()
		{
		}

		// **********
		public bool IsAvailableInSourceCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.BaseAreaControllerAction),
			Name = Resources.Models.Strings.BaseAreaControllerActionKeys.IsVisibleJustForProgrammer)]
		public bool IsVisibleJustForProgrammer { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		// Note: دستور ذيل نبايد نوشته شود
		//[System.ComponentModel.DataAnnotations.Required
		//	(AllowEmptyStrings = false,
		//	ErrorMessageResourceType = typeof(Resources.Messages),
		//	ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.BaseAreaControllerAction),
			Name = Resources.Models.Strings.BaseAreaControllerActionKeys.DisplayName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string DisplayName { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********
	}
}
