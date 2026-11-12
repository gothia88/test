using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1392/12/19
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.Role)]
	public partial class RoleController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public RoleController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varRoles =
				UnitOfWork.RoleRepository.GetByCultureLcidAndLessThanOrEqualToCode
				(CultureLcid, (int)Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			return (View(varRoles));
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

			Models.Role oRole =
				UnitOfWork.RoleRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oRole == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oRole));
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

			Models.Role oRole =
				UnitOfWork.RoleRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oRole == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oRole));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Name,Description,IsActive,Ordering")]
			Models.Role role)
		{
			// **************************************************
			Models.Role oOriginalRole =
				UnitOfWork.RoleRepository.GetById(role.Id);

			if (oOriginalRole == null)
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
				oOriginalRole.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalRole.Name = role.Name;
				oOriginalRole.Ordering = role.Ordering;
				oOriginalRole.Description = role.Description;
				// **************************************************

				// **************************************************
				oOriginalRole.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalRole.SetIsActive(role.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.RoleRepository.Update(oOriginalRole);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Role.Index()));
			}

			return (View(role));
		}
	}
}
