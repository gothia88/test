using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/09/15
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class SettingsRepository :
		Repository<Models.Cms.CmsSettings>, ISettingsRepository
	{
		public SettingsRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Cms.CmsSettings GetByCultureLcid(int cultureLcid)
		{
			Models.Cms.CmsSettings oSettings =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.FirstOrDefault();

			return (oSettings);
		}
	}
}
