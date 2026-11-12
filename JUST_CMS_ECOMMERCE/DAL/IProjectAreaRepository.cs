namespace DAL
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1392/12/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IProjectAreaRepository : IRepository<Models.ProjectArea>
	{
		Models.ProjectArea GetByName(string name);

		Models.ProjectArea GetByNameExceptId(string name, System.Guid id);

		System.Linq.IOrderedQueryable<Models.ProjectArea>
			GetByRole(Models.Enums.Roles role);

		System.Linq.IQueryable<ViewModels.Areas.Administrator.ProjectArea.IndexViewModel>
			GetViewModelByRole(Models.Enums.Roles role);
	}
}
