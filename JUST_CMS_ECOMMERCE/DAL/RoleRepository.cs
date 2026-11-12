using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class RoleRepository : Repository<Models.Role>, IRoleRepository
	{
		public RoleRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Role GetByCultureLcidAndCode(int cultureLcid, int code)
		{
			Models.Role oRole = Get()
				.Where(current => current.Code == code)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (oRole);
		}

		public IOrderedQueryable<Models.Role>
			GetByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code)
		{
			var varEntities =
				Get()

				.Where(current => current.Code <= code)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Code)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Role>
			GetActiveByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.Code <= code)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Code)
				;

			return (varEntities);
		}



		/// <summary>
		/// Version: 1.0.7
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public System.Collections.Generic.List<Models.Role>
			GetForDropDownList(int cultureLcid, bool forSiteResponsibles, Models.Enums.Roles code)
		{
			if (forSiteResponsibles)
			{
				var varEntities =
					Get()

					.Where(current => current.Code <= (int)code)
					.Where(current => current.CultureLcid == cultureLcid)

					.OrderBy(current => current.IsDeleted)
					.ThenByDescending(current => current.IsActive)
					.ThenBy(current => current.Ordering)
					.ThenBy(current => current.Code)

					.ToList();
				;

				return (varEntities);
			}
			else
			{
				var varEntities =
					Get()

					.Where(current => current.IsActive)
					.Where(current => current.IsDeleted == false)

					.Where(current => current.Code <= (int)code)
					.Where(current => current.CultureLcid == cultureLcid)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.Code)

					.ToList();
				;

				return (varEntities);
			}
		}
	}
}
