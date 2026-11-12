namespace MY_WEB_APPLICATION.Areas.Administrator.BLL
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/08/19
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class User
	{
		static User()
		{
		}

		public static bool CheckPermission
			(System.Web.Mvc.ActionResult actionResult)
		{
			// **************************************************
			System.Web.Mvc.IT4MVCActionResult oT4MVCActionResult =
				actionResult as System.Web.Mvc.IT4MVCActionResult;

			if (oT4MVCActionResult == null)
			{
				return (false);
			}
			// **************************************************

			// **************************************************
			string strAreaName = string.Empty;

			object objAreaName =
				oT4MVCActionResult.RouteValueDictionary["Area"];

			if (objAreaName != null)
			{
				strAreaName =
					objAreaName.ToString().Replace(" ", string.Empty);
			}
			// **************************************************

			bool blnCheckPermission =
				CheckPermission(strAreaName, oT4MVCActionResult.Controller, oT4MVCActionResult.Action);

			return (blnCheckPermission);

			//return (CheckPermission
			//	(strAreaName, oT4MVCActionResult.Controller, oT4MVCActionResult.Action));
		}

		public static bool CheckPermission
			(string areaName, string controllerName, string actionName)
		{
			if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			{
				switch (Infrastructure.Sessions.AuthenticatedUser.Role)
				{
					case Models.Enums.Roles.Programmer:
					{
						return (true);
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
				.GetByRouteValues(areaName, controllerName, actionName);

			if (oProjectAction == null)
			{
				return (false);
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
				return (false);
			}

			if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			{
				switch (Infrastructure.Sessions.AuthenticatedUser.Role)
				{
					case Models.Enums.Roles.Owner:
					case Models.Enums.Roles.Administrator:
					{
						return (true);
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
				return (false);
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
				return (true);
			}

			// **************************************************
			// اگر
			// oProjectAction.IsPublic == false
			// **************************************************

			if (Infrastructure.AuthenticatedUser.IsAuthenticated == false)
			{
				return (false);
			}

			// **************************************************
			// اگر
			// Infrastructure.AuthenticatedUser.IsAuthenticated = true
			// **************************************************

			if (oProjectAction.AccessTypeEnum == Models.Enums.AccessTypes.Registered)
			{
				return (true);
			}

			// **************************************************
			// اگر
			// oProjectAction.AccessTypeEnum = Models.Enums.AccessTypes.Special
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
				return (true);
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
				return (true);
			}

			// **************************************************
			// اگر
			// blnUserHasDirectAccessForAction = false
			// **************************************************

			return (false);
		}
	}
}
