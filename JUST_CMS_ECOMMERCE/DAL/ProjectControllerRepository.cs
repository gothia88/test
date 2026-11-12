using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/04/24
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectControllerRepository :
		Repository<Models.ProjectController>, IProjectControllerRepository
	{
		public ProjectControllerRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.ProjectController GetByProjectAreaIdAndName
			(System.Guid projectAreaId, string name)
		{
			Models.ProjectController oProjectController =
				Get(current => (current.ProjectAreaId == projectAreaId) &&
					(current.Name == name))
				.FirstOrDefault();

			return (oProjectController);
		}

		public Models.ProjectController GetByProjectAreaIdAndNameExceptId
			(System.Guid projectAreaId, string name, System.Guid id)
		{
			Models.ProjectController oProjectController =
				Get(current => (current.ProjectAreaId == projectAreaId) &&
					(current.Name == name) && (current.Id != id))
				.FirstOrDefault();

			return (oProjectController);
		}

		public System.Linq.IOrderedEnumerable<Models.ProjectController>
			GetByProjectAreaAndRole(Models.ProjectArea projectArea, Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Administrator)
			{
				var varProjectControllers =
					projectArea.ProjectControllers

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectControllers);
			}
			else
			{
				var varProjectControllers =
					projectArea.ProjectControllers

					.Where(current => current.IsVisibleJustForProgrammer == false)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varProjectControllers);
			}
		}

		public System.Linq.IQueryable<ViewModels.Areas.Administrator.ProjectController.IndexViewModel>
			GetViewModelByRole(System.Guid projectAreaId, Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Programmer)
			{
				var varEntities =
					Get()

					.Where(current => current.ProjectAreaId == projectAreaId)

					.OrderBy(current => current.Ordering)
					// برای برنامه نويس، گزينه ذيل بهتر است
					.ThenBy(current => current.Name)
					//.ThenBy(current => current.DisplayName)

					.Select(current => new ViewModels.Areas.Administrator.ProjectController.IndexViewModel
					{
						Id = current.Id,
						Name = current.Name,
						IsActive = current.IsActive,
						Ordering = current.Ordering,
						DisplayName = current.DisplayName,
						InsertDateTime = current.InsertDateTime,
						UpdateDateTime = current.UpdateDateTime,
						ProjectActionCount = current.ProjectActions.Count(),
						IsVisibleJustForProgrammer = current.IsVisibleJustForProgrammer
					})
					;

				return (varEntities);
			}
			else
			{
				var varEntities =
					Get()

					.Where(current => current.ProjectAreaId == projectAreaId)
					.Where(current => current.IsVisibleJustForProgrammer == false)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)

					.Select(current => new ViewModels.Areas.Administrator.ProjectController.IndexViewModel
					{
						Id = current.Id,
						Name = current.Name,
						IsActive = current.IsActive,
						Ordering = current.Ordering,
						DisplayName = current.DisplayName,
						InsertDateTime = current.InsertDateTime,
						UpdateDateTime = current.UpdateDateTime,
						ProjectActionCount = current.ProjectActions.Count(),
						IsVisibleJustForProgrammer = current.IsVisibleJustForProgrammer

					})
					;

				return (varEntities);
			}
		}
	}
}
