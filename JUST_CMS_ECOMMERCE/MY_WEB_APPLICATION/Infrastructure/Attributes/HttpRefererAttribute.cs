using System.Linq;
using System.Data.Entity;

namespace Infrastructure.Attributes
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/01/04
	/// Changed In Version: 1.8.4
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class HttpRefererAttribute : System.Web.Mvc.ActionFilterAttribute
	{
		public HttpRefererAttribute()
		{
		}

		public override void OnActionExecuting
			(System.Web.Mvc.ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			if ((System.Web.HttpContext.Current != null) &&
				(System.Web.HttpContext.Current.Request != null))
			{
				string strHttpReferer =
					System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

				if (string.IsNullOrEmpty(strHttpReferer) == false)
				{
					System.Uri oUri =
						new System.Uri(strHttpReferer);

					string strHost = oUri.Host;

					if (string.Compare(strHost, "localhost",
						comparisonType: System.StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						// دستور ذيل نبايد نوشته شود
						//System.Web.HttpContext.Current.Session.Remove("HttpReferer");

						return;
					}

					if (string.Compare(strHost, "IranianExperts.ir",
						comparisonType: System.StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						// دستور ذيل نبايد نوشته شود
						//System.Web.HttpContext.Current.Session.Remove("HttpReferer");

						return;
					}

					System.Web.HttpContext.Current.Session["HttpReferer"] = strHttpReferer;
				}
			}
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
