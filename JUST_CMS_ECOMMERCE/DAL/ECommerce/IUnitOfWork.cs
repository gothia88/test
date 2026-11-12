namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/01/19
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IUnitOfWork
	{
		IPlanRepository PlanRepository { get; }

		ISettingsRepository SettingsRepository { get; }

		ITransactionRepository TransactionRepository { get; }
	}
}
