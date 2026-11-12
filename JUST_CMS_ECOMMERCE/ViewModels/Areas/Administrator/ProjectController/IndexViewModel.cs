namespace ViewModels.Areas.Administrator.ProjectController
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/06/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class IndexViewModel : System.Object, Models.IBaseExtendedEntity
	{
		public IndexViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Id)]
		public System.Guid Id { get; set; }
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
			Name = Resources.Models.Strings.GeneralKeys.Ordering)]
		public int Ordering { get; set; }
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
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.BaseAreaControllerAction),
			Name = Resources.Models.Strings.BaseAreaControllerActionKeys.DisplayName)]
		public string DisplayName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ProjectController),
			Name = Resources.Models.Strings.ProjectControllerKeys.ProjectActionCount)]
		public int ProjectActionCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.InsertDateTime)]
		public Models.ComplexTypes.CustomDateTime InsertDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.UpdateDateTime)]
		public Models.ComplexTypes.CustomDateTime UpdateDateTime { get; set; }
		// **********

		public bool IsSystem
		{
			get
			{
				return (false);
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public bool IsDeleted
		{
			get
			{
				return (false);
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public bool IsVerified
		{
			get
			{
				return (false);
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
