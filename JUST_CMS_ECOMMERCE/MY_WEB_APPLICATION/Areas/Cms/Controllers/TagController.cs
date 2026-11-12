using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/12/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Cms.Tag)]
	public partial class TagController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public TagController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varTags =
				UnitOfWork.CmsUnitOfWork.TagRepository
				.GetViewModelByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varTags));
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

			Models.Common.Tag oTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oTag == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oTag));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.Common.Tag oTag =
				new Models.Common.Tag();
			// **************************************************

			return (View(oTag));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "Name,IsActive,Ordering,IsVerified,IsDeleted")]
			Models.Common.Tag tag)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				tag.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				tag.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				tag.SetIsActive(tag.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				tag.SetIsDeleted(tag.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);
				tag.SetIsVerified(tag.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.TagRepository.Insert(tag);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Tag.Index()));
			}

			return (View(tag));
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

			Models.Common.Tag oTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oTag == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oTag));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Name,IsActive,Ordering,IsVerified,IsDeleted")]
			Models.Common.Tag tag)
		{
			// **************************************************
			Models.Common.Tag oOriginalTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.GetById(tag.Id);

			if (oOriginalTag == null)
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
				oOriginalTag.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalTag.Name = tag.Name;
				oOriginalTag.Ordering = tag.Ordering;
				// **************************************************

				// **************************************************
				oOriginalTag.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalTag.SetIsActive(tag.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalTag.SetIsDeleted(tag.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalTag.SetIsVerified(tag.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.TagRepository.Update(oOriginalTag);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Tag.Index()));
			}

			return (View(tag));
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

			Models.Common.Tag oTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oTag == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oTag));
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
			Models.Common.Tag oTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oTag == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.CmsUnitOfWork.TagRepository.Delete(oTag);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.Tag.Index()));
		}
	}
}
