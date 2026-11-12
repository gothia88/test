using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/02/14
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ApplicationSettingsRepository :
		Repository<Models.ApplicationSettings>, IApplicationSettingsRepository
	{
		public ApplicationSettingsRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.ApplicationSettings GetByCultureLcid(int cultureLcid)
		{
			Models.ApplicationSettings oApplicationSettings =
				Get()
				// ذيل خيلی اهميت دارد Include دستور
				.Include(current => current.MailSettings)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (oApplicationSettings);
		}
	}
}
