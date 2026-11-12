using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/25
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class HttpRefererRepository : Repository<Models.HttpReferer>, IHttpRefererRepository
	{
		public HttpRefererRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IOrderedQueryable<Models.HttpReferer> GetPublic()
		{
			var varResult =
				Get()

				.Where(current => current.IsPublic)

				.OrderByDescending(current => current.Hits)
				.ThenByDescending(current => current.LastHitDateTime.Value)
				;

			return (varResult);
		}
	}
}
