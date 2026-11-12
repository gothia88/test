using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/10/10
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.Group),
		resourceName: Resources.Models.Strings.GroupKeys.EntitiesName)]
	public partial class GroupController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public GroupController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			// دستور ذيل جنبه آموزشی دارد
			//var varGroups =
			//	UnitOfWork.GroupRepository
			//	.GetByCultureLcid(CultureLcid)
			//	.ToList()
			//	;

			var varGroups =
				UnitOfWork.GroupRepository
				.GetViewModelByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varGroups));
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

			Models.Group oGroup =
				UnitOfWork.GroupRepository
				.GetById(id.Value);

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.Group oGroup =
				new Models.Group();
			// **************************************************

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "Name,IsActive,Ordering,Description")]
			Models.Group group)
		{
			// **************************************************
			Models.Group oGroup =
				UnitOfWork.GroupRepository
				.GetByCultureLcidAndName(CultureLcid, group.Name);

			if (oGroup != null)
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
				group.CultureLcid = CultureLcid;

				group.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				group.SetIsActive
					(group.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GroupRepository.Insert(group);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Group.Index()));
			}

			return (View(group));
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

			Models.Group oGroup =
				UnitOfWork.GroupRepository
				.GetById(id.Value);

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Name,IsActive,Ordering,Description")]
			Models.Group group)
		{
			// **************************************************
			Models.Group oOriginalGroup =
				UnitOfWork.GroupRepository
				.GetById(group.Id);

			if (oOriginalGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			Models.Group oGroup =
				UnitOfWork.GroupRepository
				.GetByCultureLcidAndNameExceptId(CultureLcid, group.Name, group.Id);

			if (oGroup != null)
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
				oOriginalGroup.Name = group.Name;
				oOriginalGroup.Ordering = group.Ordering;
				oOriginalGroup.Description = group.Description;
				// **************************************************

				// **************************************************
				oOriginalGroup.CultureLcid = CultureLcid;

				oOriginalGroup.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalGroup.SetIsActive
					(group.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GroupRepository.Update(oOriginalGroup);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Group.Index()));
			}

			return (View(group));
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

			Models.Group oGroup =
				UnitOfWork.GroupRepository
				.GetById(id.Value);

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGroup));
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
			Models.Group oGroup =
				UnitOfWork.GroupRepository
				.GetById(id);

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			int intPageCount =
				UnitOfWork.GroupRepository
				.GetPageCount(oGroup);

			if (intPageCount != 0)
			{
				// از اين اطلاعات در جای ديگری استفاده شده است
				// لذا شما قادر به حذف آن نمی‌باشيد
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.Error101));

				return (View(oGroup));
			}
			// **************************************************

			// **************************************************
			int intUserCount =
				UnitOfWork.GroupRepository
				.GetUserCount(oGroup);

			if (intUserCount != 0)
			{
				// از اين اطلاعات در جای ديگری استفاده شده است
				// لذا شما قادر به حذف آن نمی‌باشيد
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.Error101));

				return (View(oGroup));
			}
			// **************************************************

			// **************************************************
			int intProjectActionCount =
				UnitOfWork.GroupRepository
				.GetProjectActionCount(oGroup);

			if (intProjectActionCount != 0)
			{
				// از اين اطلاعات در جای ديگری استفاده شده است
				// لذا شما قادر به حذف آن نمی‌باشيد
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.Error101));

				return (View(oGroup));
			}
			// **************************************************

			UnitOfWork.GroupRepository.Delete(oGroup);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.Group.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectProjectActionsForGroup)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/Group'>{0}</a>",
				Resources.Controllers.Administrator_Group);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oGroup.Id,
					name: oGroup.Name,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Resources.Models.Group.EntityName,
					selectedProjectActions: oGroup.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectProjectActionsForGroup)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oProjectActions =
				UnitOfWork.ProjectActionRepository.Get()
				.ToList()
				;

			oGroup.ProjectActions.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varProjectAction =
						oProjectActions
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varProjectAction != null)
					{
						oGroup.ProjectActions.Add(varProjectAction);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/Group'>{0}</a>",
				Resources.Controllers.Administrator_Group);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oGroup.Id,
					name: oGroup.Name,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Resources.Models.Group.EntityName,
					selectedProjectActions: oGroup.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetSpecial()
				.ToList()
				;

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oPages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.ToList()
				;

			oGroup.Pages.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varPage =
						oPages
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varPage != null)
					{
						oGroup.Pages.Add(varPage);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetSpecial()
				.ToList()
				;

			return (View(oGroup));
		}
	}
}
