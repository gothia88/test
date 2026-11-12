using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/10/03
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.Cms.Settings),
		resourceName: Resources.Models.Cms.Strings.SettingsKeys.EntityName)]
	public partial class SettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public SettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit()
		{
			Models.Cms.CmsSettings oSettings =
				UnitOfWork.CmsUnitOfWork.SettingsRepository.Get()
				.Where(current => current.CultureLcid == CultureLcid)
				.FirstOrDefault();

			if (oSettings == null)
			{
				oSettings =
					new Models.Cms.CmsSettings();

				UnitOfWork.CmsUnitOfWork.SettingsRepository.Insert(oSettings);

				UnitOfWork.Save();
			}

			return (View(oSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.CmsSettings settings)
		{
			// **************************************************
			Models.Cms.CmsSettings oOriginalSettings =
				UnitOfWork.CmsUnitOfWork.SettingsRepository
				.GetById(settings.Id);

			if (oOriginalSettings == null)
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
				oOriginalSettings.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalSettings.PostsPageSizeOnHomePage = settings.PostsPageSizeOnHomePage;
				oOriginalSettings.PostsPageSizeOnPostsPage = settings.PostsPageSizeOnPostsPage;
				oOriginalSettings.PostsPageSizeOnPostCategoryPage = settings.PostsPageSizeOnPostCategoryPage;
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.SettingsRepository
					.Update(oOriginalSettings);

				UnitOfWork.Save();

				// با توجه به آخرين تغييرات، ديگر نيازی به دستور ذيل نمی‌باشد
				//Infrastructure.ApplicationSettings.GetInstanceByCultureLcid();

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));
			}

			return (View(settings));
		}
	}
}
