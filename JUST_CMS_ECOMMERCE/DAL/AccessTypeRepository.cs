using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/12/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AccessTypeRepository : Repository<Models.AccessType>, IAccessTypeRepository
	{
		public AccessTypeRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IOrderedQueryable<Models.AccessType> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Code)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.AccessType> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Code)
				;

			return (varEntities);
		}
	}
}
