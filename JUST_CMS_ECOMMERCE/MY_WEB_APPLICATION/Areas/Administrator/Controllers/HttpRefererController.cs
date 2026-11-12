using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/25
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.HttpReferer)]
	public partial class HttpRefererController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public HttpRefererController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varHttpReferers =
				UnitOfWork.HttpRefererRepository.Get()
				.OrderByDescending(current => current.FirstHitDateTime.Value)
				.ToList()
				;

			return (View(varHttpReferers));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.HttpReferer oHttpReferer =
				UnitOfWork.HttpRefererRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oHttpReferer == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oHttpReferer));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Url,Name,Hits,DomainName,Description,IsPublic")]
			Models.HttpReferer httpReferer)
		{
			// **************************************************
			Models.HttpReferer oOriginalHttpReferer =
				UnitOfWork.HttpRefererRepository.Get()
				.Where(current => current.Id == httpReferer.Id)
				.FirstOrDefault();

			if (oOriginalHttpReferer == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				oOriginalHttpReferer.Url = httpReferer.Url;
				oOriginalHttpReferer.Hits = httpReferer.Hits;
				oOriginalHttpReferer.Name = httpReferer.Name;
				oOriginalHttpReferer.IsPublic = httpReferer.IsPublic;
				oOriginalHttpReferer.DomainName = httpReferer.DomainName;
				oOriginalHttpReferer.Description = httpReferer.Description;

				UnitOfWork.HttpRefererRepository.Update(oOriginalHttpReferer);
				// **************************************************

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.HttpReferer.Index()));
			}

			return (View(httpReferer));
		}
	}
}
