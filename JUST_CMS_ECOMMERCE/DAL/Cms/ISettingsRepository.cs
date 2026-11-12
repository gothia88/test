using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/09/05
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ISettingsRepository : IRepository<Models.Cms.CmsSettings>
	{
		Models.Cms.CmsSettings GetByCultureLcid(int cultureLcid);
	}
}
