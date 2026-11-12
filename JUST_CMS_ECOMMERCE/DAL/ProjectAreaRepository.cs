using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1392/12/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectAreaRepository : Repository<Models.ProjectArea>, IProjectAreaRepository
	{
		public ProjectAreaRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.ProjectArea GetByName(string name)
		{
			Models.ProjectArea oProjectArea =
				Get(current => current.Name == name)
				.FirstOrDefault();

			return (oProjectArea);
		}

		public Models.ProjectArea GetByNameExceptId(string name, System.Guid id)
		{
			Models.ProjectArea oProjectArea =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.FirstOrDefault();

			return (oProjectArea);
		}

		public IOrderedQueryable<Models.ProjectArea> GetByRole(Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Programmer)
			{
				var varEntities =
					Get()

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varEntities);
			}
			else
			{
				var varEntities =
					Get()

					.Where(current => current.IsVisibleJustForProgrammer == false)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)
					;

				return (varEntities);
			}
		}

		public System.Linq.IQueryable<ViewModels.Areas.Administrator.ProjectArea.IndexViewModel>
			GetViewModelByRole(Models.Enums.Roles role)
		{
			if (role == Models.Enums.Roles.Programmer)
			{
				var varEntities =
					Get()

					.OrderBy(current => current.Ordering)
					// برای برنامه نويس، گزينه ذيل بهتر است
					.ThenBy(current => current.Name)
					//.ThenBy(current => current.DisplayName)

					.Select(current => new ViewModels.Areas.Administrator.ProjectArea.IndexViewModel
					{
						Id = current.Id,

						IsActive = current.IsActive,

						Name = current.Name,
						DisplayName = current.DisplayName,

						InsertDateTime = current.InsertDateTime,
						UpdateDateTime = current.UpdateDateTime,

						Ordering = current.Ordering,
						ProjectControllerCount = current.ProjectControllers.Count(),

						IsVisibleJustForProgrammer = current.IsVisibleJustForProgrammer
					})
					;

				return (varEntities);
			}
			else
			{
				var varEntities =
					Get()

					.Where(current => current.IsVisibleJustForProgrammer == false)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.DisplayName)

					.Select(current => new ViewModels.Areas.Administrator.ProjectArea.IndexViewModel
					{
						Id = current.Id,
						Name = current.Name,
						IsActive = current.IsActive,
						Ordering = current.Ordering,
						DisplayName = current.DisplayName,
						InsertDateTime = current.InsertDateTime,
						UpdateDateTime = current.UpdateDateTime,
						ProjectControllerCount = current.ProjectControllers.Count(),
						IsVisibleJustForProgrammer = current.IsVisibleJustForProgrammer
					})
					;

				return (varEntities);
			}
		}
	}
}
