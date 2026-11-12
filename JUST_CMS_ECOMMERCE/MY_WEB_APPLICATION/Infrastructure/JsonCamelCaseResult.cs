namespace Infrastructure
{
	public class JsonCamelCaseResult : System.Web.Mvc.JsonResult
	{
		public JsonCamelCaseResult()
		{
		}

		public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
		{
			if (context == null)
			{
				throw (new System.ArgumentNullException("context"));
			}

			if ((JsonRequestBehavior == System.Web.Mvc.JsonRequestBehavior.DenyGet) &&
				(System.String.Equals(context.HttpContext.Request.HttpMethod, "GET", System.StringComparison.OrdinalIgnoreCase)))
			{
				throw (new System.InvalidOperationException
					("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet."));
			}

			var varResponse = context.HttpContext.Response;

			if (string.IsNullOrWhiteSpace(ContentType))
			{
				ContentType = "application/json";
				varResponse.ContentType = "application/json";
			}

			if (ContentEncoding != null)
			{
				varResponse.ContentEncoding = ContentEncoding;
			}

			if (Data == null)
			{
				return;
			}

			var varJsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
			{
				ContractResolver =
					new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
			};

			varResponse.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Data, varJsonSerializerSettings));
		}
	}
}
