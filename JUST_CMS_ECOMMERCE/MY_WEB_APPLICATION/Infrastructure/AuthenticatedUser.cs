namespace Infrastructure
{
	/// <summary>
	/// Version: 1.1.2
	/// Update Date: 1394/01/10
	/// Changed In Version: 1.8.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AuthenticatedUser : System.Object, Models.IAuthenticatedUser
	{
		public static bool IsAuthenticated
		{
			get
			{
				if ((System.Web.HttpContext.Current != null) &&
					(System.Web.HttpContext.Current.Request != null) &&
					(System.Web.HttpContext.Current.Request.IsAuthenticated))
				{
					if (Sessions.AuthenticatedUser == null)
					{
						// باشد false بايد ،endResponse دقت کنيد که مقدار
						System.Web.HttpContext.Current.Response.Redirect
							("~/Account/Logout", endResponse: false);

						return (false);
					}
					else
					{
						return (true);
					}
				}
				else
				{
					if (Sessions.AuthenticatedUser != null)
					{
						Sessions.AuthenticatedUser = null;

						// باشد false بايد ،endResponse دقت کنيد که مقدار
						System.Web.HttpContext.Current.Response.Redirect
							("~/Account/Logout", endResponse: false);
					}

					return (false);
				}
			}
		}

		public static void SignOut()
		{
			// **************************************************
			DAL.UnitOfWork oUnitOfWork = null;
			try
			{
				if (Infrastructure.Sessions.AuthenticatedUser != null)
				{
					System.Guid sUserId =
						Infrastructure.Sessions.AuthenticatedUser.Id;

					string strSessionId =
						System.Web.HttpContext.Current.Session.SessionID;

					oUnitOfWork =
						new DAL.UnitOfWork();

					Models.UserLoginLog oUserLoginLog =
						oUnitOfWork.UserLoginLogRepository
						.GetBySessionIdAndUserId(strSessionId, sUserId);

					if (oUserLoginLog != null)
					{
						oUserLoginLog.LogoutDateTime.Value = Models.Utility.Now;

						oUnitOfWork.Save();
					}
				}
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
			// **************************************************

			System.Web.Security.FormsAuthentication.SignOut();

			//Session.Clear();
			Sessions.AuthenticatedUser = null;
			//Session.Remove(Infrastructure.Sessions.AuthenticatedUserKeyName);
		}

		public AuthenticatedUser(Models.User user)
		{
			Id = user.Id;

			FullName = user.FullName;
			Role = user.Role.CodeEnum;
			EmailAddress = user.EmailAddress.ToLower();
			IsCellPhoneNumberVerified = user.IsCellPhoneNumberVerified;

			if (user.PlanId.HasValue == false)
			{
				PlanId = null;
				PlanName = null;
				FinishPlanDateTime = null;
			}
			else
			{
				PlanId = user.Plan.Id;
				PlanName = user.Plan.Name;
				FinishPlanDateTime = user.FinishPlanDateTime.Value;
			}
		}

		// **********
		public System.Guid Id { get; protected set; }
		// **********

		// **********
		public string FullName { get; protected set; }
		// **********

		// **********
		public string EmailAddress { get; protected set; }
		// **********

		// **********
		// **********
		// **********
		public Models.Enums.Roles Role { get; protected set; }
		// **********

		// **********
		public bool IsSiteResponsible
		{
			get
			{
				switch (Role)
				{
					case Models.Enums.Roles.Owner:
					case Models.Enums.Roles.Programmer:
					case Models.Enums.Roles.Supervisor:
					case Models.Enums.Roles.Administrator:
					{
						return (true);
					}

					default:
					{
						return (false);
					}
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		public bool IsCellPhoneNumberVerified { get; protected set; }
		// **********

		// **********
		// **********
		// **********
		// **********
		public string PlanName { get; protected set; }
		// **********

		// **********
		public System.Guid? PlanId { get; protected set; }
		// **********

		// **********
		public System.DateTime? FinishPlanDateTime { get; set; }
		// **********
		// **********
		// **********
	}
}
