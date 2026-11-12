using System.Linq;
using System.Data.Entity;

namespace Models
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/25
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Utility
	{
		static Utility()
		{
		}

		public static bool IsEntityValid(BaseEntity entity)
		{
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext =
					new DatabaseContext();

				var varValidationResult =
					oDatabaseContext.Entry(entity).GetValidationResult();

				if (varValidationResult.IsValid)
				{
					return (true);
				}
				else
				{
					foreach (var varValidationError in varValidationResult.ValidationErrors)
					{
						string strPropertyName =
							varValidationError.PropertyName;

						string strErrorMessage =
							varValidationError.ErrorMessage;

						string strResult =
							string.Format("{0}: {1}", strPropertyName, strErrorMessage);
					}

					return (false);
				}
			}
			catch
			{
				return (false);
			}
			finally
			{
				if (oDatabaseContext != null)
				{
					oDatabaseContext.Dispose();
					oDatabaseContext = null;
				}
			}
		}

		//public static bool IsDatabaseSqlServerCompactEdition
		//{
		//	get
		//	{
		//		bool blnResult = false;

		//		if (System.Configuration.ConfigurationManager.ConnectionStrings.Count != 0)
		//		{
		//			System.Configuration.ConnectionStringSettings oConnectionStringSettings =
		//				System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"];

		//			if (oConnectionStringSettings != null)
		//			{
		//				if (oConnectionStringSettings.ConnectionString.Contains("MyDatabase.sdf"))
		//				{
		//					blnResult = true;
		//				}
		//			}
		//		}

		//		return (blnResult);
		//	}
		//}

		public static Enums.DatabaseProviders DatabaseProvider
		{
			get
			{
				Enums.DatabaseProviders enmDatabaseProvider =
					Enums.DatabaseProviders.SqlServer;

				if (System.Configuration.ConfigurationManager.ConnectionStrings.Count != 0)
				{
					System.Configuration.ConnectionStringSettings oConnectionStringSettings =
						System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"];

					if (oConnectionStringSettings != null)
					{
						if (oConnectionStringSettings.ConnectionString.Contains("MyDatabase.sdf"))
						{
							enmDatabaseProvider =
								Enums.DatabaseProviders.SqlServerCompactEdition;
						}
						else
						{
							//if (oConnectionStringSettings.ConnectionString.Contains(""))
							//{
							//	enmDatabaseProvider =
							//		Enums.DatabaseProviders.Oracle;
							//}
						}
					}
				}

				return (enmDatabaseProvider);
			}
		}

		public static System.DateTime Now
		{
			get
			{
				System.DateTime dtmNow = System.DateTime.Now;

				DatabaseContext oDatabaseContext = null;

				try
				{
					oDatabaseContext =
						new DatabaseContext();

					ApplicationSettings oApplicationSettings =
						oDatabaseContext.MyApplicationSettings
						.FirstOrDefault();

					if (oApplicationSettings != null)
					{
						dtmNow =
							dtmNow.AddMinutes(oApplicationSettings.ServerTimeZoneDifference);
					}
				}
				catch
				{
				}
				finally
				{
					if (oDatabaseContext != null)
					{
						oDatabaseContext.Dispose();
						oDatabaseContext = null;
					}
				}

				return (dtmNow);
			}
		}
	}
}
