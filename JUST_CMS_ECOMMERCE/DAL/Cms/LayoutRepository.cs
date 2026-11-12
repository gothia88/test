using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1392/12/10
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class LayoutRepository : Repository<Models.Cms.Layout>, ILayoutRepository
	{
		public LayoutRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Cms.Layout GetByCultureLcidAndName(int cultureLcid, string name)
		{
			Models.Cms.Layout oLayout =
				Get(current => (current.Name == name) &&
					(current.CultureLcid == cultureLcid))
				.FirstOrDefault();

			return (oLayout);
		}

		public Models.Cms.Layout GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id)
		{
			Models.Cms.Layout oLayout =
				Get(current =>
					(current.Id != id) &&
					(current.Name == name) &&
					(current.CultureLcid == cultureLcid))
				.FirstOrDefault();

			return (oLayout);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.Layout> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderByDescending(current => current.IsSystem)
				.ThenBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Cms.Layout> GetActiveByCultureLcid(int cultureLcid)
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
	}
}
