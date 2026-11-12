using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.1.1
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PageRepository : Repository<Models.Cms.Page>, IPageRepository
	{
		public PageRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Cms.Page GetByCultureLcidAndName(int cultureLcid, string name)
		{
			Models.Cms.Page oPage =
				Get()

				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)

				.FirstOrDefault();

			return (oPage);
		}

		public Models.Cms.Page GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id)
		{
			Models.Cms.Page oPage =
				Get()

				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)

				.FirstOrDefault();

			return (oPage);
		}

		public int GetPageCountByLayoutId(System.Guid layoutId)
		{
			int intPageCount =
				Get()

				.Where(current => current.LayoutId == layoutId)

				.Count()
				;

			return (intPageCount);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.Page> GetSpecial()
		{
			var varEntities =
				Get()

				.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
					Models.Enums.AccessTypes.Special)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.Page> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Include(current => current.Layout)
				.Include(current => current.AccessType)
				.Include(current => current.CreatorUser)

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}


		public System.Linq.IQueryable<ViewModels.Areas.Cms.Page.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Select(current => new ViewModels.Areas.Cms.Page.IndexViewModel
				{
					Id = current.Id,

					Name = current.Name,
					Title = current.Title,

					IsDraft = current.IsDraft,
					IsActive = current.IsActive,
					IsSystem = current.IsSystem,
					IsDeleted = current.IsDeleted,
					IsVerified = current.IsVerified,

					//UserCount = current.Users.Count(),
					GroupCount = current.Groups.Count(),

					LayoutId = current.LayoutId,
					LayoutName = current.Layout.Name,

					AccessTypeId = current.AccessTypeId,
					AccessTypeCode = current.AccessType.Code,
					DisplayAccessType = current.AccessType.Name,
				})
				;

			return (varEntities);
		}
	}
}
