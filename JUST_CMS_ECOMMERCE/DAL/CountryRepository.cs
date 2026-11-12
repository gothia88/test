using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CountryRepository : Repository<Models.Country>, ICountryRepository
	{
		public CountryRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IOrderedQueryable<Models.Country> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.Country> GetActiveByCultureLcid(int cultureLcid)
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

		public Models.Country GetByCultureLcidAndName(int cultureLcid, string name)
		{
			var varEntity =
				Get()
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (varEntity);
		}

		public Models.Country
			GetByCultureLcidAndNameExceptId(int cultureLcid, string name, System.Guid id)
		{
			var varEntity =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (varEntity);
		}

		public Models.Country
			GetByCultureLcidAndMasterDataCode(int cultureLcid, long masterDataCode)
		{
			var varEntity =
				Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.MasterDataCode == masterDataCode)
				.FirstOrDefault();

			return (varEntity);
		}

		public Models.Country
			GetByCultureLcidAndMasterDataCodeExceptId(int cultureLcid, long masterDataCode, System.Guid id)
		{
			var varEntity =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.MasterDataCode == masterDataCode)
				.FirstOrDefault();

			return (varEntity);
		}
	}
}
