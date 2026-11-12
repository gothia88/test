namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1392/12/10
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ILayoutRepository : IRepository<Models.Cms.Layout>
	{
		Models.Cms.Layout GetByCultureLcidAndName(int cultureLcid, string name);

		Models.Cms.Layout GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id);

		System.Linq.IOrderedQueryable<Models.Cms.Layout> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Cms.Layout> GetActiveByCultureLcid(int cultureLcid);
	}
}
