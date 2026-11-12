using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CityRepository : Repository<Models.City>, ICityRepository
	{
		public CityRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IOrderedQueryable<Models.City> GetByStateId(System.Guid stateId)
		{
			var varEntities =
				Get()

				.Where(current => current.StateId == stateId)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.City> GetActiveByStateId(System.Guid stateId)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.StateId == stateId)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public Models.City GetByStateIdAndName(System.Guid stateId, string name)
		{
			var varEntity =
				Get()

				.Where(current => current.Name == name)
				.Where(current => current.StateId == stateId)

				.FirstOrDefault();

			return (varEntity);
		}

		public Models.City
			GetByStateIdAndNameExceptId(System.Guid stateId, string name, System.Guid id)
		{
			var varEntity =
				Get()

				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.StateId == stateId)

				.FirstOrDefault();

			return (varEntity);
		}

		public Models.City
			GetByStateIdAndMasterDataCode(System.Guid stateId, long masterDataCode)
		{
			var varEntity =
				Get()

				.Where(current => current.StateId == stateId)
				.Where(current => current.MasterDataCode == masterDataCode)

				.FirstOrDefault();

			return (varEntity);
		}

		public Models.City
			GetByStateIdAndMasterDataCodeExceptId(System.Guid stateId, long masterDataCode, System.Guid id)
		{
			var varEntity =
				Get()

				.Where(current => current.Id != id)
				.Where(current => current.StateId == stateId)
				.Where(current => current.MasterDataCode == masterDataCode)

				.FirstOrDefault();

			return (varEntity);
		}
	}
}
