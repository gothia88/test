namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/07/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Error)]
	public partial class ErrorController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ErrorController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Errors),
			resourceName: Resources.Strings.ErrorsKeys.BadRequest)]
		public virtual System.Web.Mvc.ActionResult BadRequest()
		{
			return (RedirectToAction
				(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Errors),
			resourceName: Resources.Strings.ErrorsKeys.Forbidden)]
		public virtual System.Web.Mvc.ActionResult Forbidden()
		{
			return (RedirectToAction
				(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Errors),
			resourceName: Resources.Strings.ErrorsKeys.DisplayErrors)]
		public virtual System.Web.Mvc.ViewResult Display(System.Net.HttpStatusCode? code)
		{
			if (code == null)
			{
				code = System.Net.HttpStatusCode.BadRequest;
			}

			switch (code.Value)
			{
				case System.Net.HttpStatusCode.BadRequest:
				{
					ViewBag.Message = Resources.Errors.BadRequest;
					break;
				}

				case System.Net.HttpStatusCode.Forbidden:
				{
					ViewBag.Message = Resources.Errors.Forbidden;
					break;
				}

				case System.Net.HttpStatusCode.NotFound:
				{
					ViewBag.Message = Resources.Errors.NotFound;
					break;
				}

				case System.Net.HttpStatusCode.TemporaryRedirect:
				{
					ViewBag.Message = Resources.Errors.TemporaryRedirect;
					break;
				}

				case System.Net.HttpStatusCode.Unauthorized:
				{
					ViewBag.Message = Resources.Errors.Unauthorized;
					break;
				}

				default:
				{
					ViewBag.Message = code.ToString();

					break;
				}
			}

			return (View());
		}
	}
}
