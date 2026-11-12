namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IPlanRepository : IRepository<Models.ECommerce.Plan>
	{
		System.Linq.IOrderedQueryable<Models.ECommerce.Plan>
			GetActiveByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.ECommerce.Plan>
			GetForDropDownListByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.ECommerce.Plan>
			GetActiveForDropDownListByCultureLcid(int cultureLcid);

		System.Linq.IQueryable<ViewModels.Areas.ECommerce.Plan.IndexViewModel>
			GetByCultureLcid(int cultureLcid);



		/// <summary>
		/// Version: 1.0.6
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		System.Collections.Generic.List<Models.ECommerce.Plan>
			GetForDropDownList(int cultureLcid, bool forSiteResponsibles);
	}
}
