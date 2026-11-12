using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/01/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectActionRepository :
		Repository<Models.ProjectAction>, IProjectActionRepository
	{
		public ProjectActionRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public int GetProjectActionCountByLayoutId(System.Guid layoutId)
		{
			int intProjectActionCount =
				Get()
				.Where(current => current.LayoutId == layoutId)
				.Count()
				;

			return (intProjectActionCount);
		}

		public Models.ProjectAction GetByRouteValues
			(string areaName, string controllerName, string actionName)
		{
			Models.ProjectAction oAction =
				Get()
				.Include(current => current.ProjectController)
				.Include(current => current.ProjectController.ProjectArea)
				.Where(current => current.Name == actionName)
				.Where(current => current.ProjectController.Name == controllerName)
				.Where(current => current.ProjectController.ProjectArea.Name == areaName)
				.FirstOrDefault();

			return (oAction);
		}


		public Models.ProjectAction GetByProjectControllerIdAndName
			(System.Guid projectControllerId, string name)
		{
			Models.ProjectAction oProjectAction =
				Get()
				.Where(current => string.Compare(current.Name, name, true) == 0)
				.Where(current => current.ProjectControllerId == projectControllerId)
				.FirstOrDefault();

			return (oProjectAction);
		}

		public Models.ProjectAction GetByProjectControllerIdAndNameExceptId
			(System.Guid projectControllerId, string name, System.Guid id)
		{
			Models.ProjectAction oProjectAction =
				Get()

				.Where(current => current.Id != id)
				.Where(current => string.Compare(current.Name, name, true) == 0)
				.Where(current => current.ProjectControllerId == projectControllerId)

				.FirstOrDefault();

			return (oProjectAction);
		}

		public System.Linq.IOrderedQueryable<Models.ProjectAction>
			GetByProjectControllerIdAndRole
			(System.Guid projectControllerId, Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Programmer)
			{
				var varProjectActions =
					Get()

					.Where(current => current.ProjectControllerId == projectControllerId)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectActions);
			}
			else
			{
				var varProjectActions =
					Get()

					.Where(current => current.IsVisibleJustForProgrammer == false)
					.Where(current => current.ProjectControllerId == projectControllerId)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectActions);
			}
		}

		public System.Linq.IOrderedEnumerable<Models.ProjectAction>
			GetByProjectControllerAndRole
			(Models.ProjectController projectController, Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Programmer)
			{
				var varProjectActions =
					projectController.ProjectActions

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectActions);
			}
			else
			{
				var varProjectActions =
					projectController.ProjectActions

					.Where(current => current.IsVisibleJustForProgrammer == false)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectActions);
			}
		}

		public System.Linq.IOrderedEnumerable<Models.ProjectAction>
			GetSpecialByProjectControllerAndRole
			(Models.ProjectController projectController, Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Programmer)
			{
				var varProjectActions =
					projectController.ProjectActions

					.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
						Models.Enums.AccessTypes.Special)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectActions);
			}
			else
			{
				var varProjectActions =
					projectController.ProjectActions

					.Where(current => current.IsVisibleJustForProgrammer == false)
					.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
						Models.Enums.AccessTypes.Special)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectActions);
			}
		}
	}
}
