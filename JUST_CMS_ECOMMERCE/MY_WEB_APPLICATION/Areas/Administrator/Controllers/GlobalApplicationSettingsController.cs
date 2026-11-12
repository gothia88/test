using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/10/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.GlobalApplicationSettings)]
	public partial class GlobalApplicationSettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public GlobalApplicationSettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit()
		{
			Models.GlobalApplicationSettings oGlobalApplicationSettings =
				UnitOfWork.GlobalApplicationSettingsRepository.Get()
				.FirstOrDefault();

			if (oGlobalApplicationSettings == null)
			{
				oGlobalApplicationSettings =
					new Models.GlobalApplicationSettings();

				UnitOfWork.GlobalApplicationSettingsRepository.Insert(oGlobalApplicationSettings);

				UnitOfWork.Save();
			}

			return (View(oGlobalApplicationSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.GlobalApplicationSettings model)
		{
			// **************************************************
			Models.GlobalApplicationSettings oOriginalGlobalApplicationSettings =
				UnitOfWork.GlobalApplicationSettingsRepository
				.GetById(model.Id);

			if (oOriginalGlobalApplicationSettings == null)
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
				oOriginalGlobalApplicationSettings.SiteUrl = model.SiteUrl;
				oOriginalGlobalApplicationSettings.AlexaCode = model.AlexaCode;
				oOriginalGlobalApplicationSettings.GoogleCode = model.GoogleCode;
				// **************************************************

				// **************************************************
				oOriginalGlobalApplicationSettings.RssImageUrl = model.RssImageUrl;
				oOriginalGlobalApplicationSettings.RssItemCount = model.RssItemCount;
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalGlobalApplicationSettings.IsSslEnabled = model.IsSslEnabled;
					oOriginalGlobalApplicationSettings.MasterPassword = model.MasterPassword;

					oOriginalGlobalApplicationSettings.TotalUserCount = model.TotalUserCount;

					oOriginalGlobalApplicationSettings.SmsCenterNumber = model.SmsCenterNumber;
					oOriginalGlobalApplicationSettings.SmsCenterUsername = model.SmsCenterUsername;
					oOriginalGlobalApplicationSettings.SmsCenterPassword = model.SmsCenterPassword;

					oOriginalGlobalApplicationSettings.CurrentUserProfileLevel = model.CurrentUserProfileLevel;

					oOriginalGlobalApplicationSettings.SleepTimeoutInDemoVersion = model.SleepTimeoutInDemoVersion;
					oOriginalGlobalApplicationSettings.IsApplicationInDemoVersion = model.IsApplicationInDemoVersion;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GlobalApplicationSettingsRepository.Update(oOriginalGlobalApplicationSettings);

				UnitOfWork.Save();

				// با توجه به آخرين تغييرات، ديگر نيازی به دستور ذيل نمی‌باشد
				//Infrastructure.GlobalApplicationSettings.GetInstanceByCultureLcid();

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));
			}

			return (View(model));
		}
	}
}
