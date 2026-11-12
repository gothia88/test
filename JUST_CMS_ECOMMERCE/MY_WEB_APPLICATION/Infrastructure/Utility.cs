namespace Infrastructure
{
	/// <summary>
	/// Version: 1.1.3
	/// Update Date: 1393/12/09
	/// Changed In Version: 1.8.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Utility
	{
		static Utility()
		{
		}

		public static string SimpleFixSuchAsAddress(string text)
		{
			if (text == null)
			{
				return (null);
			}

			text = text.Trim();

			if (text == string.Empty)
			{
				return (null);
			}

			while (text.Contains("  "))
			{
				text =
					text.Replace("  ", " ");
			}

			text =
				text.Replace(System.Environment.NewLine, "<br />");

			text =
				text.Replace("\r", "<br />");

			text =
				text.Replace("\n", "<br />");

			return (text);
		}

		public static string RenderPartialViewToString
			(System.Web.Mvc.ControllerContext context, string viewName, object model)
		{
			var varController = context.Controller;

			var varPartialView =
				System.Web.Mvc.ViewEngines.Engines
				.FindPartialView(varController.ControllerContext, viewName);

			var varStringBuilder = new System.Text.StringBuilder();

			using (var varStringWriter = new System.IO.StringWriter(varStringBuilder))
			{
				using (var varHtmlWriter = new System.Web.UI.HtmlTextWriter(varStringWriter))
				{
					varController.ViewData.Model = model;

					varPartialView.View.Render
						(new System.Web.Mvc.ViewContext
							(varController.ControllerContext,
							varPartialView.View,
							varController.ViewData,
							new System.Web.Mvc.TempDataDictionary(),
							varHtmlWriter), varHtmlWriter);
				}
			}

			return (varStringBuilder.ToString());
		}

		public static string FixText(string text)
		{
			string strResult = text;

			strResult = strResult.Trim();

			while (strResult.Contains("  "))
			{
				strResult =
					strResult.Replace("  ", " ");
			}

			strResult =
				strResult
				.Replace("ي", "ی")
				.Replace("ك", "ک");

			return (strResult);
		}

		public static string GetEditText()
		{
			string strResult =
				string.Format("<span class='glyphicon glyphicon-edit'data-toggle='tooltip' data-placement='top' title='{0}'></span>",
				Resources.Actions.Edit);

			return (strResult);
		}

		public static bool CheckNationalCode(string nationalCode)
		{
			if (nationalCode.Length != 10 ||
				nationalCode.Equals("0000000000") || nationalCode.Equals("1111111111") ||
				nationalCode.Equals("2222222222") || nationalCode.Equals("3333333333") ||
				nationalCode.Equals("4444444444") || nationalCode.Equals("5555555555") ||
				nationalCode.Equals("6666666666") || nationalCode.Equals("7777777777") ||
				nationalCode.Equals("8888888888") || nationalCode.Equals("9999999999"))
			{
				return (false);
			}

			int intSumNational = 0;

			for (int intIndex = 0; intIndex < 9; intIndex++)
			{
				intSumNational +=
					((10 - intIndex) * System.Convert.ToInt32(nationalCode[intIndex].ToString()));
			}

			var modNationalCardNumber = intSumNational % 11;

			if (modNationalCardNumber >= 2)
			{
				if (11 - modNationalCardNumber != System.Convert.ToInt32(nationalCode[9].ToString()))
				{
					return (false);
				}
			}

			return (true);
		}

		public static string GetUserIP()
		{
			return (System.Web.HttpContext.Current.Request.UserHostAddress);
		}

		//public static System.DateTime Now
		//{
		//	get
		//	{
		//		int intServerTimeZoneDifference =
		//			Infrastructure.ApplicationSettings.Instance.ServerTimeZoneDifference;

		//		return (System.DateTime.Now.AddMinutes(intServerTimeZoneDifference));
		//	}
		//}

		public static int CultureLcid
		{
			get
			{
				return (System.Threading.Thread.CurrentThread.CurrentCulture.LCID);
			}
		}

		public static string CultureName
		{
			get
			{
				return (System.Threading.Thread.CurrentThread.CurrentCulture.Name);
			}
		}

		public static string GetAreaName()
		{
			if ((System.Web.HttpContext.Current == null) ||
				(System.Web.HttpContext.Current.Request == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens.Count == 0))
			{
				return (string.Empty);
			}

			object objAreaName =
				System.Web.HttpContext.Current.Request
				.RequestContext.RouteData.DataTokens["area"];

			string strAreaName = string.Empty;
			if (objAreaName != null)
			{
				strAreaName =
					objAreaName.ToString().Replace(" ", string.Empty);
			}

			return (strAreaName);
		}

		public static string GetControllerName()
		{
			if ((System.Web.HttpContext.Current == null) ||
				(System.Web.HttpContext.Current.Request == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values.Count == 0))
			{
				return (string.Empty);
			}

			object objControllerName =
				System.Web.HttpContext.Current.Request
				.RequestContext.RouteData.Values["Controller"];

			string strControllerName = string.Empty;
			if (objControllerName != null)
			{
				strControllerName =
					objControllerName.ToString().Replace(" ", string.Empty);
			}

			return (strControllerName);
		}

		public static string GetActionName()
		{
			if ((System.Web.HttpContext.Current == null) ||
				(System.Web.HttpContext.Current.Request == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values == null) ||
				(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values.Count == 0))
			{
				return (string.Empty);
			}

			object objActionName =
				System.Web.HttpContext.Current.Request
				.RequestContext.RouteData.Values["Action"];

			string strActionName = string.Empty;
			if (objActionName != null)
			{
				strActionName =
					objActionName.ToString().Replace(" ", string.Empty);
			}

			return (strActionName);
		}

		public static string GetLayoutRootRelativePathName()
		{
			string strAreaName = Infrastructure.Utility.GetAreaName();
			string strActionNamae = Infrastructure.Utility.GetActionName();
			string strControllerName = Infrastructure.Utility.GetControllerName();

			DAL.UnitOfWork oUnitOfWork = new DAL.UnitOfWork();

			Models.ProjectAction oProjectAction =
				oUnitOfWork.ProjectActionRepository.GetByRouteValues
				(strAreaName, strControllerName, strActionNamae);

			string strCulture =
				System.Threading.Thread.CurrentThread.CurrentCulture.Name;

			string strResult = string.Empty;
			string strLayoutPath = string.Empty;
			string strLayout = "~/Views/Shared/_Layouts";

			if (oProjectAction == null)
			{
				strResult =
					string.Format("{0}/default.{1}", strLayout, strCulture);
			}
			else
			{
				if (oProjectAction.Layout == null)
				{
					strResult =
						string.Format("{0}/default.{1}", strLayout, strCulture);
				}
				else
				{
					strResult =
						string.Format("{0}/{1}.{2}",
						strLayout, oProjectAction.Layout.Name.ToLower(), strCulture);

					strLayoutPath =
						System.Web.HttpContext.Current.Server.MapPath(strResult);

					if (System.IO.Directory.Exists(strLayoutPath) == false)
					{
						strResult =
							string.Format("{0}/default.{1}", strLayout, strCulture);
					}
				}
			}

			strLayoutPath =
				System.Web.HttpContext.Current.Server.MapPath(strResult);

			if (System.IO.Directory.Exists(strLayoutPath) == false)
			{
				strResult =
					string.Format("{0}/default.fa-IR", strLayout);
			}

			strResult =
				string.Format("{0}/_Site.cshtml", strResult);

			return (strResult);
		}

		public static string GetRowStyle(Models.IBaseExtendedEntity entity)
		{
			if (entity.IsDeleted)
			{
				return ("danger");
			}
			else
			{
				if (entity.IsSystem)
				{
					return ("warning");
				}
				else
				{
					if (entity.IsVerified)
					{
						return ("success");
					}
					else
					{
						if (entity.IsActive == false)
						{
							return ("active");
						}
					}
				}
			}

			return (string.Empty);
		}

		public static void IsApplicationInDemoVersion()
		{
			Models.GlobalApplicationSettings oGlobalApplicationSettings =
				Infrastructure.GlobalApplicationSettings.Instance;

			if (oGlobalApplicationSettings.IsApplicationInDemoVersion)
			{
				System.Threading.Thread.Sleep
					(oGlobalApplicationSettings.SleepTimeoutInDemoVersion);
			}
		}
	}
}
