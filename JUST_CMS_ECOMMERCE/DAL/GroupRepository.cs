using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/10/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GroupRepository : Repository<Models.Group>, IGroupRepository
	{
		public GroupRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IQueryable<ViewModels.Areas.Administrator.Group.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)

				.Select(current => new ViewModels.Areas.Administrator.Group.IndexViewModel
				{
					Id = current.Id,

					Name = current.Name,

					IsActive = current.IsActive,
					IsSystem = current.IsSystem,
					IsDeleted = current.IsDeleted,
					IsVerified = current.IsVerified,

					Ordering = current.Ordering,

					UserCount = current.Users.Count(),
					PageCount = current.Pages.Count(),
					ProjectActionCount = current.ProjectActions.Count(),

					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime,
				})
				;

			return (varEntities);
		}

		public Models.Group
			GetByCultureLcidAndName(int cultureLcid, string name)
		{
			var varEntity =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => string.Compare(current.Name, name, true) == 0)

				.FirstOrDefault();

			return (varEntity);
		}

		public Models.Group
			GetByCultureLcidAndNameExceptId(int cultureLcid, string name, System.Guid id)
		{
			var varEntity =
				Get()

				.Where(current => current.Id != id)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => string.Compare(current.Name, name, true) == 0)

				.FirstOrDefault();

			return (varEntity);
		}

		public int GetPageCount(System.Guid id)
		{
			Models.Group oGroup = GetById(id);

			return (GetPageCount(oGroup));
		}

		public int GetPageCount(Models.Group group)
		{
			if (group == null)
			{
				return (0);
			}

			int intResult =
				DatabaseContext.Entry(group)
				.Collection(current => current.Pages)
				.Query()
				.Count();

			return (intResult);
		}

		public int GetUserCount(System.Guid id)
		{
			Models.Group oGroup = GetById(id);

			return (GetUserCount(oGroup));
		}

		public int GetUserCount(Models.Group group)
		{
			if (group == null)
			{
				return (0);
			}

			int intResult =
				DatabaseContext.Entry(group)
				.Collection(current => current.Users)
				.Query()
				.Count();

			return (intResult);
		}

		public int GetProjectActionCount(System.Guid id)
		{
			Models.Group oGroup = GetById(id);

			return (GetProjectActionCount(oGroup));
		}

		public int GetProjectActionCount(Models.Group group)
		{
			if (group == null)
			{
				return (0);
			}

			int intResult =
				DatabaseContext.Entry(group)
				.Collection(current => current.ProjectActions)
				.Query()
				.Count();

			return (intResult);
		}










		public IOrderedQueryable<Models.Group> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Group> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public int GetActiveGroupCountByUserIdAndActionId(System.Guid userId, System.Guid actionId)
		{
			int intGroupCount =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.Users.Any(user => user.Id == userId))
				.Where(current => current.ProjectActions.Any(action => action.Id == actionId))

				.Count()
				;

			return (intGroupCount);
		}
	}
}
