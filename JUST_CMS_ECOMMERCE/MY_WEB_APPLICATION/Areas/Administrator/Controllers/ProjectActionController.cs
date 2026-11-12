using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.7
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.ProjectAction)]
	public partial class ProjectActionController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ProjectActionController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index(System.Guid? projectControllerId)
		{
			// **************************************************
			if (projectControllerId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == projectControllerId.Value)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.ProjectController = oProjectController;
			// **************************************************

			var varProjectActions =
				UnitOfWork.ProjectActionRepository
				.GetByProjectControllerIdAndRole
				(projectControllerId.Value, Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			return (View(varProjectActions));
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

			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectAction));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(System.Guid? projectControllerId)
		{
			// **************************************************
			if (projectControllerId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == projectControllerId.Value)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.ProjectController = oProjectController;
			// **************************************************

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
			// **************************************************

			// **************************************************
			Models.ProjectAction oProjectAction =
				new Models.ProjectAction();

			oProjectAction.ProjectControllerId = projectControllerId.Value;
			// **************************************************

			return (View(oProjectAction));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "Id,ProjectControllerId,LayoutId,AccessTypeId,Name,IsAvailableInSourceCode,IsVisibleJustForProgrammer,DisplayName,Description,MasterDataCode,IsSystem,Ordering,InsertDateTime,CreatorUserId,UpdateDateTime,EditorUserId,IsActive,ActiveDateTime,ActivatorUserId,IsVerified,VerifyDateTime,VerifierUserId,IsDeleted,DeleteDateTime,RemoverUserId")]
			Models.ProjectAction projectAction)
		{
			// **************************************************
			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Id == projectAction.ProjectControllerId)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.ProjectController = oProjectController;
			// **************************************************

			// **************************************************
			projectAction.Name =
				projectAction.Name.Replace(" ", string.Empty);

			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository
				.GetByProjectControllerIdAndName
				(projectAction.ProjectControllerId, projectAction.Name);

			if (oProjectAction != null)
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
				projectAction.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.ProjectActionRepository.Insert(projectAction);

				UnitOfWork.Save();

				return (RedirectToAction
					(MVC.Administrator.ProjectAction.Index(projectAction.ProjectControllerId)));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
			// **************************************************

			return (View(projectAction));
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

			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			ViewBag.ProjectController =
				oProjectAction.ProjectController;

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", oProjectAction.AccessTypeId);
			// **************************************************

			return (View(oProjectAction));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,ProjectControllerId,LayoutId,AccessTypeId,Name,IsAvailableInSourceCode,IsVisibleJustForProgrammer,DisplayName,Description,MasterDataCode,IsSystem,Ordering,InsertDateTime,CreatorUserId,UpdateDateTime,EditorUserId,IsActive,ActiveDateTime,ActivatorUserId,IsVerified,VerifyDateTime,VerifierUserId,IsDeleted,DeleteDateTime,RemoverUserId")]
			Models.ProjectAction projectAction)
		{
			// **************************************************
			Models.ProjectAction oOriginalProjectAction =
				UnitOfWork.ProjectActionRepository.GetById(projectAction.Id);

			if (oOriginalProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			projectAction.Name =
				projectAction.Name.Replace(" ", string.Empty);

			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository
				.GetByProjectControllerIdAndNameExceptId
				(oOriginalProjectAction.ProjectControllerId, projectAction.Name, projectAction.Id);

			if (oProjectAction != null)
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
				oOriginalProjectAction.Ordering = projectAction.Ordering;
				oOriginalProjectAction.DisplayName = projectAction.DisplayName;
				oOriginalProjectAction.Description = projectAction.Description;
				oOriginalProjectAction.AccessTypeId = projectAction.AccessTypeId;
				// **************************************************

				// **************************************************
				oOriginalProjectAction.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalProjectAction.SetIsActive(projectAction.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalProjectAction.Name = projectAction.Name;
					oOriginalProjectAction.IsVisibleJustForProgrammer =
						projectAction.IsVisibleJustForProgrammer;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ProjectActionRepository.Update(oOriginalProjectAction);

				UnitOfWork.Save();

				return (RedirectToAction
					(MVC.Administrator.ProjectAction.Index
					(oOriginalProjectAction.ProjectControllerId)));
			}

			ViewBag.ProjectController =
				oOriginalProjectAction.ProjectController;

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", projectAction.AccessTypeId);
			// **************************************************

			return (View(projectAction));
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

			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectAction));
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
			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			System.Guid sProjectControllerId =
				oProjectAction.ProjectControllerId;

			UnitOfWork.ProjectActionRepository.Delete(oProjectAction);

			UnitOfWork.Save();

			return (RedirectToAction
				(MVC.Administrator.ProjectAction.Index(sProjectControllerId)));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups(System.Guid id)
		{
			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Groups =
				UnitOfWork.GroupRepository.Get()
				.OrderBy(current => current.Name)
				.ToList()
				;

			return (View(oProjectAction));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oGroups =
				UnitOfWork.GroupRepository.Get()
				.OrderBy(current => current.Name)
				.ToList()
				;

			oProjectAction.Groups.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varGroup =
						oGroups
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varGroup != null)
					{
						oProjectAction.Groups.Add(varGroup);
					}
				}
			}

			UnitOfWork.Save();

			ViewBag.Groups = oGroups;

			return (View(oProjectAction));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectUsers)]
		public virtual System.Web.Mvc.ActionResult SelectUsers(System.Guid id)
		{
			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Users =
				UnitOfWork.UserRepository.Get()
				.OrderBy(current => current.Gender.Name2)
				.ThenBy(current => current.FirstName)
				.ThenBy(current => current.LastName)
				.ToList()
				;

			return (View(oProjectAction));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectUsers)]
		public virtual System.Web.Mvc.ActionResult SelectUsers
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectAction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oUsers =
				UnitOfWork.UserRepository.Get()
				.OrderBy(current => current.Gender.Name2)
				.ThenBy(current => current.FirstName)
				.ThenBy(current => current.LastName)
				.ToList()
				;

			oProjectAction.Users.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varUser =
						oUsers
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varUser != null)
					{
						oProjectAction.Users.Add(varUser);
					}
				}
			}

			UnitOfWork.Save();

			ViewBag.Users = oUsers;

			return (View(oProjectAction));
		}
	}
}
