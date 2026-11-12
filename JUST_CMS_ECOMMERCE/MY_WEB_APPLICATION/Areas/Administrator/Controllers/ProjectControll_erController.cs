using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.7
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.ProjectController)]
	public partial class ProjectControll_erController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ProjectControll_erController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index(System.Guid? projectAreaId)
		{
			// **************************************************
			if (projectAreaId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == projectAreaId.Value)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.ProjectArea = oProjectArea;
			// **************************************************

			var varIndexViewModels =
				UnitOfWork.ProjectControllerRepository
				.GetViewModelByRole(projectAreaId.Value, Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			return (View(varIndexViewModels));
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

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectController));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(System.Guid? projectAreaId)
		{
			// **************************************************
			if (projectAreaId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == projectAreaId.Value)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.ProjectArea = oProjectArea;
			// **************************************************

			// **************************************************
			Models.ProjectController oProjectController =
				new Models.ProjectController();

			oProjectController.ProjectAreaId = oProjectArea.Id;
			// **************************************************

			return (View(oProjectController));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind
				(Include = "Id,ProjectAreaId,Name,IsAvailableInSourceCode,IsVisibleJustForProgrammer,DisplayName,Description,MasterDataCode,IsSystem,Ordering,InsertDateTime,CreatorUserId,UpdateDateTime,EditorUserId,IsActive,ActiveDateTime,ActivatorUserId,IsVerified,VerifyDateTime,VerifierUserId,IsDeleted,DeleteDateTime,RemoverUserId")]
			Models.ProjectController projectController)
		{
			// **************************************************
			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == projectController.ProjectAreaId)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.ProjectArea = oProjectArea;
			// **************************************************

			// **************************************************
			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository
				.GetByProjectAreaIdAndName(projectController.ProjectAreaId, projectController.Name);

			if (oProjectController != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				projectController.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.ProjectControllerRepository.Insert(projectController);

				UnitOfWork.Save();

				return (RedirectToAction
					(MVC.Administrator.ProjectControll_er.Index(projectController.ProjectAreaId)));
			}

			return (View(projectController));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			// **************************************************
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			ViewBag.ProjectArea =
				oProjectController.ProjectArea;

			return (View(oProjectController));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind
				(Include = "Id,ProjectAreaId,Name,IsAvailableInSourceCode,IsVisibleJustForProgrammer,DisplayName,Description,MasterDataCode,IsSystem,Ordering,InsertDateTime,CreatorUserId,UpdateDateTime,EditorUserId,IsActive,ActiveDateTime,ActivatorUserId,IsVerified,VerifyDateTime,VerifierUserId,IsDeleted,DeleteDateTime,RemoverUserId")]
			Models.ProjectController projectController)
		{
			// **************************************************
			Models.ProjectController oOriginalProjectController =
				UnitOfWork.ProjectControllerRepository.GetById(projectController.Id);

			if (oOriginalProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			projectController.Name =
				projectController.Name.Replace(" ", string.Empty);

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository
				.GetByProjectAreaIdAndNameExceptId
				(oOriginalProjectController.ProjectAreaId, projectController.Name, projectController.Id);

			if (oProjectController != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalProjectController.Ordering = projectController.Ordering;
				oOriginalProjectController.DisplayName = projectController.DisplayName;
				oOriginalProjectController.Description = projectController.Description;
				// **************************************************

				// **************************************************
				oOriginalProjectController.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalProjectController.SetIsActive
					(projectController.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalProjectController.Name = projectController.Name;
					oOriginalProjectController.IsVisibleJustForProgrammer =
						projectController.IsVisibleJustForProgrammer;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ProjectControllerRepository.Update(oOriginalProjectController);

				UnitOfWork.Save();

				return (RedirectToAction
					(MVC.Administrator.ProjectControll_er.Index(oOriginalProjectController.ProjectAreaId)));
			}

			ViewBag.ProjectArea =
				oOriginalProjectController.ProjectArea;

			return (View(projectController));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectController));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
		{
			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			System.Guid sProjectAreaId =
				oProjectController.ProjectAreaId;

			UnitOfWork.ProjectControllerRepository.Delete(oProjectController);

			UnitOfWork.Save();

			return (RedirectToAction
				(MVC.Administrator.ProjectControll_er.Index(sProjectAreaId)));
		}
	}
}
