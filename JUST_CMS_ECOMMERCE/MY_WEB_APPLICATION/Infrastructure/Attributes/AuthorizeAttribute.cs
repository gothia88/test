using System.Linq;
using System.Data.Entity;

namespace Infrastructure.Attributes
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1394/01/04
	/// Changed In Version: 1.8.4
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AuthorizeAttribute : System.Web.Mvc.ActionFilterAttribute
	{
		public AuthorizeAttribute()
		{
		}

		public override void OnActionExecuting
			(System.Web.Mvc.ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			// **************************************************
			object objArea =
				filterContext.RouteData.DataTokens["area"];

			string strAreaName = string.Empty;
			if (objArea != null)
			{
				strAreaName = objArea.ToString().Trim();
			}
			// **************************************************

			// **************************************************
			object objController =
				filterContext.RouteData.Values["controller"];

			if ((objController == null) ||
				(objController.ToString().Trim() == string.Empty))
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "BadRequest",
						filterContext: filterContext
					);

				return;
			}

			string strControllerName =
				objController.ToString().Trim();
			// **************************************************

			// **************************************************
			object objAction =
				filterContext.RouteData.Values["action"];

			if ((objAction == null) ||
				(objAction.ToString().Trim() == string.Empty))
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "BadRequest",
						filterContext: filterContext
					);

				return;
			}

			string strActionName =
				objAction.ToString().Trim();
			// **************************************************

			// **************************************************
			string strId = string.Empty;

			object objId =
				filterContext.RouteData.Values["Id"];

			if (objId != null)
			{
				strId = objId.ToString();
			}
			// **************************************************

			// **************************************************
			string strReturnUrl = string.Empty;
			if (string.IsNullOrEmpty(strAreaName))
			{
				if (string.IsNullOrWhiteSpace(strId))
				{
					strReturnUrl =
						string.Format("/{0}/{1}",
						strControllerName, strActionName);
				}
				else
				{
					strReturnUrl =
						string.Format("/{0}/{1}/{2}",
						strControllerName, strActionName, strId);
				}
			}
			else
			{
				if (string.IsNullOrWhiteSpace(strId))
				{
					strReturnUrl =
						string.Format("/{0}/{1}/{2}",
						strAreaName, strControllerName, strActionName);
				}
				else
				{
					strReturnUrl =
						string.Format("/{0}/{1}/{2}/{3}",
						strAreaName, strControllerName, strActionName, strId);
				}
			}
			// **************************************************

			// **************************************************
			// **************************************************
			// **************************************************

			if ((strAreaName == string.Empty) &&
				(string.Compare(strActionName, "Generate", ignoreCase: true) == 0) &&
				(string.Compare(strControllerName, "DefaultCaptcha", ignoreCase: true) == 0))
			{
				return;
			}

			if ((strAreaName == string.Empty) &&
				(string.Compare(strActionName, "Refresh", ignoreCase: true) == 0) &&
				(string.Compare(strControllerName, "DefaultCaptcha", ignoreCase: true) == 0))
			{
				return;
			}

			// **************************************************
			// **************************************************
			// **************************************************

			if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			{
				switch (Infrastructure.Sessions.AuthenticatedUser.Role)
				{
					case Models.Enums.Roles.Programmer:
					{
						return;
					}
				}
			}

			// **************************************************
			// اگر
			// Infrastructure.AuthenticatedUser.IsAuthenticated = false
			// يا
			//		Infrastructure.AuthenticatedUser.IsAuthenticated = true
			//		و
			//		Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer
			// **************************************************

			DAL.UnitOfWork oUnitOfWork = new DAL.UnitOfWork();

			// **************************************************
			Models.ProjectAction oProjectAction =
				oUnitOfWork.ProjectActionRepository
				.GetByRouteValues(strAreaName, strControllerName, strActionName);

			if (oProjectAction == null)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "BadRequest",
						filterContext: filterContext
					);

				return;
			}
			// **************************************************

			// **************************************************
			// اگر
			// oProjectAction != null
			// **************************************************

			if ((oProjectAction.IsVisibleJustForProgrammer) ||
				(oProjectAction.ProjectController.IsVisibleJustForProgrammer) ||
				(oProjectAction.ProjectController.ProjectArea.IsVisibleJustForProgrammer))
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "Forbidden",
						filterContext: filterContext
					);

				return;
			}

			if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			{
				switch (Infrastructure.Sessions.AuthenticatedUser.Role)
				{
					case Models.Enums.Roles.Owner:
					case Models.Enums.Roles.Administrator:
					{
						return;
					}
				}
			}

			// **************************************************
			// اگر
			// Infrastructure.AuthenticatedUser.IsAuthenticated = false
			// يا
			//		Infrastructure.AuthenticatedUser.IsAuthenticated = true
			//		و
			//			Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User
			//			يا
			//			Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Supervisor
			// **************************************************

			if ((oProjectAction.IsActive == false) ||
				(oProjectAction.ProjectController.IsActive == false) ||
				(oProjectAction.ProjectController.ProjectArea.IsActive == false))
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "Forbidden",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// oProjectAction.IsActive = true
			// و
			// oProjectAction.ProjectController.IsActive = true
			// و
			// oProjectAction.ProjectController.ProjectArea.IsActive = true
			// **************************************************

			if (oProjectAction.AccessTypeEnum == Models.Enums.AccessTypes.Public)
			{
				return;
			}

			// **************************************************
			// اگر
			// oProjectAction.IsPublic == false
			// **************************************************

			if (Infrastructure.AuthenticatedUser.IsAuthenticated == false)
			{
				string strUrl =
					string.Format("/Account/Login?ReturnUrl={0}", strReturnUrl);

				filterContext.Result =
					new System.Web.Mvc.RedirectResult(strUrl);

				return;
			}

			// **************************************************
			// اگر
			// Infrastructure.AuthenticatedUser.IsAuthenticated = true
			// **************************************************

			if (oProjectAction.AccessTypeEnum == Models.Enums.AccessTypes.Registered)
			{
				return;
			}

			// **************************************************
			// اگر
			// oAction.AccessTypeEnum = Models.Enums.AccessTypes.Special
			// و
			//		Infrastructure.Sessions.AuthenticatedUser.Role = Models.Enums.Roles.User
			//		يا
			//		Infrastructure.Sessions.AuthenticatedUser.Role = Models.Enums.Roles.Supervisor
			// **************************************************

			int intGroupCount =
				oUnitOfWork.GroupRepository.GetActiveGroupCountByUserIdAndActionId
				(Infrastructure.Sessions.AuthenticatedUser.Id, oProjectAction.Id);

			if (intGroupCount >= 1)
			{
				return;
			}

			// **************************************************
			// اگر
			// intGroupCount = 0
			// **************************************************

			bool blnUserHasDirectAccessForAction =
				oUnitOfWork.UserRepository.CheckDirectAccessForAction
				(Infrastructure.Sessions.AuthenticatedUser.Id, oProjectAction.Id);

			if (blnUserHasDirectAccessForAction)
			{
				return;
			}

			// **************************************************
			// اگر
			// blnUserHasDirectAccessForAction = false
			// **************************************************

			RedirectToRouteResult
				(
					areaName: string.Empty,
					controllerName: "Error",
					actionName: "Forbidden",
					filterContext: filterContext
				);
		}

		private void RedirectToRouteResult
			(System.Web.Mvc.ActionExecutingContext filterContext,
			string areaName, string controllerName, string actionName,
			System.Collections.Generic.IDictionary<string, object> actionParameters = null)
		{
			if (actionParameters != null)
			{
				foreach (var varItem in actionParameters)
				{
					filterContext.ActionParameters.Add(varItem);
				}
			}

			filterContext.Result =
				new System.Web.Mvc.RedirectToRouteResult(
				new System.Web.Routing.RouteValueDictionary
					{
						{ "area", areaName },
						{ "controller", controllerName },
						{ "action", actionName }
					});
		}

		public override void OnActionExecuted
			(System.Web.Mvc.ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);
		}

		public override void OnResultExecuting
			(System.Web.Mvc.ResultExecutingContext filterContext)
		{
			base.OnResultExecuting(filterContext);
		}

		public override void OnResultExecuted
			(System.Web.Mvc.ResultExecutedContext filterContext)
		{
			base.OnResultExecuted(filterContext);
		}
	}
}
