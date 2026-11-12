namespace MY_WEB_APPLICATION.Areas.ECommerce
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ECommerceAreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public ECommerceAreaRegistration()
		{
		}

		public override string AreaName
		{
			get
			{
				return ("ECommerce");
			}
		}

		public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
		{
			context.MapRoute(
				"ECommerce_default",
				"ECommerce/{controller}/{action}/{id}",
				new { action = "Index", id = System.Web.Mvc.UrlParameter.Optional }
			);
		}
	}
}
