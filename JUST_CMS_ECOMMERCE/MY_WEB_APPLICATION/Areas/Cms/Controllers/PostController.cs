using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.1.0
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Cms.Post)]
	public partial class PostController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PostController()
		{
		}

		#region General
		private Infrastructure.AccessToEntityResult<Models.Cms.Post>
			CheckAccessToEntity(System.Guid? id)
		{
			Infrastructure.AccessToEntityResult<Models.Cms.Post> oResult =
				new Infrastructure.AccessToEntityResult<Models.Cms.Post>();

			if (id.HasValue == false)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest));

				return (oResult);
			}

			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetById(id.Value);

			if (oPost == null)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.NotFound));

				return (oResult);
			}

			oResult.Entity = oPost;

			return (oResult);
		}

		private void SetViewBagsForDropDownLists(Models.Cms.Post post)
		{
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			var varLayouts =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveLeavesByCultureLcid(CultureLcid)
				.ToList()
				;

			if (post == null)
			{
				ViewBag.LayoutId =
					new System.Web.Mvc.SelectList
						(varLayouts, "Id", "FullName");

				ViewBag.AccessTypeId =
					new System.Web.Mvc.SelectList
						(varAccessTypes, "Id", "Name");

				ViewBag.PostCategoryId =
					new System.Web.Mvc.SelectList
						(varPostCategories, "Id", "FullName");
			}
			else
			{
				ViewBag.LayoutId =
					new System.Web.Mvc.SelectList
						(varLayouts, "Id", "FullName", post.LayoutId);

				ViewBag.AccessTypeId =
					new System.Web.Mvc.SelectList
						(varAccessTypes, "Id", "Name", post.AccessTypeId);

				ViewBag.PostCategoryId =
					new System.Web.Mvc.SelectList
						(varPostCategories, "Id", "FullName", post.PostCategoryId);
			}
		}
		#endregion /General

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.GetData)]
		public virtual System.Web.Mvc.JsonResult GetData()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varData =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetByCultureLcid(CultureLcid)
				;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Cms.Post.GetDataViewModel>(varData);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
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
			Models.Cms.Post oPost =
				new Models.Cms.Post();

			oPost.Author =
				Infrastructure.Sessions.AuthenticatedUser.FullName;

			oPost.Copyright =
				Infrastructure.Sessions.AuthenticatedUser.FullName;
			// **************************************************

			SetViewBagsForDropDownLists(oPost);

			return (View(oPost));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.Post post)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				post.CultureLcid = CultureLcid;

				post.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				post.SetIsActive
					(post.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				post.SetIsDeleted
					(post.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);

				post.SetIsVerified
					(post.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Insert(post);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Post.Index()));
			}

			SetViewBagsForDropDownLists(post);

			return (View(post));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
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
		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.Post post)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
				oResult = CheckAccessToEntity(post.Id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			Models.Cms.Post oOriginalPost = oResult.Entity;

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalPost.Body = post.Body;
				oOriginalPost.Tags = post.Tags;
				oOriginalPost.Title = post.Title;
				oOriginalPost.Author = post.Author;
				oOriginalPost.IsDraft = post.IsDraft;
				oOriginalPost.ImageUrl = post.ImageUrl;
				oOriginalPost.Keywords = post.Keywords;
				oOriginalPost.Copyright = post.Copyright;
				oOriginalPost.Description = post.Description;
				oOriginalPost.Introduction = post.Introduction;
				oOriginalPost.Classification = post.Classification;
				oOriginalPost.PostCategoryId = post.PostCategoryId;
				oOriginalPost.DisplayCreatorUser = post.DisplayCreatorUser;
				oOriginalPost.IsCommentingEnabled = post.IsCommentingEnabled;
				oOriginalPost.DoesSearchEnginesIndexIt = post.DoesSearchEnginesIndexIt;
				oOriginalPost.DoesSearchEnginesFollowIt = post.DoesSearchEnginesFollowIt;
				oOriginalPost.StartPublishingDateTime.Value = post.StartPublishingDateTime.Value;
				oOriginalPost.FinishPublishingDateTime.Value = post.FinishPublishingDateTime.Value;

				// ويژگی‌هايی که صرفا مسوولين پايگاه می‌توانند آنها را تغيير دهند
				oOriginalPost.Hits = post.Hits;
				oOriginalPost.LayoutId = post.LayoutId;
				oOriginalPost.Ordering = post.Ordering;
				oOriginalPost.Password = post.Password;
				oOriginalPost.IsFeatured = post.IsFeatured;
				oOriginalPost.AccessTypeId = post.AccessTypeId;
				// **************************************************

				// **************************************************
				oOriginalPost.CultureLcid = CultureLcid;

				oOriginalPost.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPost.SetIsActive
					(post.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPost.SetIsDeleted
					(post.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPost.SetIsVerified
					(post.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Update(oOriginalPost);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Post.Index()));
			}

			SetViewBagsForDropDownLists(post);

			return (View(post));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
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
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			UnitOfWork.CmsUnitOfWork.PostRepository.Delete(oResult.Entity);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.Post.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.UploadFile)]
		public virtual System.Web.Mvc.ActionResult UploadFile(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.Cms.Post oPost = oResult.Entity;
			// **************************************************

			return (View(model: oPost));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("UploadFile")]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.UploadFile)]
		public virtual System.Web.Mvc.ActionResult UploadFileConfirmed(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.Cms.Post oPost = oResult.Entity;
			// **************************************************

			// **************************************************
			string strParentPath =
				Server.MapPath("~/App_Data");

			Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
			// **************************************************

			// **************************************************
			string strPath =
				string.Format("{0}\\PostFiles", strParentPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);

				Dtx.IO.Directory.SetFullControlForEveryOne(strPath);
			}
			// **************************************************

			Infrastructure.DownloadAndUploadManager.ActionResult oActionResult = null;

			// **************************************************
			string strOldExtension =
				oPost.AttachmentExtension;

			string strOldPathName =
				string.Format("{0}\\{1}{2}",
				strPath, oPost.Id, strOldExtension);

			oActionResult =
				Infrastructure.DownloadAndUploadManager.DeleteFile(strOldPathName);

			if (oActionResult.State == Infrastructure.DownloadAndUploadManager.ActionResultStates.Error)
			{
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, oActionResult.Message));

				return (View(model: oPost));
			}
			// **************************************************

			// **************************************************
			oActionResult =
				Infrastructure.DownloadAndUploadManager.UploadFile
				(path: strPath,
				fileNameWithoutExtension: oPost.Id.ToString());

			if (oActionResult.State == Infrastructure.DownloadAndUploadManager.ActionResultStates.Error)
			{
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, oActionResult.Message));

				return (View(model: oPost));
			}
			// **************************************************

			// **************************************************
			System.IO.FileInfo oFileInfo =
				new System.IO.FileInfo(oActionResult.PathName);

			oPost.HasAttachment = true;
			oPost.AttachmentLength = oFileInfo.Length;
			oPost.AttachmentExtension = oActionResult.FileExtension;

			oPost.Sha1OfAttachment =
				Dtx.Security.Hashing.GetSha1ByPathName(oActionResult.PathName);

			oPost.SetUpdateDateTime
				(Infrastructure.Sessions.AuthenticatedUser.Id);

			UnitOfWork.CmsUnitOfWork.PostRepository.Update(oPost);

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, oActionResult.Message));
			// **************************************************

			return (View(model: oPost));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.DeleteFile)]
		public virtual System.Web.Mvc.ActionResult DeleteFile(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.Cms.Post oPost = oResult.Entity;
			// **************************************************

			// **************************************************
			string strParentPath =
				Server.MapPath("~/App_Data");

			Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
			// **************************************************

			// **************************************************
			string strPath =
				string.Format("{0}\\PostFiles", strParentPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
			}
			// **************************************************

			// **************************************************
			string strPathName =
				string.Format("{0}\\{1}{2}",
				strPath, oPost.Id, oPost.AttachmentExtension);

			Infrastructure.DownloadAndUploadManager.ActionResult oActionResult =
				Infrastructure.DownloadAndUploadManager.DeleteFile(strParentPath);

			if (oActionResult.State == Infrastructure.DownloadAndUploadManager.ActionResultStates.Error)
			{
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, oActionResult.Message));

				return (View(viewName: MVC.Cms.Post.Views.UploadFile, model: oPost));
			}

			// **************************************************
			oPost.AttachmentLength = 0;
			oPost.HasAttachment = false;
			oPost.Sha1OfAttachment = null;
			oPost.AttachmentExtension = null;

			oPost.SetUpdateDateTime
				(Infrastructure.Sessions.AuthenticatedUser.Id);

			UnitOfWork.CmsUnitOfWork.PostRepository.Update(oPost);

			UnitOfWork.Save();
			// **************************************************

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, oActionResult.Message));

			return (View(viewName: MVC.Cms.Post.Views.UploadFile, model: oPost));
		}
	}
}
