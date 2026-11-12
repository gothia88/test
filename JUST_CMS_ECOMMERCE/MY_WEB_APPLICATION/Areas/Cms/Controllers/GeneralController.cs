using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.8
	/// Update Date: 1394/03/09
	/// Changed In Version: 1.9.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.Cms.General),
		resourceName: Resources.Models.Cms.Strings.GeneralKeys.GeneralActions)]
	public partial class GeneralController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public GeneralController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.GenerateRandomData)]
		public virtual System.Web.Mvc.ViewResult GenerateRandomData()
		{
			Models.Cms.CmsSettings oSettings =
				Infrastructure.Cms.Settings.Instance;

			if ((oSettings != null) &&
				(oSettings.IsRandomDataGenerated))
			{
				// **************************************************
				// اطلاعات تصادفی (تستی) قبلا ايجاد شده است
				Infrastructure.PageMessage oPageMessage =
					new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, Resources.Messages.Error200);

				PageMessages.Add(oPageMessage);
				// **************************************************
			}
			else
			{
				if (Models.Cms.Utility.GenerateRandomData())
				{
					// **************************************************
					// اطلاعات تصادفی (تستی) با موفقيت ايجاد گرديد
					Infrastructure.PageMessage oPageMessage =
						new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Information007);

					PageMessages.Add(oPageMessage);
					// **************************************************
				}
				else
				{
					// **************************************************
					Infrastructure.PageMessage oPageMessage =
						new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, Resources.Messages.UnexpectedError);

					PageMessages.Add(oPageMessage);
					// **************************************************
				}
			}

			return (View());
		}

		/// <summary>
		/// Version: 1.0.8
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.DisplayPage)]
		public virtual System.Web.Mvc.ActionResult DisplayPage(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			name = name.Replace(" ", string.Empty);

			if (name == string.Empty)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetByCultureLcidAndName(CultureLcid, name);

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			ViewBag.Title = oPage.Title;
			ViewBag.Author = oPage.Author;
			ViewBag.Keywords = oPage.Keywords;
			ViewBag.Copyright = oPage.Copyright;
			ViewBag.Description = oPage.Description;
			ViewBag.Classification = oPage.Classification;

			if (oPage.DoesSearchEnginesIndexIt)
			{
				if (oPage.DoesSearchEnginesFollowIt)
				{
					ViewBag.Robots = "index,follow";
				}
				else
				{
					ViewBag.Robots = "index,nofollow";
				}
			}
			else
			{
				if (oPage.DoesSearchEnginesFollowIt)
				{
					ViewBag.Robots = "noindex,follow";
				}
				else
				{
					ViewBag.Robots = "noindex,nofollow";
				}
			}
			// **************************************************

			if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			{
				if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
				{
					// در اين حالت، دستورات ذيل نبايد اجرا شوند
					//oPage.Hits++;

					//UnitOfWork.Save();

					return (View(model: oPage));
				}
			}

			if (oPage.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if ((oPage.Layout != null) && (oPage.Layout.IsActive == false))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPage.StartPublishingDateTime.Value.HasValue)
			{
				if (Models.Utility.Now < oPage.StartPublishingDateTime.Value.Value)
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
				}
			}

			if (oPage.FinishPublishingDateTime.Value.HasValue)
			{
				if (Models.Utility.Now > oPage.FinishPublishingDateTime.Value.Value)
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
				}
			}

			switch (oPage.AccessTypeEnum)
			{
				case Models.Enums.AccessTypes.Registered:
				{
					if (Infrastructure.AuthenticatedUser.IsAuthenticated == false)
					{
						string strReturnUrl =
							string.Format("/Page/{0}", oPage.Name);

						return (RedirectToAction(MVC.Account.Login(strReturnUrl)));
					}

					break;
				}

				case Models.Enums.AccessTypes.Special:
				{
					if (Infrastructure.AuthenticatedUser.IsAuthenticated == false)
					{
						string strReturnUrl =
							string.Format("/Page/{0}", oPage.Name);

						return (RedirectToAction(MVC.Account.Login(strReturnUrl)));
					}

					// دستور ذيل تعداد گروه‌هايی را برمی‌گرداند که فعال بوده
					// و کاربر جاری در آن عضو بوده
					// و در همان گروه‌ها، اين صفحه معرفی شده است
					int intCount =
						UnitOfWork.GroupRepository.Get()
						.Where(current => current.IsActive)
						.Where(current => current.Pages.Any
							(page => page.Id == oPage.Id))
						.Where(current => current.Users.Any
							(user => user.Id == Infrastructure.Sessions.AuthenticatedUser.Id))
						.Count();

					if (intCount == 0)
					{
						// دستور ذيل شناسايی می کند که
						// آيا کاربر مستقيما به صفحه دسترسی دارد يا خير؟
						intCount =
							UnitOfWork.UserRepository.Get()
							.Where(current => current.Id ==
								Infrastructure.Sessions.AuthenticatedUser.Id)
							.Where(current => current.Pages.Any
								(page => page.Id == oPage.Id))
							.Count();

						if (intCount == 0)
						{
							return (RedirectToAction
								(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
						}
					}

					break;
				}
			}

			// **************************************************
			oPage.Hits++;

			UnitOfWork.Save();
			// **************************************************

			return (View(model: oPage));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.DisplayPostCategory)]
		public virtual System.Web.Mvc.ActionResult DisplayPostCategory(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id.Value);

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((oPostCategory.IsActive == false) || (oPostCategory.IsDeleted))
			{
				if (Infrastructure.AuthenticatedUser.IsAuthenticated)
				{
					if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User)
					{
						return (RedirectToAction
							(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
					}
				}
				else
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
				}
			}

			oPostCategory.Hits++;

			UnitOfWork.Save();

			return (View(oPostCategory));
		}

		/// <summary>
		/// Version: 1.0.8
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.DisplayPost)]
		public virtual System.Web.Mvc.ActionResult DisplayPost(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetById(id.Value);

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			ViewBag.Title = oPost.Title;
			ViewBag.Author = oPost.Author;
			ViewBag.Keywords = oPost.Keywords;
			ViewBag.Copyright = oPost.Copyright;
			ViewBag.Description = oPost.Description;
			ViewBag.Classification = oPost.Classification;

			if (oPost.DoesSearchEnginesIndexIt)
			{
				if (oPost.DoesSearchEnginesFollowIt)
				{
					ViewBag.Robots = "index,follow";
				}
				else
				{
					ViewBag.Robots = "index,nofollow";
				}
			}
			else
			{
				if (oPost.DoesSearchEnginesFollowIt)
				{
					ViewBag.Robots = "noindex,follow";
				}
				else
				{
					ViewBag.Robots = "noindex,nofollow";
				}
			}
			// **************************************************

			if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			{
				if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
				{
					return (View(model: oPost));
				}
				else
				{
					if (oPost.CreatorUserId == Infrastructure.Sessions.AuthenticatedUser.Id)
					{
						return (View(model: oPost));
					}
				}
			}

			// اگر کاربر لاگين نکرده بود
			// يا
			// اگر کاربر لاگين کرده بود و مسوول پايگاه نبود و مطلب را خودش وارد نکرده بود

			if ((oPost.IsDraft) ||
				(oPost.IsDeleted) ||
				(oPost.IsActive == false) ||

				(oPost.PostCategory.IsDeleted) ||
				(oPost.PostCategory.IsActive == false) ||

				(oPost.CreatorUser.IsDeleted) ||
				(oPost.CreatorUser.IsActive == false) ||
				(oPost.CreatorUser.IsAccountTerminated))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			oPost.Hits++;

			UnitOfWork.Save();

			return (View(model: oPost));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.DisplayPostCategories)]
		public virtual System.Web.Mvc.ViewResult DisplayPostCategories()
		{
			var var‌PostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varPostCategories));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.DisplayPosts)]
		public virtual System.Web.Mvc.ActionResult DisplayPosts()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.GetPosts)]
		public virtual System.Web.Mvc.PartialViewResult GetPosts(int pageIndex = 0)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			Models.Cms.CmsSettings oSettings =
				Infrastructure.Cms.Settings.Instance;

			int intTake =
				oSettings.PostsPageSizeOnPostsPage;

			int intSkip = pageIndex * intTake;

			var varPosts =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetActiveByCultureLcid(CultureLcid, intSkip, intTake)
				.ToList()
				;

			if (varPosts.Count == 0)
			{
				return (null);
			}
			else
			{
				ViewBag.DisplayPostCategory = true;

				return (PartialView
					(viewName: MVC.Cms.Shared.Views._Partial_DisplayPosts, model: varPosts));
			}
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.GetPostsForHomePage)]
		public virtual System.Web.Mvc.PartialViewResult GetPostsForHomePage(int pageIndex = 0)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			Models.Cms.CmsSettings oSettings =
				Infrastructure.Cms.Settings.Instance;

			int intTake =
				oSettings.PostsPageSizeOnHomePage;

			int intSkip = pageIndex * intTake;

			var varPosts =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetActiveByCultureLcid(CultureLcid, intSkip, intTake)
				.ToList()
				;

			if (varPosts.Count == 0)
			{
				return (null);
			}
			else
			{
				ViewBag.DisplayPostCategory = true;

				return (PartialView
					(viewName: MVC.Cms.Shared.Views._Partial_DisplayPosts, model: varPosts));
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.DisplayPostsOfPostCategory)]
		public virtual System.Web.Mvc.ActionResult DisplayPostsOfPostCategory(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id.Value);

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((oPostCategory.IsActive == false) || (oPostCategory.IsDeleted))
			{
				if (Infrastructure.AuthenticatedUser.IsAuthenticated)
				{
					if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User)
					{
						return (RedirectToAction
							(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
					}
				}
				else
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
				}
			}

			return (View(oPostCategory));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.Cms.General),
			resourceName: Resources.Models.Cms.Strings.GeneralKeys.GetPostsOfPostCategory)]
		public virtual System.Web.Mvc.PartialViewResult
			GetPostsOfPostCategory(System.Guid? id, int pageIndex = 0)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			if (id.HasValue == false)
			{
				return (null);
			}

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id.Value);

			if (oPostCategory == null)
			{
				return (null);
			}

			if ((oPostCategory.IsActive == false) || (oPostCategory.IsDeleted))
			{
				if (Infrastructure.AuthenticatedUser.IsAuthenticated)
				{
					if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User)
					{
						return (null);
					}
				}
				else
				{
					return (null);
				}
			}

			Models.Cms.CmsSettings oSettings =
				Infrastructure.Cms.Settings.Instance;

			int intTake =
				oSettings.PostsPageSizeOnPostCategoryPage;

			int intSkip = pageIndex * intTake;

			var varPosts =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetActiveByCultureLcidAndPostCategoryId(CultureLcid, oPostCategory.Id, intSkip, intTake)
				.ToList()
				;

			if (varPosts.Count == 0)
			{
				return (null);
			}
			else
			{
				ViewBag.DisplayPostCategory = false;

				return (PartialView
					(viewName: MVC.Cms.Shared.Views._Partial_DisplayPosts, model: varPosts));
			}
		}

		/// <summary>
		/// Version: 1.0.7
		/// Update Date: 1394/02/05
		/// Changed In Version: 1.9.0
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.General),
			resourceName: Resources.Models.Strings.GeneralKeys.Download)]
		public virtual System.Web.Mvc.ActionResult Download(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			// **************************************************
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetById(id.Value);

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (BLL.Post.IsAllowedForDisplaying(oPost) == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oPost.HasAttachment == false)
			{
				string strErrorMessage =
					"فايل پيوستی برای اين مطلب وجود ندارد!";

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}
			// **************************************************

			// **************************************************
			Models.User oDownloaderUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oDownloaderUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			bool blnIsDownloaderSiteResponsibleOrOwner = false;

			if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
			{
				blnIsDownloaderSiteResponsibleOrOwner = true;
			}
			else
			{
				if (Infrastructure.Sessions.AuthenticatedUser.Id == oPost.CreatorUserId)
				{
					blnIsDownloaderSiteResponsibleOrOwner = true;
				}
			}
			// **************************************************

			// **************************************************
			string strFileName = oPost.FileName;

			string strRootRelativePath = "~/App_Data/PostFiles";

			string strPath =
				Server.MapPath(strRootRelativePath);
			// **************************************************

			// **************************************************
			// **************************************************
			// **************************************************
			// سرعت نامحدود
			int intMaxDownloadSpeedInKB = 0;

			if (blnIsDownloaderSiteResponsibleOrOwner == false)
			{
				// **************************************************
				oPost.DownloadCount++;

				UnitOfWork.Save();
				// **************************************************

				if (oDownloaderUser.Plan == null)
				{
					intMaxDownloadSpeedInKB = 8;
				}
				else
				{
					intMaxDownloadSpeedInKB =
						oDownloaderUser.Plan.MaxDownloadSpeedInKB;
				}
			}
			// **************************************************
			// **************************************************
			// **************************************************

			// **************************************************
			bool blnResult =
				Infrastructure.DownloadAndUploadManager.DownloadFile
				(path: strPath, fileName: strFileName, maxDownloadSpeedInKB: intMaxDownloadSpeedInKB);

			if (blnResult)
			{
				return (null);
			}
			else
			{
				string strErrorMessage =
					Resources.Messages.UnexpectedError;

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}
			// **************************************************
		}
	}
}
