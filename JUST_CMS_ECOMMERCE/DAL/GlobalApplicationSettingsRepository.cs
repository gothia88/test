using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/01/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GlobalApplicationSettingsRepository :
		Repository<Models.GlobalApplicationSettings>, IGlobalApplicationSettingsRepository
	{
		public GlobalApplicationSettingsRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}
	}
}
