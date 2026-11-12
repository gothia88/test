using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.1.5
	/// Update Date: 1394/01/22
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.ApplicationSettings)]
	public partial class ApplicationSettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ApplicationSettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit()
		{
			Models.ApplicationSettings oApplicationSettings =
				UnitOfWork.ApplicationSettingsRepository.Get()
				.Where(current => current.CultureLcid == CultureLcid)
				.FirstOrDefault();

			// **************************************************
			var varAllOfMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;
			// **************************************************

			if (oApplicationSettings == null)
			{
				oApplicationSettings =
					new Models.ApplicationSettings();

				UnitOfWork.ApplicationSettingsRepository.Insert(oApplicationSettings);

				UnitOfWork.Save();
			}

			ViewBag.MailSettingsId =
				new System.Web.Mvc.SelectList
					(varAllOfMailSettings, "Id", "Name", oApplicationSettings.MailSettingsId);

			return (View(oApplicationSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.ApplicationSettings applicationSettings)
		{
			// **************************************************
			Models.ApplicationSettings oOriginalApplicationSettings =
				UnitOfWork.ApplicationSettingsRepository.Get()
				.Where(current => current.Id == applicationSettings.Id)
				.FirstOrDefault();

			if (oOriginalApplicationSettings == null)
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
				oOriginalApplicationSettings.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalApplicationSettings.MailSettingsId =
					applicationSettings.MailSettingsId;

				oOriginalApplicationSettings.IsSmsServiceEnabled =
					applicationSettings.IsSmsServiceEnabled;

				oOriginalApplicationSettings.IsCaptchaImageEnabled =
					applicationSettings.IsCaptchaImageEnabled;

				oOriginalApplicationSettings.ServerTimeZoneDifference =
					applicationSettings.ServerTimeZoneDifference;

				oOriginalApplicationSettings.ForceUserToUpdateProfileAfterLogin =
					applicationSettings.ForceUserToUpdateProfileAfterLogin;

				oOriginalApplicationSettings.ApplicationName = applicationSettings.ApplicationName;

				oOriginalApplicationSettings.CanUserAttachFileInContactUsPage =
					applicationSettings.CanUserAttachFileInContactUsPage;

				oOriginalApplicationSettings.ShouldUserBeActivatedAfterRegistration =
					applicationSettings.ShouldUserBeActivatedAfterRegistration;

				oOriginalApplicationSettings.IsUserEmailVerificationRequiredForLogin =
					applicationSettings.IsUserEmailVerificationRequiredForLogin;
				// **************************************************

				// **************************************************
				oOriginalApplicationSettings.HomePageTitle = applicationSettings.HomePageTitle;
				oOriginalApplicationSettings.HomePageKeywords = applicationSettings.HomePageKeywords;
				oOriginalApplicationSettings.HomePageCopyright = applicationSettings.HomePageCopyright;
				oOriginalApplicationSettings.HomePageDescription = applicationSettings.HomePageDescription;
				oOriginalApplicationSettings.HomePageClassification = applicationSettings.HomePageClassification;
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalApplicationSettings.IsCmsEnabled =
						applicationSettings.IsCmsEnabled;

					oOriginalApplicationSettings.CheckNationalCode =
						applicationSettings.CheckNationalCode;

					oOriginalApplicationSettings.ApplicationVersion =
						applicationSettings.ApplicationVersion;

					oOriginalApplicationSettings.ApplicationCopyrightText =
						applicationSettings.ApplicationCopyrightText;

					oOriginalApplicationSettings.IsECommerceSystemEnabled =
						applicationSettings.IsECommerceSystemEnabled;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ApplicationSettingsRepository.Update(oOriginalApplicationSettings);

				UnitOfWork.Save();

				// با توجه به آخرين تغييرات، ديگر نيازی به دستور ذيل نمی‌باشد
				//Infrastructure.ApplicationSettings.GetInstanceByCultureLcid();

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));
			}

			// **************************************************
			var varAllOfMailSettings =
				UnitOfWork.MailSettingsRepository.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			ViewBag.MailSettingsId =
				new System.Web.Mvc.SelectList
					(varAllOfMailSettings, "Id", "Name", applicationSettings.MailSettingsId);
			// **************************************************

			return (View(applicationSettings));
		}
	}
}
