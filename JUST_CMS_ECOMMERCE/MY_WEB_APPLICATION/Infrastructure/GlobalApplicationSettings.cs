using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/10/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class GlobalApplicationSettings
	{
		static GlobalApplicationSettings()
		{
		}

		private static Models.GlobalApplicationSettings _instance;
		public static Models.GlobalApplicationSettings Instance
		{
			get
			{
				GetInstance();

				return (_instance);
			}
		}

		private static void GetInstance()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				Models.GlobalApplicationSettings oGlobalApplicationSettings =
					oUnitOfWork.GlobalApplicationSettingsRepository
					.Get()
					.FirstOrDefault();

				if (oGlobalApplicationSettings == null)
				{
					oGlobalApplicationSettings =
						new Models.GlobalApplicationSettings();
				}

				_instance = oGlobalApplicationSettings;
			}
			catch //(System.Exception ex)
			{
				_instance =
					new Models.GlobalApplicationSettings();
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
