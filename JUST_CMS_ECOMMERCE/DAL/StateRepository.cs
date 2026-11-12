using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class StateRepository : Repository<Models.State>, IStateRepository
	{
		public StateRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IOrderedQueryable<Models.State> GetByCountryId(System.Guid countryId)
		{
			var varEntities =
				Get()

				.Where(current => current.CountryId == countryId)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.State> GetActiveByCountryId(System.Guid countryId)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CountryId == countryId)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public Models.State GetByCountryIdAndName(System.Guid countryId, string name)
		{
			var varEntity =
				Get()

				.Where(current => current.Name == name)
				.Where(current => current.CountryId == countryId)

				.FirstOrDefault();

			return (varEntity);
		}

		public Models.State
			GetByCountryIdAndNameExceptId(System.Guid countryId, string name, System.Guid id)
		{
			var varEntity =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CountryId == countryId)
				.FirstOrDefault();

			return (varEntity);
		}

		public Models.State
			GetByCountryIdAndMasterDataCode(System.Guid countryId, long masterDataCode)
		{
			var varEntity =
				Get()
				.Where(current => current.CountryId == countryId)
				.Where(current => current.MasterDataCode == masterDataCode)
				.FirstOrDefault();

			return (varEntity);
		}

		public Models.State
			GetByCountryIdAndMasterDataCodeExceptId(System.Guid countryId, long masterDataCode, System.Guid id)
		{
			var varEntity =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.CountryId == countryId)
				.Where(current => current.MasterDataCode == masterDataCode)
				.FirstOrDefault();

			return (varEntity);
		}
	}
}
