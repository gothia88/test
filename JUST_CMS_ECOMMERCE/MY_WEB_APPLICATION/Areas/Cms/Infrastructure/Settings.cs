namespace Infrastructure.Cms
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/09/15
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Settings
	{
		static Settings()
		{
		}

		private static Models.Cms.CmsSettings _instance;
		public static Models.Cms.CmsSettings Instance
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

				Models.Cms.CmsSettings oSettings =
					oUnitOfWork.CmsUnitOfWork.SettingsRepository
					.GetByCultureLcid(intCultureLcid);

				if (oSettings == null)
				{
					oSettings =
						new Models.Cms.CmsSettings();
				}

				_instance = oSettings;
			}
			catch //(System.Exception ex)
			{
				_instance =
					new Models.Cms.CmsSettings();
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
