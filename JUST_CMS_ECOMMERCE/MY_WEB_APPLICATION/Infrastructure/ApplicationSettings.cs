namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/10/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class ApplicationSettings
	{
		static ApplicationSettings()
		{
		}

		private static Models.ApplicationSettings _instance;
		public static Models.ApplicationSettings Instance
		{
			get
			{
				// دستورات ذيل برای چندزبانگی کار نمی‌کند
				//if (_instance == null)
				//{
				//	GetInstanceByCultureLcid();
				//}
				//return (_instance);

				GetInstanceByCultureLcid();

				return (_instance);
			}
		}

		private static void GetInstanceByCultureLcid()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				int intCultureLcid =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				Models.ApplicationSettings oApplicationSettings =
					oUnitOfWork.ApplicationSettingsRepository
					.GetByCultureLcid(intCultureLcid);

				if (oApplicationSettings == null)
				{
					oApplicationSettings =
						new Models.ApplicationSettings();
				}

				_instance = oApplicationSettings;
			}
			catch //(System.Exception ex)
			{
				_instance =
					new Models.ApplicationSettings();
			}
			finally
			{
				if (oUnitOfWork != null)
				{
					oUnitOfWork.Dispose();
					oUnitOfWork = null;
				}
			}
		}
	}
}
