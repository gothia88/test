using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/01/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Tag)]
	public partial class TagController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public TagController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Display)]
		public virtual System.Web.Mvc.ActionResult Display(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Common.Tag oTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oTag == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oTag.IsDeleted)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oTag.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			return (View(oTag));
		}
	}
}
