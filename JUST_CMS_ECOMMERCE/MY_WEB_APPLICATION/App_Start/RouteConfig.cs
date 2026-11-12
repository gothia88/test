using System.Web.Mvc;

namespace MY_WEB_APPLICATION
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/10/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class RouteConfig
	{
		static RouteConfig()
		{
		}

		public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// **************************************************
			//routes.MapRoute("Captcha",
			//	"DefaultCaptcha/Generate",
			//	new { controller = "DefaultCaptcha", action = "Generate" });

			//routes.MapRoute("CaptchaRefresh",
			//	"DefaultCaptcha/Refresh",
			//	new { controller = "DefaultCaptcha", action = "Refresh" });
			// **************************************************

			//// **************************************************
			//routes.MapRoute(
			//	null,
			//	"connector",
			//	new { controller = "Files", action = "Index" },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			//	);

			//routes.MapRoute(
			//	null,
			//	"Thumbnails/{tmb}",
			//	new { controller = "Files", action = "Thumbs", tmb = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			//	);
			//// **************************************************

			// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			routes.MapRoute(
				name: null,
				url: "Error/{code}",
				defaults: new { controller = "Error", action = "Display", code = System.Web.Mvc.UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			);

			//// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			//routes.MapRoute(
			//	name: null,
			//	url: "Page/{name}",
			//	defaults: new { area = "Cms", controller = "General", action = "DisplayPage", name = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Areas.Cms.Controllers" }
			//);

			//// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			//routes.MapRoute(
			//	name: "MyRoute2",
			//	url: "Post/{id}",
			//	defaults: new { controller = "Post", action = "Display", id = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			//);

			//// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			//routes.MapRoute(
			//	name: "MyRoute3",
			//	url: "PostCategory/{id}",
			//	defaults: new { controller = "PostCategory", action = "Index", id = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			//);

			//// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			//routes.MapRoute(
			//	name: "MyRoute4",
			//	url: "Photo/{id}",
			//	defaults: new { controller = "Photo", action = "Display", id = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			//);

			//// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			//routes.MapRoute(
			//	name: "MyRoute5",
			//	url: "PhotoAlbum/{id}",
			//	defaults: new { controller = "PhotoAlbum", action = "Display", id = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			//);

			//// قرار بگيرد Default ، Route دقت کنيد که دستور ذيل بايد قبل از
			//routes.MapRoute(
			//	name: "MyRoute6",
			//	url: "Rss/{id}",
			//	defaults: new { controller = "Rss", action = "Index", id = System.Web.Mvc.UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			//);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = System.Web.Mvc.UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Controllers" }
			);
		}
	}
}
