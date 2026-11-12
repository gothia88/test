using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/12/09
	/// Changed In Version: 1.8.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UserAddressRepository : Repository<Models.UserAddress>, IUserAddressRepository
	{
		public UserAddressRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IQueryable<Models.UserAddress> GetByUserId(System.Guid userId)
		{
			var varEntities =
				Get()
				.Where(current => current.UserId == userId)
				;

			return (varEntities);
		}
	}
}
