namespace DAL
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/01/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IProjectActionRepository : IRepository<Models.ProjectAction>
	{
		int GetProjectActionCountByLayoutId(System.Guid layoutId);

		Models.ProjectAction GetByRouteValues
			(string areaName, string controllerName, string actionName);

		Models.ProjectAction GetByProjectControllerIdAndName
			(System.Guid projectControllerId, string name);

		Models.ProjectAction GetByProjectControllerIdAndNameExceptId
			(System.Guid projectControllerId, string name, System.Guid id);

		System.Linq.IOrderedQueryable<Models.ProjectAction> GetByProjectControllerIdAndRole
			(System.Guid projectControllerId, Models.Enums.Roles role);

		System.Linq.IOrderedEnumerable<Models.ProjectAction>
			GetByProjectControllerAndRole
			(Models.ProjectController projectController, Models.Enums.Roles role);

		System.Linq.IOrderedEnumerable<Models.ProjectAction>
			GetSpecialByProjectControllerAndRole
			(Models.ProjectController projectController, Models.Enums.Roles role);
	}
}
