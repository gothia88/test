namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ITransactionRepository : IRepository<Models.ECommerce.Transaction>
	{
		System.Linq.IQueryable<ViewModels.Areas.ECommerce.Transaction.GetDataForAdminViewModel>
			GetDataForAdmin();

		System.Linq.IQueryable<ViewModels.Areas.ECommerce.Transaction.GetDataForUserViewModel>
			GetDataForUserByUserId(System.Guid userId);
	}
}
