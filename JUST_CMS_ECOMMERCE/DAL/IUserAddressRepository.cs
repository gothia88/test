namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/12/09
	/// Changed In Version: 1.8.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IUserAddressRepository : IRepository<Models.UserAddress>
	{
		System.Linq.IQueryable<Models.UserAddress> GetByUserId(System.Guid userId);
	}
}
