namespace MY_WEB_APPLICATION
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public MvcApplication()
		{
		}

		protected void Application_Start()
		{
			System.Web.Mvc.AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(System.Web.Mvc.GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);

			//BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);

			// **************************************************
			Infrastructure.CustomDateTimeModelBinder oCustomDateTimeModelBinder =
				new Infrastructure.CustomDateTimeModelBinder();

			System.Web.Mvc.ModelBinders.Binders.Add
				(typeof(Models.ComplexTypes.CustomDateTime), oCustomDateTimeModelBinder);
			// **************************************************

			// **************************************************
			Infrastructure.CustomRequiredDateTimeModelBinder oCustomRequiredDateTimeModelBinder =
				new Infrastructure.CustomRequiredDateTimeModelBinder();

			System.Web.Mvc.ModelBinders.Binders.Add
				(typeof(Models.ComplexTypes.CustomRequiredDateTime), oCustomRequiredDateTimeModelBinder);
			// **************************************************
		}

		protected void Session_Start()
		{
		}

		protected void Application_Error()
		{
			if ((Server != null) &&
				(Server.GetLastError() != null))
			{
				System.Exception ex = Server.GetLastError();

				Dtx.LogHandler.Report(GetType(), null, ex);
			}
		}

		protected void Session_End()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				var varResult =
					oUnitOfWork.UserLoginLogRepository.GetBySessionId(Session.SessionID);

				foreach (Models.UserLoginLog oCurrentUserLoginLog in varResult)
				{
					oCurrentUserLoginLog.LogoutDateTime.Value = Models.Utility.Now;
				}

				oUnitOfWork.Save();
			}
			catch
			{
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

		protected void Application_End()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				var varResult =
					oUnitOfWork.UserLoginLogRepository.GetAuthenticatedUsers();

				foreach (Models.UserLoginLog oCurrentUserLoginLog in varResult)
				{
					oCurrentUserLoginLog.LogoutDateTime.Value = Models.Utility.Now;
				}

				oUnitOfWork.Save();
			}
			catch
			{
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
