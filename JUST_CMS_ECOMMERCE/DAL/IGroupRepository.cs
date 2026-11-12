namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/10/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IGroupRepository : IRepository<Models.Group>
	{
		System.Linq.IQueryable<ViewModels.Areas.Administrator.Group.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid);

		Models.Group
			GetByCultureLcidAndName(int cultureLcid, string name);

		Models.Group
			GetByCultureLcidAndNameExceptId(int cultureLcid, string name, System.Guid id);

		int GetPageCount(System.Guid id);

		int GetPageCount(Models.Group group);

		int GetUserCount(System.Guid id);

		int GetUserCount(Models.Group group);

		int GetProjectActionCount(System.Guid id);

		int GetProjectActionCount(Models.Group group);










		System.Linq.IOrderedQueryable<Models.Group> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Group> GetActiveByCultureLcid(int cultureLcid);

		int GetActiveGroupCountByUserIdAndActionId(System.Guid userId, System.Guid actionId);
	}
}
