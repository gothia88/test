namespace MY_WEB_APPLICATION.Areas.Cms
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CmsAreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public CmsAreaRegistration()
		{
		}

		public override string AreaName
		{
			get
			{
				return ("Cms");
			}
		}

		public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
		{
			context.MapRoute(
				name: null,
				url: "Page/{name}",
				defaults: new { controller = "General", action = "DisplayPage", name = System.Web.Mvc.UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Areas.Cms.Controllers" }
			);

			context.MapRoute(
				"Cms_Default",
				"Cms/{controller}/{action}/{id}",
				new { action = "Index", id = System.Web.Mvc.UrlParameter.Optional }
			);
		}
	}
}
