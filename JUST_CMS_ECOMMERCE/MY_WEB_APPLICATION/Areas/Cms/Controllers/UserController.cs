using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1394/01/29
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.Cms.User),
		resourceName: Resources.Models.Cms.Strings.UserKeys.UserActions)]
	public partial class UserController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserController()
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

			if (oPost.IsDeleted)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.NotFound));

				return (oResult);
			}

			if (oPost.CreatorUserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.Unauthorized));

				return (oResult);
			}

			return (oResult);
		}

		private void SetViewBagsForDropDownLists(Models.Cms.Post post)
		{
			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveLeavesByCultureLcid(CultureLcid)
				.ToList()
				;

			if (post == null)
			{
				ViewBag.PostCategoryId =
					new System.Web.Mvc.SelectList
						(varPostCategories, "Id", "FullName");
			}
			else
			{
				ViewBag.PostCategoryId =
					new System.Web.Mvc.SelectList
						(varPostCategories, "Id", "FullName", post.PostCategoryId);
			}
		}
		#endregion /General

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.DisplayPosts)]
		public virtual System.Web.Mvc.ActionResult DisplayPosts()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.GetPosts)]
		public virtual System.Web.Mvc.JsonResult GetPosts()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varData =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetByCultureLcidAndCreatorUserId
				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.Id)
				;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Cms.User.GetPostsViewModel>(varData);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.DisplayPost)]
		public virtual System.Web.Mvc.ActionResult DisplayPost(System.Guid? id)
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

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.CreatePost)]
		public virtual System.Web.Mvc.ActionResult CreatePost()
		{
			// **************************************************
			Models.Cms.Post oPost =
				new Models.Cms.Post();

			oPost.Copyright =
				Infrastructure.Sessions.AuthenticatedUser.FullName;

			oPost.Author =
				Infrastructure.Sessions.AuthenticatedUser.FullName;
			// **************************************************

			SetViewBagsForDropDownLists(oPost);

			return (View(oPost));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.CreatePost)]
		public virtual System.Web.Mvc.ActionResult CreatePost(Models.Cms.Post post)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				Models.Cms.Post oNewPost =
					new Models.Cms.Post();

				oNewPost.Body = post.Body;
				oNewPost.Tags = post.Tags;
				oNewPost.Title = post.Title;
				oNewPost.Author = post.Author;
				oNewPost.IsDraft = post.IsDraft;
				oNewPost.ImageUrl = post.ImageUrl;
				oNewPost.Keywords = post.Keywords;
				oNewPost.Copyright = post.Copyright;
				oNewPost.Description = post.Description;
				oNewPost.Introduction = post.Introduction;
				oNewPost.Classification = post.Classification;
				oNewPost.PostCategoryId = post.PostCategoryId;
				oNewPost.DisplayCreatorUser = post.DisplayCreatorUser;
				oNewPost.IsCommentingEnabled = post.IsCommentingEnabled;
				oNewPost.DoesSearchEnginesIndexIt = post.DoesSearchEnginesIndexIt;
				oNewPost.DoesSearchEnginesFollowIt = post.DoesSearchEnginesFollowIt;
				oNewPost.StartPublishingDateTime.Value = post.StartPublishingDateTime.Value;
				oNewPost.FinishPublishingDateTime.Value = post.FinishPublishingDateTime.Value;

				oNewPost.AccessTypeId =
					Models.AccessType.GetByCultureAndCode
					(CultureLcid, Models.Enums.AccessTypes.Public).Id;
				// **************************************************

				// **************************************************
				oNewPost.CultureLcid = CultureLcid;

				oNewPost.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Insert(oNewPost);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.User.DisplayPosts()));
			}

			SetViewBagsForDropDownLists(post);

			return (View(post));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.EditPost)]
		public virtual System.Web.Mvc.ActionResult EditPost(System.Guid? id)
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
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.EditPost)]
		public virtual System.Web.Mvc.ActionResult EditPost(Models.Cms.Post post)
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

				oOriginalPost.AccessTypeId =
					Models.AccessType.GetByCultureAndCode
					(CultureLcid, Models.Enums.AccessTypes.Public).Id;
				// **************************************************

				// **************************************************
				oOriginalPost.CultureLcid = CultureLcid;

				oOriginalPost.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPost.SetIsActive
					(false, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Update(oOriginalPost);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.User.DisplayPosts()));
			}

			SetViewBagsForDropDownLists(post);

			return (View(post));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.DeletePost)]
		public virtual System.Web.Mvc.ActionResult DeletePost(System.Guid? id)
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
		[System.Web.Mvc.ActionName("DeletePost")]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.Cms.User),
			resourceName: Resources.Models.Cms.Strings.UserKeys.DeletePost)]
		public virtual System.Web.Mvc.ActionResult DeletePostConfirmed(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.Cms.Post>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}
			// **************************************************

			Models.Cms.Post oPost = oResult.Entity;

			oPost.SetIsDeleted
				(true, Infrastructure.Sessions.AuthenticatedUser.Id);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.User.DisplayPosts()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
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
			// **************************************************

			return (View(oResult.Entity));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("UploadFile")]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
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
			}

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);
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
			Models.User oCurrentUser =
				UnitOfWork.UserRepository.GetById
				(Infrastructure.Sessions.AuthenticatedUser.Id);

			long lngMaxFileLengthInKB =
				Infrastructure.Constants.MAX_FILE_LENGTH_FOR_UPLOADING_IN_KB;

			if (oCurrentUser.Plan != null)
			{
				lngMaxFileLengthInKB =
					oCurrentUser.Plan.MaxFileLengthForUploadingInMB * 1024;
			}
			// **************************************************

			// **************************************************
			oActionResult =
				Infrastructure.DownloadAndUploadManager.UploadFile
				(path: strPath,
				maxFileLengthInKB: lngMaxFileLengthInKB,
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
			accessType: Models.Enums.AccessTypes.Registered,
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

				return (View(viewName: MVC.Cms.User.Views.UploadFile, model: oPost));
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

			return (View(viewName: MVC.Cms.User.Views.UploadFile, model: oPost));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.Post),
			resourceName: Resources.Models.Cms.Strings.PostKeys.GetActiveUserPosts)]
		public virtual System.Web.Mvc.JsonResult GetActiveUserPosts(System.Guid? userId)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			if (userId.HasValue == false)
			{
				return (null);
			}

			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(userId.Value);

			if (oUser == null)
			{
				return (null);
			}

			if ((oUser.IsDeleted) ||
				(oUser.IsActive == false) ||
				(oUser.IsAccountTerminated))
			{
				return (null);
			}

			var varData =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetActiveUserPosts(CultureLcid, oUser.Id)
				;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Cms.Shared.GetActiveUserPostsViewModel>(varData);

			return (Json(varResult,
				System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
