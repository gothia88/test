using System.Linq;
using System.Data.Entity;

namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PlanRepository : Repository<Models.ECommerce.Plan>, IPlanRepository
	{
		public PlanRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IQueryable<ViewModels.Areas.ECommerce.Plan.IndexViewModel>
			GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)

				.Select(current => new ViewModels.Areas.ECommerce.Plan.IndexViewModel
				{
					Id = current.Id,

					Name = current.Name,

					Ordering = current.Ordering,

					IsActive = current.IsActive,
					IsSystem = current.IsSystem,
					IsDeleted = current.IsDeleted,
					IsVerified = current.IsVerified,

					Price = current.Price,
					DurationInMonth = current.DurationInMonth,

					UserCount = current.Users.Count(),
				})
				;

			return (varEntities);

		}

		public System.Linq.IOrderedQueryable<Models.ECommerce.Plan>
			GetActiveByCultureLcid(int cultureLcid)
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

		public System.Linq.IOrderedQueryable<Models.ECommerce.Plan>
			GetActiveForDropDownListByCultureLcid(int cultureLcid)
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

		public System.Linq.IOrderedQueryable<Models.ECommerce.Plan>
			GetForDropDownListByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}



		/// <summary>
		/// Version: 1.0.6
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public System.Collections.Generic.List<Models.ECommerce.Plan>
			GetForDropDownList(int cultureLcid, bool forSiteResponsibles)
		{
			if (forSiteResponsibles)
			{
				var varEntities =
					Get()

					.Where(current => current.CultureLcid == cultureLcid)

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
					.Where(current => current.CultureLcid == cultureLcid)

					.OrderBy(current => current.Ordering)
					.ThenBy(current => current.Name)

					.ToList();
				;

				return (varEntities);
			}
		}
	}
}
