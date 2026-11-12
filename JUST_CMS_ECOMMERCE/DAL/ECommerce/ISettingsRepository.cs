using System.Linq;
using System.Data.Entity;

namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/01/19
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ISettingsRepository : IRepository<Models.ECommerce.ECommerceSettings>
	{
		Models.ECommerce.ECommerceSettings GetByCultureLcid(int cultureLcid);
	}
}
