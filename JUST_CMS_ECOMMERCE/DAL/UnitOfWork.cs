namespace DAL
{
	/// <summary>
	/// Version: 1.3.4
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UnitOfWork : System.Object, IUnitOfWork
	{
		public UnitOfWork()
		{
			IsDisposed = false;
		}

		protected bool IsDisposed { get; set; }

		private Models.DatabaseContext _databaseContext;
		protected virtual Models.DatabaseContext DatabaseContext
		{
			get
			{
				if (_databaseContext == null)
				{
					_databaseContext =
						new Models.DatabaseContext();
				}
				return (_databaseContext);
			}
		}

		public object GetFieldValueByCurrentCulture(Models.BaseEntity entity, string propertyName)
		{
			if (entity == null)
			{
				return (null);
			}

			string strPropertyName = propertyName;

			// مثلا
			// با توجه به مدل نامگذاری فيلدهای چند زبانه خودتون
			if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "fa-IR")
			{
				strPropertyName = "FA_" + strPropertyName;
			}

			object objResult =
				DatabaseContext.Entry(entity).Member(strPropertyName).CurrentValue;

			return (objResult);
		}

		private Cms.IUnitOfWork _cmsUnitOfWork;
		public Cms.IUnitOfWork CmsUnitOfWork
		{
			get
			{
				if (_cmsUnitOfWork == null)
				{
					_cmsUnitOfWork =
						new Cms.UnitOfWork(DatabaseContext);
				}

				return (_cmsUnitOfWork);
			}
		}

		private ECommerce.IUnitOfWork _eCommerceUnitOfWork;
		public ECommerce.IUnitOfWork ECommerceUnitOfWork
		{
			get
			{
				if (_eCommerceUnitOfWork == null)
				{
					_eCommerceUnitOfWork =
						new ECommerce.UnitOfWork(DatabaseContext);
				}

				return (_eCommerceUnitOfWork);
			}
		}

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

		private ICityRepository _cityRepository;
		public ICityRepository CityRepository
		{
			get
			{
				if (_cityRepository == null)
				{
					_cityRepository =
						new CityRepository(DatabaseContext);
				}
				return (_cityRepository);
			}
		}

		private IRoleRepository _roleRepository;
		public IRoleRepository RoleRepository
		{
			get
			{
				if (_roleRepository == null)
				{
					_roleRepository =
						new RoleRepository(DatabaseContext);
				}
				return (_roleRepository);
			}
		}

		private IUserRepository _userRepository;
		public IUserRepository UserRepository
		{
			get
			{
				if (_userRepository == null)
				{
					_userRepository =
						new UserRepository(DatabaseContext);
				}
				return (_userRepository);
			}
		}

		private IGroupRepository _groupRepository;
		public IGroupRepository GroupRepository
		{
			get
			{
				if (_groupRepository == null)
				{
					_groupRepository =
						new GroupRepository(DatabaseContext);
				}
				return (_groupRepository);
			}
		}

		private IStateRepository _stateRepository;
		public IStateRepository StateRepository
		{
			get
			{
				if (_stateRepository == null)
				{
					_stateRepository =
						new StateRepository(DatabaseContext);
				}
				return (_stateRepository);
			}
		}

		private IGenderRepository _genderRepository;
		public IGenderRepository GenderRepository
		{
			get
			{
				if (_genderRepository == null)
				{
					_genderRepository =
						new GenderRepository(DatabaseContext);
				}
				return (_genderRepository);
			}
		}

		private ICountryRepository _countryRepository;
		public ICountryRepository CountryRepository
		{
			get
			{
				if (_countryRepository == null)
				{
					_countryRepository =
						new CountryRepository(DatabaseContext);
				}
				return (_countryRepository);
			}
		}

		private ICultureRepository _cultureRepository;
		public ICultureRepository CultureRepository
		{
			get
			{
				if (_cultureRepository == null)
				{
					_cultureRepository =
						new CultureRepository(DatabaseContext);
				}
				return (_cultureRepository);
			}
		}

		private IAccessTypeRepository _accessTypeRepository;
		public IAccessTypeRepository AccessTypeRepository
		{
			get
			{
				if (_accessTypeRepository == null)
				{
					_accessTypeRepository =
						new AccessTypeRepository(DatabaseContext);
				}
				return (_accessTypeRepository);
			}
		}

		private IHttpRefererRepository _httpRefererRepository;
		public IHttpRefererRepository HttpRefererRepository
		{
			get
			{
				if (_httpRefererRepository == null)
				{
					_httpRefererRepository =
						new HttpRefererRepository(DatabaseContext);
				}
				return (_httpRefererRepository);
			}
		}

		private IProjectAreaRepository _projectAreaRepository;
		public IProjectAreaRepository ProjectAreaRepository
		{
			get
			{
				if (_projectAreaRepository == null)
				{
					_projectAreaRepository =
						new ProjectAreaRepository(DatabaseContext);
				}
				return (_projectAreaRepository);
			}
		}

		private IUserAddressRepository _userAddressRepository;
		public IUserAddressRepository UserAddressRepository
		{
			get
			{
				if (_userAddressRepository == null)
				{
					_userAddressRepository =
						new UserAddressRepository(DatabaseContext);
				}
				return (_userAddressRepository);
			}
		}

		private IMailSettingsRepository _mailSettingsRepository;
		public IMailSettingsRepository MailSettingsRepository
		{
			get
			{
				if (_mailSettingsRepository == null)
				{
					_mailSettingsRepository =
						new MailSettingsRepository(DatabaseContext);
				}
				return (_mailSettingsRepository);
			}
		}

		private IUserLoginLogRepository _userLoginLogRepository;
		public IUserLoginLogRepository UserLoginLogRepository
		{
			get
			{
				if (_userLoginLogRepository == null)
				{
					_userLoginLogRepository =
						new UserLoginLogRepository(DatabaseContext);
				}
				return (_userLoginLogRepository);
			}
		}

		private IProjectActionRepository _projectActionRepository;
		public IProjectActionRepository ProjectActionRepository
		{
			get
			{
				if (_projectActionRepository == null)
				{
					_projectActionRepository =
						new ProjectActionRepository(DatabaseContext);
				}
				return (_projectActionRepository);
			}
		}

		private IProjectControllerRepository _projectControllerRepository;
		public IProjectControllerRepository ProjectControllerRepository
		{
			get
			{
				if (_projectControllerRepository == null)
				{
					_projectControllerRepository =
						new ProjectControllerRepository(DatabaseContext);
				}
				return (_projectControllerRepository);
			}
		}

		private IApplicationSettingsRepository _applicationSettingsRepository;
		public IApplicationSettingsRepository ApplicationSettingsRepository
		{
			get
			{
				if (_applicationSettingsRepository == null)
				{
					_applicationSettingsRepository =
						new ApplicationSettingsRepository(DatabaseContext);
				}
				return (_applicationSettingsRepository);
			}
		}

		private IGlobalApplicationSettingsRepository _globalApplicationSettingsRepository;
		public IGlobalApplicationSettingsRepository GlobalApplicationSettingsRepository
		{
			get
			{
				if (_globalApplicationSettingsRepository == null)
				{
					_globalApplicationSettingsRepository =
						new GlobalApplicationSettingsRepository(DatabaseContext);
				}
				return (_globalApplicationSettingsRepository);
			}
		}

		public void Save()
		{
			try
			{
				DatabaseContext.SaveChanges();
			}
			catch (System.Exception ex)
			{
				Dtx.LogHandler.Report(GetType(), null, ex);

				throw;
			}
		}

		public virtual int ExecuteSqlCommand(string commandText)
		{
			int intResult = -1;

			using (System.Transactions.TransactionScope oTransactionScope =
				new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Suppress))
			{
				intResult = DatabaseContext.Database.ExecuteSqlCommand
					(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, commandText);
			}

			return (intResult);
		}

		public virtual int BackupDatabase
			(ViewModels.Areas.Administrator.Programmer.BackupDatabaseViewModel viewModel, string pathName)
		{
			string strDatabaseName =
				DatabaseContext.Database.Connection.Database;

			string strCommandText =
				string.Format("BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH", strDatabaseName, pathName);

			if (viewModel.INIT)
			{
				strCommandText += " " + "INIT";
			}
			else
			{
				strCommandText += " " + "NOINIT";
			}

			if (viewModel.SKIP)
			{
				strCommandText += ", " + "SKIP";
			}
			else
			{
				strCommandText += ", " + "NOSKIP";
			}

			if (viewModel.FORMAT)
			{
				strCommandText += ", " + "FORMAT";
			}
			else
			{
				strCommandText += ", " + "NOFORMAT";
			}

			if (viewModel.CHECKSUM)
			{
				strCommandText += ", " + "CHECKSUM";
			}
			else
			{
				strCommandText += ", " + "NO-CHECKSUM";
			}

			if (viewModel.CONTINUE_AFTER_ERROR)
			{
				strCommandText += ", " + "CONTINUE_AFTER_ERROR";
			}
			else
			{
				strCommandText += ", " + "STOP_ON_ERROR";
			}

			if (string.IsNullOrWhiteSpace(viewModel.NAME) == false)
			{
				strCommandText += ", NAME = N'" + viewModel.NAME + "'";
			}

			if (string.IsNullOrWhiteSpace(viewModel.MEDIANAME) == false)
			{
				strCommandText += ", MEDIANAME = N'" + viewModel.MEDIANAME + "'";
			}

			if (string.IsNullOrWhiteSpace(viewModel.MEDIADESCRIPTION) == false)
			{
				strCommandText += ", MEDIADESCRIPTION = N'" + viewModel.MEDIADESCRIPTION + "'";
			}

			int intResult = ExecuteSqlCommand(strCommandText);

			return (intResult);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed == false)
			{
				if (disposing)
				{
					_databaseContext.Dispose();
				}
			}
			IsDisposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}
	}
}
