using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/06/03
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.Culture)]
	public partial class CultureController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public CultureController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varCultures =
				UnitOfWork.CultureRepository.Get()
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				.ToList()
				;

			return (View(varCultures));
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

			Models.Culture oCulture =
				UnitOfWork.CultureRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCulture == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCulture));
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

			Models.Culture oCulture =
				UnitOfWork.CultureRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCulture == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCulture));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.Culture culture)
		{
			// **************************************************
			Models.Culture oOriginalCulture =
				UnitOfWork.CultureRepository.GetById(culture.Id);

			if (oOriginalCulture == null)
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
				oOriginalCulture.Ordering = culture.Ordering;
				oOriginalCulture.DisplayName = culture.DisplayName;
				oOriginalCulture.Description = culture.Description;
				// **************************************************

				// **************************************************
				oOriginalCulture.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalCulture.SetIsActive(culture.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CultureRepository.Update(oOriginalCulture);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Culture.Index()));
			}

			return (View(culture));
		}
	}
}
