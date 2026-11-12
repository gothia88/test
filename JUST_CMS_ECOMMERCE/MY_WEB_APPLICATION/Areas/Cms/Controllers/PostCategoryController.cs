using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/10/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.Cms.PostCategory),
		resourceName: Resources.Models.Cms.Strings.PostCategoryKeys.EntitiesName)]
	public partial class PostCategoryController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PostCategoryController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index(System.Guid? parentId = null)
		{
			if (parentId.HasValue)
			{
				Models.Cms.PostCategory oParentPostCategory =
					UnitOfWork.CmsUnitOfWork.PostCategoryRepository
					.GetById(parentId.Value);

				if (oParentPostCategory == null)
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
				}

				ViewBag.ParentPostCategory = oParentPostCategory;
			}

			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetViewModelByCultureLcid(CultureLcid, parentId)
				.ToList()
				;

			return (View(varPostCategories));
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

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id.Value);

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPostCategory));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(System.Guid? parentId = null)
		{
			if (parentId.HasValue)
			{
				Models.Cms.PostCategory oParentPostCategory =
					UnitOfWork.CmsUnitOfWork.PostCategoryRepository
					.GetById(parentId.Value);

				if (oParentPostCategory == null)
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
				}

				ViewBag.ParentPostCategory = oParentPostCategory;
			}

			// **************************************************
			Models.Cms.PostCategory oPostCategory =
				new Models.Cms.PostCategory();

			oPostCategory.ParentPostCategoryId = parentId;
			// **************************************************

			return (View(oPostCategory));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			(Models.Cms.PostCategory postCategory)
		{
			// **************************************************
			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetByCultureLcidAndParentIdAndName(CultureLcid, postCategory.ParentPostCategoryId, postCategory.Name);

			if (oPostCategory != null)
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
				postCategory.CultureLcid = CultureLcid;

				postCategory.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				postCategory.SetIsActive
					(postCategory.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
					.Insert(postCategory);

				UnitOfWork.Save();

				return (RedirectToAction
					(MVC.Cms.PostCategory.Index(postCategory.ParentPostCategoryId)));
			}

			return (View(postCategory));
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

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id.Value);

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPostCategory));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			(Models.Cms.PostCategory postCategory)
		{
			// **************************************************
			Models.Cms.PostCategory oOriginalPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(postCategory.Id);

			if (oOriginalPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetByCultureLcidAndParentIdAndNameExceptId
				(CultureLcid, postCategory.ParentPostCategoryId, postCategory.Name, postCategory.Id);

			if (oPostCategory != null)
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
				oOriginalPostCategory.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalPostCategory.Hits = postCategory.Hits;
				oOriginalPostCategory.Name = postCategory.Name;
				oOriginalPostCategory.ImageUrl = postCategory.ImageUrl;
				oOriginalPostCategory.Ordering = postCategory.Ordering;
				oOriginalPostCategory.IsFeatured = postCategory.IsFeatured;
				oOriginalPostCategory.Description = postCategory.Description;
				// **************************************************

				// **************************************************
				oOriginalPostCategory.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPostCategory.SetIsActive
					(postCategory.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
					.Update(oOriginalPostCategory);

				UnitOfWork.Save();

				return (RedirectToAction
					(MVC.Cms.PostCategory.Index(oOriginalPostCategory.ParentPostCategoryId)));
			}

			return (View(postCategory));
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

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id.Value);

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPostCategory));
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
			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetById(id);

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			int intPostCount =
				UnitOfWork.CmsUnitOfWork.PostRepository.Get()
				.Where(current => current.PostCategoryId == id)
				.Count();

			if (intPostCount != 0)
			{
				// از اين اطلاعات در جای ديگری استفاده شده است
				// لذا شما قادر به حذف آن نمی‌باشيد
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.Error101));

				return (View(oPostCategory));
			}
			// **************************************************

			// **************************************************
			int intChildCount =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
				.Where(current => current.ParentPostCategoryId == id)
				.Count();

			if (intChildCount != 0)
			{
				// از اين اطلاعات در جای ديگری استفاده شده است
				// لذا شما قادر به حذف آن نمی‌باشيد
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.Error101));

				return (View(oPostCategory));
			}
			// **************************************************

			System.Guid? sParentId =
				oPostCategory.ParentPostCategoryId;

			UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.Delete(oPostCategory);

			UnitOfWork.Save();

			return (RedirectToAction
				(MVC.Cms.PostCategory.Index(sParentId)));
		}
	}
}























//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
//{
//	/// <summary>
//	/// Version: 1.0.2
//	/// Update Date: 1393/04/30
//	/// Developer: Mr. Dariush Tasdighi
//	/// </summary>
//	[Infrastructure.Attributes.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		resourceType: typeof(Resources.Models.Cms.PostCategory),
//		resourceName: Resources.Models.Cms.Strings.PostCategoryKeys.EntitiesName)]
//	public partial class PostCategoryController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public PostCategoryController()
//		{
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Index)]
//		public virtual System.Web.Mvc.ActionResult Index()
//		{
//			var varPostCategories =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
//				.GetByCultureLcid(CultureLcid)
//				.ToList()
//				;

//			return (View(varPostCategories));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Details)]
//		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
//		{
//			if (id.HasValue == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
//			}

//			Models.Cms.PostCategory oPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
//				.Where(current => current.Id == id.Value)
//				.FirstOrDefault();

//			if (oPostCategory == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPostCategory));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create()
//		{
//			// **************************************************
//			Models.Cms.PostCategory oPostCategory =
//				new Models.Cms.PostCategory();
//			// **************************************************

//			return (View(oPostCategory));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ValidateAntiForgeryToken]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.PostCategory postCategory)
//		{
//			Models.Cms.PostCategory oPostCategory = null;

//			// **************************************************
//			oPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
//				.GetByCultureLcidAndName(CultureLcid, postCategory.Name)
//				;

//			if (oPostCategory != null)
//			{
//				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
//				string strErrorMessage =
//					string.Format(Resources.Messages.DuplicateError,
//					Resources.Models.General.Name);

//				ModelState.AddModelError("Name", strErrorMessage);
//			}
//			// **************************************************

//			// **************************************************
//			if (postCategory.MasterDataCode.HasValue)
//			{
//				oPostCategory =
//					UnitOfWork.CmsUnitOfWork.PostCategoryRepository
//					.GetByCultureLcidAndMasterDataCode
//					(CultureLcid, postCategory.MasterDataCode.Value)
//					;

//				if (oPostCategory != null)
//				{
//					// مقدار فيلد کد تکراری است! لطفا از مقدار ديگری استفاده نماييد
//					string strErrorMessage =
//						string.Format(Resources.Messages.DuplicateError,
//						Resources.Models.General.MasterDataCode);

//					ModelState.AddModelError("MasterDataCode", strErrorMessage);
//				}
//			}
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				// **************************************************
//				postCategory.CultureLcid = CultureLcid;

//				postCategory.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
//				postCategory.SetIsActive(postCategory.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
//				// **************************************************

//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Insert(postCategory);

//				UnitOfWork.Save();

//				return (RedirectToAction(MVC.Cms.PostCategory.Index()));
//			}

//			return (View(postCategory));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
//		{
//			if (id.HasValue == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
//			}

//			Models.Cms.PostCategory oPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
//				.Where(current => current.Id == id.Value)
//				.FirstOrDefault();

//			if (oPostCategory == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPostCategory));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ValidateAntiForgeryToken]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.PostCategory postCategory)
//		{
//			Models.Cms.PostCategory oPostCategory = null;

//			// **************************************************
//			oPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
//				.GetByCultureLcidAndNameExceptId
//				(CultureLcid, postCategory.Name, postCategory.Id)
//				;

//			if (oPostCategory != null)
//			{
//				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
//				string strErrorMessage =
//					string.Format(Resources.Messages.DuplicateError,
//					Resources.Models.General.Name);

//				ModelState.AddModelError("Name", strErrorMessage);
//			}
//			// **************************************************

//			// **************************************************
//			if (postCategory.MasterDataCode.HasValue)
//			{
//				oPostCategory =
//					UnitOfWork.CmsUnitOfWork.PostCategoryRepository
//					.GetByCultureLcidAndMasterDataCodeExceptId
//					(CultureLcid, postCategory.MasterDataCode.Value, postCategory.Id)
//					;

//				if (oPostCategory != null)
//				{
//					// مقدار فيلد کد تکراری است! لطفا از مقدار ديگری استفاده نماييد
//					string strErrorMessage =
//						string.Format(Resources.Messages.DuplicateError,
//						Resources.Models.General.MasterDataCode);

//					ModelState.AddModelError("MasterDataCode", strErrorMessage);
//				}
//			}
//			// **************************************************

//			// **************************************************
//			Models.Cms.PostCategory oOriginalPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
//				.GetById(postCategory.Id)
//				;

//			if (oOriginalPostCategory == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				// **************************************************
//				// **************************************************
//				// **************************************************
//				oOriginalPostCategory.Name = postCategory.Name;
//				oOriginalPostCategory.Ordering = postCategory.Ordering;
//				oOriginalPostCategory.PostCount = postCategory.PostCount;
//				oOriginalPostCategory.Description = postCategory.Description;
//				oOriginalPostCategory.MasterDataCode = postCategory.MasterDataCode;

//				oOriginalPostCategory.RssImageUrl = postCategory.RssImageUrl;
//				oOriginalPostCategory.RssImageLink = postCategory.RssImageLink;
//				oOriginalPostCategory.RssImageTitle = postCategory.RssImageTitle;

//				oOriginalPostCategory.RssItemCount = postCategory.RssItemCount;
//				oOriginalPostCategory.RssRootCreatorUrl = postCategory.RssRootCreatorUrl;

//				oOriginalPostCategory.RssChannelLink = postCategory.RssChannelLink;
//				oOriginalPostCategory.RssChannelTitle = postCategory.RssChannelTitle;
//				oOriginalPostCategory.RssChannelDescription = postCategory.RssChannelDescription;
//				// **************************************************

//				// **************************************************
//				oOriginalPostCategory.CultureLcid = CultureLcid;

//				oOriginalPostCategory.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
//				oOriginalPostCategory.SetIsActive(postCategory.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
//				// **************************************************
//				// **************************************************
//				// **************************************************

//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Update(oOriginalPostCategory);

//				UnitOfWork.Save();

//				return (RedirectToAction(MVC.Cms.PostCategory.Index()));
//			}

//			return (View(postCategory));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
//		{
//			if (id.HasValue == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
//			}

//			Models.Cms.PostCategory oPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
//				.Where(current => current.Id == id.Value)
//				.FirstOrDefault();

//			if (oPostCategory == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPostCategory));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ActionName("Delete")]
//		[System.Web.Mvc.ValidateAntiForgeryToken]
//		[Infrastructure.Attributes.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			resourceName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
//		{
//			Models.Cms.PostCategory oPostCategory =
//				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oPostCategory == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Delete(oPostCategory);

//			UnitOfWork.Save();

//			return (RedirectToAction(MVC.Cms.PostCategory.Index()));
//		}
//	}
//}
