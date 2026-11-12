namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.2.2
	/// Update Date: 1394/03/09
	/// Changed In Version: 1.9.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IPostRepository : IRepository<Models.Cms.Post>
	{
		System.Linq.IQueryable<ViewModels.Areas.Common.Rss.IndexViewModel>
			GetActiveForRss(int take);

		System.Linq.IQueryable<ViewModels.Areas.Cms.Post.GetDataViewModel>
			GetByCultureLcid(int cultureLcid);

		System.Linq.IQueryable<ViewModels.Areas.Cms.Shared.DisplayPostsViewModel>
			GetActiveByCultureLcid(int cultureLcid, int skip, int take);

		System.Linq.IQueryable<ViewModels.Areas.Cms.User.GetPostsViewModel>
			GetByCultureLcidAndCreatorUserId(int cultureLcid, System.Guid creatorUserId);

		System.Linq.IQueryable<ViewModels.Areas.Cms.Shared.DisplayPostsViewModel>
			GetActiveByCultureLcidAndPostCategoryId(int cultureLcid, System.Guid postCategoryId, int skip, int take);



		System.Linq.IQueryable<ViewModels.Areas.Cms.Shared.GetActiveUserPostsViewModel>
			GetActiveUserPosts(int cultureLcid, System.Guid userId);

		/// <summary>
		/// Version: 1.2.2
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		System.Collections.Generic.IList<Models.Cms.Post> Search(int cultureLcid, string keywords, int take);
	}
}
