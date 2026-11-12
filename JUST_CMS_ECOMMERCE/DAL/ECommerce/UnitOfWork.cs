namespace DAL.ECommerce
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/01/19
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UnitOfWork : System.Object, IUnitOfWork
	{
		public UnitOfWork(Models.DatabaseContext databaseContext)
		{
			if (databaseContext == null)
			{
				throw (new System.ArgumentNullException("databaseContext"));
			}

			DatabaseContext = databaseContext;
		}

		protected Models.DatabaseContext DatabaseContext { get; set; }

		//private IRepository _Repository;
		//public IRepository Repository
		//{
		//	get
		//	{
		//		if (_Repository == null)
		//		{
		//			_Repository =
		//				new Repository(DatabaseContext);
		//		}
		//		return (_Repository);
		//	}
		//}

		private IPlanRepository _planRepository;
		public IPlanRepository PlanRepository
		{
			get
			{
				if (_planRepository == null)
				{
					_planRepository =
						new PlanRepository(DatabaseContext);
				}
				return (_planRepository);
			}
		}

		private ISettingsRepository _settingsRepository;
		public ISettingsRepository SettingsRepository
		{
			get
			{
				if (_settingsRepository == null)
				{
					_settingsRepository =
						new SettingsRepository(DatabaseContext);
				}
				return (_settingsRepository);
			}
		}

		private ITransactionRepository _transactionRepository;
		public ITransactionRepository TransactionRepository
		{
			get
			{
				if (_transactionRepository == null)
				{
					_transactionRepository =
						new TransactionRepository(DatabaseContext);
				}
				return (_transactionRepository);
			}
		}
	}
}
