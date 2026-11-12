namespace MY_WEB_APPLICATION
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/01/04
	/// Changed In Version: 1.8.4
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class FilterConfig
	{
		static FilterConfig()
		{
		}

		public static void RegisterGlobalFilters
			(System.Web.Mvc.GlobalFilterCollection filters)
		{
			// ترتيب نوشتن اهميت دارد
			filters.Add(new Infrastructure.Attributes.HttpRefererAttribute());
			filters.Add(new Infrastructure.Attributes.AuthorizeAttribute());
			filters.Add(new Infrastructure.Attributes.HandleErrorAttribute());
		}
	}
}
