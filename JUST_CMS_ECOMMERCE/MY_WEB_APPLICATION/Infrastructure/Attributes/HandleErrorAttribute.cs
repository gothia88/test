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
	public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
	{
		public HandleErrorAttribute()
		{
		}

		public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
		{
			Dtx.LogHandler.Report(GetType(), null, filterContext.Exception);

			base.OnException(filterContext);
		}
	}
}