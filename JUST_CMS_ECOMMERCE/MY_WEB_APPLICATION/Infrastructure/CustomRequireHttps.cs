namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CustomRequireHttpsAttribute : System.Web.Mvc.RequireHttpsAttribute
	{
		public CustomRequireHttpsAttribute()
		{
		}

		protected override void HandleNonHttpsRequest
			(System.Web.Mvc.AuthorizationContext filterContext)
		{
			if (Infrastructure.GlobalApplicationSettings.Instance.IsSslEnabled)
			{
				base.HandleNonHttpsRequest(filterContext);
			}
		}
	}
}
