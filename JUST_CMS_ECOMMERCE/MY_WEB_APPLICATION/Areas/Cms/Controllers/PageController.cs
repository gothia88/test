using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Cms.Page)]
	public partial class PageController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PageController()
		{
		}

		#region General
		private Infrastructure.AccessToEntityResult<Models.Cms.Page>
			CheckAccessToEntity(System.Guid? id)
		{
			Infrastructure.AccessToEntityResult<Models.Cms.Page> oResult =
				new Infrastructure.AccessToEntityResult<Models.Cms.Page>();

			if (id.HasValue == false)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest));

				return (oResult);
			}

			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetById(id.Value);

			if (oPage == null)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.NotFound));

				return (oResult);
			}

			oResult.Entity = oPage;

			return (oResult);
		}

		private void SetViewBagsForDropDownLists(Models.Cms.Page page)
		{
			var varLayouts =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			if (page == null)
			{
				ViewBag.LayoutId =
					new System.Web.Mvc.SelectList
						(varLayouts, "Id", "FullName");

				ViewBag.AccessTypeId =
					new System.Web.Mvc.SelectList
						(varAccessTypes, "Id", "Name");
			}
			else
			{
				ViewBag.LayoutId =
					new System.Web.Mvc.SelectList
						(varLayouts, "Id", "FullName", page.LayoutId);

				ViewBag.AccessTypeId =
					new System.Web.Mvc.SelectList
						(varAccessTypes, "Id", "Name", page.AccessTypeId);
			}
		}
		#endregion /General

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varPages =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetViewModelByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varPages));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			return (View(oResult.Entity));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.Cms.Page oPage =
				new Models.Cms.Page();

			oPage.Author =
				Infrastructure.Sessions.AuthenticatedUser.FullName;

			oPage.Copyright =
				Infrastructure.Sessions.AuthenticatedUser.FullName;
			// **************************************************

			SetViewBagsForDropDownLists(oPage);

			return (View(oPage));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.Page page)
		{
			// **************************************************
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetByCultureLcidAndName(CultureLcid, page.Name);

			if (oPage != null)
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
				page.CultureLcid = CultureLcid;

				page.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				page.SetIsActive
					(page.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				page.SetIsDeleted
					(page.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);

				page.SetIsVerified
					(page.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer)
				{
					page.IsSystem = false;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PageRepository.Insert(page);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Page.Index()));
			}

			SetViewBagsForDropDownLists(page);

			return (View(page));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			SetViewBagsForDropDownLists(oResult.Entity);

			return (View(oResult.Entity));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.Page page)
		{
			// **************************************************
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetByCultureLcidAndNameExceptId(CultureLcid, page.Name, page.Id);

			if (oPage != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(page.Id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			Models.Cms.Page oOriginalPage = oResult.Entity;

			if (oOriginalPage.PageContent == null)
			{
				oOriginalPage.PageContent =
					new Models.Cms.PageContent();
			}

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalPage.Hits = page.Hits;
				oOriginalPage.Tags = page.Tags;
				oOriginalPage.Title = page.Title;
				oOriginalPage.Author = page.Author;
				oOriginalPage.Keywords = page.Keywords;
				oOriginalPage.LayoutId = page.LayoutId;
				oOriginalPage.Ordering = page.Ordering;
				oOriginalPage.Copyright = page.Copyright;
				oOriginalPage.Description = page.Description;
				oOriginalPage.AccessTypeId = page.AccessTypeId;
				oOriginalPage.Classification = page.Classification;
				oOriginalPage.StartPublishingDateTime = page.StartPublishingDateTime;
				oOriginalPage.DoesSearchEnginesIndexIt = page.DoesSearchEnginesIndexIt;
				oOriginalPage.FinishPublishingDateTime = page.FinishPublishingDateTime;
				oOriginalPage.DoesSearchEnginesFollowIt = page.DoesSearchEnginesFollowIt;

				//if (oOriginalPage.AccessTypeEnum != Models.Enums.AccessTypes.Special)
				//{
				//	if ((oOriginalPage.Users != null) &&
				//		(oOriginalPage.Users.Count > 0))
				//	{
				//		oOriginalPage.Users.Clear();
				//	}

				//	if ((oOriginalPage.Groups != null) &&
				//		(oOriginalPage.Groups.Count > 0))
				//	{
				//		oOriginalPage.Groups.Clear();
				//	}
				//}

				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalPage.Name = page.Name;
					oOriginalPage.IsSystem = page.IsSystem;
				}
				// **************************************************

				// **************************************************
				oOriginalPage.CultureLcid = CultureLcid;

				oOriginalPage.PageContent.Content = page.PageContent.Content;

				oOriginalPage.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPage.SetIsActive
					(page.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPage.SetIsDeleted
					(page.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPage.SetIsVerified
					(page.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PageRepository.Update(oOriginalPage);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Page.Index()));
			}

			SetViewBagsForDropDownLists(page);

			return (View(page));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			return (View(oResult.Entity));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			UnitOfWork.CmsUnitOfWork.PageRepository.Delete(oResult.Entity);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.Page.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			ViewBag.Groups =
				UnitOfWork.GroupRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(oResult.Entity));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups
			(System.Guid? id, System.Guid[] checkBoxes)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Page>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			Models.Cms.Page oPage = oResult.Entity;

			var oGroups =
				UnitOfWork.GroupRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			oPage.Groups.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					Models.Group oGroup =
						oGroups
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (oGroup != null)
					{
						oPage.Groups.Add(oGroup);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Groups = oGroups;

			return (View(oPage));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.EditLocalizedPageContent)]
		public virtual System.Web.Mvc.ActionResult EditLocalizedPageContent
			(string areaName, string controllerName, string actionName)
		{
			// **************************************************
			string strFileName = string.Empty;
			if (string.IsNullOrEmpty(areaName))
			{
				strFileName =
					string.Format("{0}_{1}.{2}.htm",
					controllerName, actionName, CultureName);
			}
			else
			{
				strFileName =
					string.Format("{0}_{1}_{2}.{3}.htm",
					areaName, controllerName, actionName, CultureName);
			}

			string strRootRelativePathName = "~/App_Data";

			string strPath =
				Server.MapPath(strRootRelativePathName);

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			strPath =
				string.Format("{0}\\LocalizedPageContents", strPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);
			}

			string strPathName =
				string.Format("{0}\\{1}", strPath, strFileName);

			string strLocalizedPageContent = Dtx.IO.File.Read(strPathName);
			// **************************************************

			// **************************************************
			ViewModels.Areas.Cms.Page.EditLocalizedPageContentViewModel oEditLocalizedPageContentViewModel =
				new ViewModels.Areas.Cms.Page.EditLocalizedPageContentViewModel();

			oEditLocalizedPageContentViewModel.AreaName = areaName;
			oEditLocalizedPageContentViewModel.ActionName = actionName;
			oEditLocalizedPageContentViewModel.ControllerName = controllerName;

			oEditLocalizedPageContentViewModel.LocalizedPageContent = strLocalizedPageContent;
			// **************************************************

			return (View(oEditLocalizedPageContentViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.EditLocalizedPageContent)]
		public virtual System.Web.Mvc.ActionResult EditLocalizedPageContent
			(ViewModels.Areas.Cms.Page.EditLocalizedPageContentViewModel viewModel)
		{
			// **************************************************
			string strFileName = string.Empty;
			if (string.IsNullOrEmpty(viewModel.AreaName))
			{
				strFileName =
					string.Format("{0}_{1}.{2}.htm",
					viewModel.ControllerName, viewModel.ActionName, CultureName);
			}
			else
			{
				strFileName =
					string.Format("{0}_{1}_{2}.{3}.htm",
					viewModel.AreaName, viewModel.ControllerName, viewModel.ActionName, CultureName);
			}

			string strRootRelativePathName = "~/App_Data";

			string strPath =
				Server.MapPath(strRootRelativePathName);

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			strPath =
				string.Format("{0}\\LocalizedPageContents", strPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);
			}

			string strPathName =
				string.Format("{0}\\{1}", strPath, strFileName);

			Dtx.IO.File.Overwrite(strPathName, viewModel.LocalizedPageContent);
			// **************************************************

			// **************************************************
			// محتوای صفحه با موفقيت ذخيره گرديد
			string strInformationMessage =
				Resources.Models.Cms.Page.Information001;

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
			// **************************************************

			return (View(viewModel));
		}
	}
}
