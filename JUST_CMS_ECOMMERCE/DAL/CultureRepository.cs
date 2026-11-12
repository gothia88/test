using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CultureRepository : Repository<Models.Culture>, ICultureRepository
	{
		public CultureRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Culture GetFirst()
		{
			return (GetActive().FirstOrDefault());
		}

		public Models.Culture GetByLcid(int lcid)
		{
			Models.Culture oCulture =
				Get()
				.Where(current => current.Lcid == lcid)
				.FirstOrDefault();

			return (oCulture);
		}

		public IOrderedQueryable<Models.Culture> GetActive()
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}



		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public System.Collections.Generic.List<Models.Culture>
			GetForDropDownList(bool forSiteResponsibles)
		{
			if (forSiteResponsibles)
			{
				var varEntities =
					Get()

					.OrderBy(current => current.IsDeleted)
					.ThenByDescending(current => current.IsActive)
					.ThenBy(current => current.Ordering)
					.ThenBy(current => current.Name)

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

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.Name)

					.ToList();
				;

				return (varEntities);
			}
		}
	}
}
