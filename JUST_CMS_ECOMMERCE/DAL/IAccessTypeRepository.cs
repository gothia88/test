namespace DAL
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/12/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IAccessTypeRepository : IRepository<Models.AccessType>
	{
		System.Linq.IOrderedQueryable<Models.AccessType> GetByCultureLcid(int cultureLcid);
		System.Linq.IOrderedQueryable<Models.AccessType> GetActiveByCultureLcid(int cultureLcid);
	}
}
