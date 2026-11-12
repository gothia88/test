namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/11/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IMailSettingsRepository : IRepository<Models.MailSettings>
	{
		System.Linq.IOrderedQueryable<Models.MailSettings> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.MailSettings> GetActiveByCultureLcid(int cultureLcid);
	}
}
