namespace ViewModels.Shared
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class _Partial_SelectProjectActionsViewModel : System.Object
	{
		//public _Partial_SelectProjectActionsViewModel()
		//{
		//}

		public _Partial_SelectProjectActionsViewModel
			(System.Guid id,
			string name,
			string entityName,
			string returnLink,
			System.Collections.Generic.IEnumerable<Models.ProjectArea> projectAreas,
			System.Collections.Generic.IEnumerable<Models.ProjectAction> selectedProjectActions)
		{
			Id = id;
			Name = name;
			EntityName = entityName;
			ReturnLink = returnLink;
			ProjectAreas = projectAreas;
			SelectedProjectActions = selectedProjectActions;
		}

		public System.Guid Id { get; set; }
		public bool DisplayAllActions { get; set; }

		public string Name { get; set; }
		public string EntityName { get; set; }
		public string ReturnLink { get; set; }

		public System.Collections.Generic.IEnumerable<Models.ProjectArea> ProjectAreas { get; set; }
		public System.Collections.Generic.IEnumerable<Models.ProjectAction> SelectedProjectActions { get; set; }
	}
}
