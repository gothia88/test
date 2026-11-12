namespace MY_WEB_APPLICATION.Areas.Administrator
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AdministratorAreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public AdministratorAreaRegistration()
		{
		}

		public override string AreaName
		{
			get
			{
				return ("Administrator");
			}
		}

		public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
		{
			//context.MapRoute(
			//	"Administrator_Default",
			//	"Administrator/{controller}/{action}/{id}",
			//	new { action = "Index", id = UrlParameter.Optional }
			//);

			context.MapRoute(
				"Administrator_Default",
				"Administrator/{controller}/{action}/{id}",
				new { action = "Index", id = System.Web.Mvc.UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			);

			//context.MapRoute(
			//	"Administrator_Default",
			//	"Administrator/Home/Index/{id}",
			//	new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			//);

			context.MapRoute(
				"Administrator_Page",
				"Administrator/Page/{action}/{id}",
				new { controller = "Page", action = "Index", id = System.Web.Mvc.UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			);
		}
	}
}
