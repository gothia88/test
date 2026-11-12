namespace Models.Common
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/09/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public abstract class BaseSeo : BaseLocalizedEntity
	{
		public BaseSeo()
		{
			StartPublishingDateTime = new ComplexTypes.CustomDateTime();
			FinishPublishingDateTime = new ComplexTypes.CustomDateTime();
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Tags)]
		public string Tags { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Hits)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int Hits { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.DoesSearchEnginesIndexIt)]
		public bool DoesSearchEnginesIndexIt { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.DoesSearchEnginesFollowIt)]
		public bool DoesSearchEnginesFollowIt { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Title)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Title { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Author)]
		public string Author { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Keywords)]
		public string Keywords { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Classification)]
		public string Classification { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Description { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.Copyright)]
		public string Copyright { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.StartPublishingDateTime)]
		public ComplexTypes.CustomDateTime StartPublishingDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Common.BaseSeo),
			Name = Resources.Models.Common.Strings.BaseSeoKeys.FinishPublishingDateTime)]
		public ComplexTypes.CustomDateTime FinishPublishingDateTime { get; set; }
		// **********
	}
}
