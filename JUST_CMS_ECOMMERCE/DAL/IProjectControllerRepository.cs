namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/04/24
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IProjectControllerRepository : IRepository<Models.ProjectController>
	{
		Models.ProjectController GetByProjectAreaIdAndName
			(System.Guid projectAreaId, string name);

		Models.ProjectController GetByProjectAreaIdAndNameExceptId
			(System.Guid projectAreaId, string name, System.Guid id);

		System.Linq.IOrderedEnumerable<Models.ProjectController>
			GetByProjectAreaAndRole(Models.ProjectArea projectArea, Models.Enums.Roles role);

		System.Linq.IQueryable<ViewModels.Areas.Administrator.ProjectController.IndexViewModel>
			GetViewModelByRole(System.Guid projectAreaId, Models.Enums.Roles role);
	}
}
