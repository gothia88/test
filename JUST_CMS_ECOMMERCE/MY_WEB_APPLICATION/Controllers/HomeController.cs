using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1394/03/09
	/// Changed In Version: 1.9.7
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Home)]
	public partial class HomeController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public HomeController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Home)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			// **************************************************
			ViewBag.Title = Infrastructure.ApplicationSettings.Instance.HomePageTitle;
			ViewBag.Keywords = Infrastructure.ApplicationSettings.Instance.HomePageKeywords;
			ViewBag.Copyright = Infrastructure.ApplicationSettings.Instance.HomePageCopyright;
			ViewBag.Description = Infrastructure.ApplicationSettings.Instance.HomePageDescription;
			ViewBag.Classification = Infrastructure.ApplicationSettings.Instance.HomePageClassification;

			ViewBag.Robots = "index,follow";
			ViewBag.Author = "Mr. Dariush Tasdighi - آقای داريوش تصديقی";
			// **************************************************

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.ChangeCulture)]
		public virtual System.Web.Mvc.ActionResult ChangeCulture(string culture)
		{
			Infrastructure.Sessions.Culture = culture;

			string strHttpReferer =
				Request.ServerVariables["HTTP_REFERER"];

			Response.Redirect(strHttpReferer, false);
			return (null);

			//return (View());
		}

		/// <summary>
		/// Version: 1.0.3
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.About)]
		public virtual System.Web.Mvc.ViewResult About()
		{
			return (View());
		}

		/// <summary>
		/// Version: 1.0.3
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Help)]
		public virtual System.Web.Mvc.ViewResult Help()
		{
			return (View());
		}

		/// <summary>
		/// Version: 1.0.4
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.7
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Search)]
		public virtual System.Web.Mvc.ViewResult Search(string searchKeywords)
		{
			ViewBag.SearchKeywords = searchKeywords;

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Contact)]
		public virtual System.Web.Mvc.ActionResult Contact()
		{
			// **************************************************
			var varRecipients =
				UnitOfWork.UserRepository
				.GetActiveRecipientsByCultureLcid(CultureLcid)
				.ToList()
				;

			ViewBag.RecipientUserId =
				new System.Web.Mvc.SelectList(varRecipients, "Id", "FullNameWithJobTitle");
			// **************************************************

			// **************************************************
			ViewModels.Home.ContactViewModel oContactViewModel =
				new ViewModels.Home.ContactViewModel();
			// **************************************************

			return (View(oContactViewModel));
		}

		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/22
		/// Changed In Version: 1.9.4
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Contact)]
		public virtual System.Web.Mvc.ActionResult Contact
			([System.Web.Mvc.Bind(Include = "RecipientUserId,FullName,EmailAddress,Subject,Description,CarbonCopyToSender")]
			ViewModels.Home.ContactViewModel viewModel)
		{
			if (Infrastructure.ApplicationSettings.Instance.IsCaptchaImageEnabled)
			{
				if (CaptchaMvc.HtmlHelpers.CaptchaHelper.IsCaptchaValid
					(this, Resources.CaptchaImage.ErrorMessage01) == false)
				{
					TempData["ErrorMessage"] =
						Resources.CaptchaImage.ErrorMessage01;

					ModelState.AddModelError
						(string.Empty, Resources.CaptchaImage.ErrorMessage01);
				}
			}

			try
			{
				Models.User oRecipientUser =
					UnitOfWork.UserRepository
					.GetById(viewModel.RecipientUserId);

				if ((oRecipientUser == null) ||
					(oRecipientUser.IsDirectContactable == false))
				{
					string strErrorMessage =
						string.Format(Resources.Messages.Required,
						Resources.ViewModels.Home.ContactViewModel.RecipientUserId);

					ModelState.AddModelError("RecipientUserId", strErrorMessage);
				}

				if (Infrastructure.AuthenticatedUser.IsAuthenticated)
				{
					ModelState.Remove("FullName");
					ModelState.Remove("EmailAddress");
				}

				if (ModelState.IsValid)
				{
					// **************************************************
					string strSenderFullName = viewModel.FullName;
					string strSenderEmailAddress = viewModel.EmailAddress;

					if (Infrastructure.AuthenticatedUser.IsAuthenticated)
					{
						strSenderFullName =
							Infrastructure.Sessions.AuthenticatedUser.FullName;

						strSenderEmailAddress =
							Infrastructure.Sessions.AuthenticatedUser.EmailAddress;
					}
					// **************************************************

					// **************************************************
					string strAttachmentPathName = string.Empty;
					System.Web.HttpPostedFileBase oPostedFile = null;

					if ((Request.Files.Count != 0) &&
						(Infrastructure.ApplicationSettings.Instance.CanUserAttachFileInContactUsPage))
					{
						oPostedFile = Request.Files[0];

						if (oPostedFile.ContentLength != 0)
						{
							string strParentPath =
								Server.MapPath("~/App_Data");

							Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

							string strPath =
								string.Format("{0}\\ContactAttachments", strParentPath);

							if (System.IO.Directory.Exists(strPath) == false)
							{
								System.IO.Directory.CreateDirectory(strPath);
							}

							string strExtension =
								System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

							if (strExtension.Length > Models.Constants.FILE_EXTENSION_LENGTH)
							{
								strExtension =
									strExtension.Substring(0, Models.Constants.FILE_EXTENSION_LENGTH);
							}

							strAttachmentPathName =
								string.Format("{0}\\{1}{2}",
								strPath, System.Guid.NewGuid(), strExtension);

							oPostedFile.SaveAs(strAttachmentPathName);
						}
					}
					// **************************************************

					// **************************************************
					string strDescription =
						Dtx.Net.Mail.MailMessage
							.ConvertTextForEmailBody(viewModel.Description);

					string strEmailBodyTemplatePathName =
						Server.MapPath(string.Format
						("~/App_Data/LocalizedTemplates/Email/Contact.{0}.htm", CultureName));

					string strBody = string.Empty;
					if (System.IO.File.Exists(strEmailBodyTemplatePathName))
					{
						string strBodyTemplate =
							Dtx.IO.File.Read(strEmailBodyTemplatePathName);

						strBody = strBodyTemplate
							.Replace("[SUBJECT]", viewModel.Subject)
							.Replace("[DESCRIPTION]", strDescription)
							.Replace("[FULL_NAME]", strSenderFullName)
							.Replace("[EMAIL_ADDRESS]", strSenderEmailAddress)
							.Replace("[SITE_URL]", Infrastructure.GlobalApplicationSettings.Instance.SiteUrl)
							;
					}
					else
					{
						strBody = strDescription;
					}
					// **************************************************

					System.Net.Mail.MailAddress oMailAddress = null;

					oMailAddress =
						new System.Net.Mail.MailAddress
							(displayName: oRecipientUser.FullName,
							address: oRecipientUser.EmailAddress,
							displayNameEncoding: System.Text.Encoding.UTF8);

					Models.MailSettings oMailSettings =
						Infrastructure.ApplicationSettings.Instance.MailSettings;

					System.Net.Mail.MailAddressCollection oMailAddressCollection =
						new System.Net.Mail.MailAddressCollection();

					oMailAddressCollection.Clear();
					oMailAddressCollection.Add(oMailAddress);

					if (string.IsNullOrEmpty(strAttachmentPathName))
					{
						Dtx.Net.Mail.MailMessage.Send
							(sender: null,
							body: strBody,
							attachmentPathNames: null,
							subject: viewModel.Subject,
							mailSettings: oMailSettings,
							recipients: oMailAddressCollection,
							priority: System.Net.Mail.MailPriority.High,
							deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.None);
					}
					else
					{
						System.Collections.Generic.List<string> oAttachmentPathNames =
							new System.Collections.Generic.List<string>();

						oAttachmentPathNames.Add(strAttachmentPathName);

						Dtx.Net.Mail.MailMessage.Send
							(sender: null,
							body: strBody,
							subject: viewModel.Subject,
							mailSettings: oMailSettings,
							recipients: oMailAddressCollection,
							attachmentPathNames: oAttachmentPathNames,
							priority: System.Net.Mail.MailPriority.High,
							deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.None);
					}

					if (viewModel.CarbonCopyToSender)
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(displayName: strSenderFullName,
								address: strSenderEmailAddress,
								displayNameEncoding: System.Text.Encoding.UTF8);

						oMailAddressCollection.Clear();
						oMailAddressCollection.Add(oMailAddress);

						if (string.IsNullOrEmpty(strAttachmentPathName))
						{
							Dtx.Net.Mail.MailMessage.Send
								(sender: null,
								body: strBody,
								attachmentPathNames: null,
								subject: viewModel.Subject,
								mailSettings: oMailSettings,
								recipients: oMailAddressCollection,
								priority: System.Net.Mail.MailPriority.High,
								deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.None);
						}
						else
						{
							System.Collections.Generic.List<string> oAttachmentPathNames =
								new System.Collections.Generic.List<string>();

							oAttachmentPathNames.Add(strAttachmentPathName);

							Dtx.Net.Mail.MailMessage.Send
								(sender: null,
								body: strBody,
								subject: viewModel.Subject,
								mailSettings: oMailSettings,
								recipients: oMailAddressCollection,
								attachmentPathNames: oAttachmentPathNames,
								priority: System.Net.Mail.MailPriority.High,
								deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.None);
						}
					}

					Infrastructure.NotificationService.NotifyRecipientAfterContacting
						(oRecipientUser, strSenderFullName);

					// **************************************************
					// نامه الکترونيکی شما با موفقيت ارسال گرديد
					string strInformationMessage =
						Resources.ViewModels.Home.ContactViewModel.Information001;

					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
					// **************************************************
				}
			}
			catch (Dtx.ApplicationException ex)
			{
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, ex.Message));
			}
			catch (System.Exception ex)
			{
				Dtx.LogHandler.Report(GetType(), null, ex);

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, Resources.Messages.UnexpectedError));
			}
			finally
			{
				// **************************************************
				var varRecipients =
					UnitOfWork.UserRepository
					.GetActiveRecipientsByCultureLcid(CultureLcid)
					.ToList()
					;

				ViewBag.RecipientUserId =
					new System.Web.Mvc.SelectList(varRecipients, "Id", "FullName");
				// **************************************************
			}

			return (View(viewModel));
		}
	}
}
