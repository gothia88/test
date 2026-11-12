namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/10/03
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IPostCategoryRepository : IRepository<Models.Cms.PostCategory>
	{
		Models.Cms.PostCategory
			GetByCultureLcidAndParentIdAndName
			(int cultureLcid, System.Guid? parentId, string name);

		Models.Cms.PostCategory
			GetByCultureLcidAndParentIdAndNameExceptId
			(int cultureLcid, System.Guid? parentId, string name, System.Guid id);

		System.Linq.IOrderedQueryable<Models.Cms.PostCategory>
			GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Cms.PostCategory>
			GetActiveLeavesByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Cms.PostCategory>
			GetActiveByCultureLcid(int cultureLcid);

		System.Linq.IQueryable<ViewModels.Areas.Cms.PostCategory.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid, System.Guid? parentId);
	}
}
