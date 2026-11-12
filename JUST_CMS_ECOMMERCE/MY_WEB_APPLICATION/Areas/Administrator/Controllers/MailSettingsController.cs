using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/10/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: true,
		resourceName: Resources.Strings.ControllersKeys.Administrator.MailSettings)]
	public partial class MailSettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public MailSettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varAllOfMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varAllOfMailSettings));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetById(id.Value);

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.MailSettings oMailSettings =
				new Models.MailSettings();
			// **************************************************

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.MailSettings mailsettings)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				mailsettings.CultureLcid = CultureLcid;

				mailsettings.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				mailsettings.SetIsActive
					(mailsettings.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.MailSettingsRepository.Insert(mailsettings);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.MailSettings.Index()));
			}

			return (View(mailsettings));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetById(id.Value);

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.MailSettings mailsettings)
		{
			// **************************************************
			Models.MailSettings oOriginalMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetById(mailsettings.Id);

			if (oOriginalMailSettings == null)
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
				oOriginalMailSettings.Name = mailsettings.Name;
				oOriginalMailSettings.Ordering = mailsettings.Ordering;
				oOriginalMailSettings.Description = mailsettings.Description;
				oOriginalMailSettings.BccAddresses = mailsettings.BccAddresses;
				oOriginalMailSettings.SenderDisplayName = mailsettings.SenderDisplayName;
				oOriginalMailSettings.SmtpClientTimeout = mailsettings.SmtpClientTimeout;
				oOriginalMailSettings.SenderEmailAddress = mailsettings.SenderEmailAddress;
				oOriginalMailSettings.SupportDisplayName = mailsettings.SupportDisplayName;
				oOriginalMailSettings.SenderEmailPassword = mailsettings.SenderEmailPassword;
				oOriginalMailSettings.SmtpClientEnableSsl = mailsettings.SmtpClientEnableSsl;
				oOriginalMailSettings.SupportEmailAddress = mailsettings.SupportEmailAddress;
				oOriginalMailSettings.EmailSubjectTemplate = mailsettings.EmailSubjectTemplate;
				oOriginalMailSettings.SmtpClientPortNumber = mailsettings.SmtpClientPortNumber;
				oOriginalMailSettings.SmtpClientHostAddress = mailsettings.SmtpClientHostAddress;
				// **************************************************

				// **************************************************
				oOriginalMailSettings.CultureLcid = CultureLcid;

				oOriginalMailSettings.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalMailSettings.SetIsActive
					(mailsettings.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.MailSettingsRepository.Update(oOriginalMailSettings);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.MailSettings.Index()));
			}

			return (View(mailsettings));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetById(id.Value);

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
		{
			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository
				.GetById(id);

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.MailSettingsRepository.Delete(oMailSettings);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.MailSettings.Index()));
		}
	}
}
