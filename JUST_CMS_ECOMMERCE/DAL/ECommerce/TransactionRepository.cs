using System.Linq;
using System.Data.Entity;

namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class TransactionRepository : Repository<Models.ECommerce.Transaction>, ITransactionRepository
	{
		public TransactionRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IQueryable<ViewModels.Areas.ECommerce.Transaction.GetDataForAdminViewModel>
			GetDataForAdmin()
		{
			var varEntities =
				Get()

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Select(current => new ViewModels.Areas.ECommerce.Transaction.GetDataForAdminViewModel
				{
					Id = current.Id,

					PlanId = current.PlanId,
					PlanName = current.Plan.Name,

					Debit = current.Debit,
					Credit = current.Credit,

					SiteDebit = current.SiteDebit,
					SiteCredit = current.SiteCredit,

					UserId = current.UserId,
					UserLastName = current.User.LastName,
					UserGender = current.User.Gender.Name2,
					UserFirstName = current.User.FirstName,

					IsPending = current.IsPending,
					IsPaidOnline = current.IsPaidOnline,

					Description = current.Description,

					UpdateDateTime = current.UpdateDateTime,

					MerchantIdentityGet = current.MerchantIdentityGet,
					MerchantTransactionIdentity = current.MerchantTransactionIdentity,
				})
				;

			return (varEntities);
		}

		public System.Linq.IQueryable<ViewModels.Areas.ECommerce.Transaction.GetDataForUserViewModel>
			GetDataForUserByUserId(System.Guid userId)
		{
			var varEntities =
				Get()

				.Where(current => current.UserId == userId)

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Select(current => new ViewModels.Areas.ECommerce.Transaction.GetDataForUserViewModel
				{
					Id = current.Id,

					PlanId = current.PlanId,
					PlanName = current.Plan.Name,

					Debit = current.Debit,
					Credit = current.Credit,

					SiteDebit = current.SiteDebit,
					SiteCredit = current.SiteCredit,

					IsPending = current.IsPending,
					IsPaidOnline = current.IsPaidOnline,

					Description = current.Description,

					UpdateDateTime = current.UpdateDateTime,

					MerchantIdentityGet = current.MerchantIdentityGet,
					MerchantTransactionIdentity = current.MerchantTransactionIdentity,
				})
				;

			return (varEntities);
		}
	}
}
