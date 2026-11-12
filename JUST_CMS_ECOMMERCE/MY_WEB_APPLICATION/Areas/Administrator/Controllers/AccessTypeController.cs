using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/06/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.AccessType)]
	public partial class AccessTypeController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public AccessTypeController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varAccessTypes));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.AccessType oAccessType =
				UnitOfWork.AccessTypeRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oAccessType == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oAccessType));
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

			Models.AccessType oAccessType =
				UnitOfWork.AccessTypeRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oAccessType == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oAccessType));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
		([System.Web.Mvc.Bind(Include = "Id,IsActive,Ordering,Name,Description")]
		Models.AccessType accessType)
		{
			// **************************************************
			Models.AccessType oOriginalAccessType =
				UnitOfWork.AccessTypeRepository.GetById(accessType.Id);

			if (oOriginalAccessType == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalAccessType.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalAccessType.Name = accessType.Name;
				oOriginalAccessType.Ordering = accessType.Ordering;
				oOriginalAccessType.Description = accessType.Description;
				// **************************************************

				// **************************************************
				oOriginalAccessType.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalAccessType.SetIsActive(accessType.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.AccessTypeRepository.Update(oOriginalAccessType);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.AccessType.Index()));
			}

			return (View(accessType));
		}
	}
}
