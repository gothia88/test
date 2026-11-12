namespace Infrastructure.ECommerce
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/01/19
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Settings
	{
		static Settings()
		{
		}

		private static Models.ECommerce.ECommerceSettings _instance;
		public static Models.ECommerce.ECommerceSettings Instance
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

				Models.ECommerce.ECommerceSettings oSettings =
					oUnitOfWork.ECommerceUnitOfWork.SettingsRepository
					.GetByCultureLcid(intCultureLcid);

				if (oSettings == null)
				{
					oSettings =
						new Models.ECommerce.ECommerceSettings();
				}

				_instance = oSettings;
			}
			catch //(System.Exception ex)
			{
				_instance =
					new Models.ECommerce.ECommerceSettings();
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
