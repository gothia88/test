using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.ECommerce.Controllers
{
	/// <summary>
	/// Version: 1.1.7
	/// Update Date: 1394/03/02
	/// Changed In Version: 1.9.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.ECommerce.Transaction),
		resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.EntitiesName)]
	public partial class TransactionController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public TransactionController()
		{
		}

		#region General
		private Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>
			CheckAccessToEntity(System.Guid? id)
		{
			Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction> oResult =
				new Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>();

			if (id.HasValue == false)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest));

				return (oResult);
			}

			Models.ECommerce.Transaction oTransaction =
				UnitOfWork.ECommerceUnitOfWork.TransactionRepository
				.GetById(id.Value);

			if (oTransaction == null)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.NotFound));

				return (oResult);
			}

			oResult.Entity = oTransaction;

			return (oResult);
		}
		#endregion /General

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.DisplayDataForAdmin)]
		public virtual System.Web.Mvc.ActionResult DisplayDataForAdmin()
		{
			return (View());
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.GetDataForAdmin)]
		public virtual System.Web.Mvc.JsonResult GetDataForAdmin
			(ViewModels.Areas.ECommerce.Transaction.AdminSearchViewModel viewModel)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varEntities =
				UnitOfWork.ECommerceUnitOfWork.TransactionRepository
				.GetDataForAdmin()
				;

			if (viewModel.PlanId.HasValue)
			{
				varEntities =
					varEntities
					.Where(current => current.PlanId == viewModel.PlanId.Value)
					;
			}

			if (viewModel.UserId.HasValue)
			{
				varEntities =
					varEntities
					.Where(current => current.UserId == viewModel.UserId.Value)
					;
			}

			if (string.IsNullOrWhiteSpace(viewModel.Description) == false)
			{
				viewModel.Description =
					viewModel.Description.Trim();

				varEntities =
					varEntities
					.Where(current => current.Description.Contains(viewModel.Description))
					;
			}

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.ECommerce.Transaction.GetDataForAdminViewModel>(varEntities);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.DisplayDataForUser)]
		public virtual System.Web.Mvc.ActionResult DisplayDataForUser()
		{
			return (View());
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.GetDataForUser)]
		public virtual System.Web.Mvc.JsonResult GetDataForUser
			(ViewModels.Areas.ECommerce.Transaction.UserSearchViewModel viewModel)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varEntities =
				UnitOfWork.ECommerceUnitOfWork.TransactionRepository
				.GetDataForUserByUserId(Infrastructure.Sessions.AuthenticatedUser.Id)
				;

			if (string.IsNullOrWhiteSpace(viewModel.Description) == false)
			{
				viewModel.Description =
					viewModel.Description.Trim();

				varEntities =
					varEntities
					.Where(current => current.Description.Contains(viewModel.Description))
					;
			}

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.ECommerce.Transaction.GetDataForUserViewModel>(varEntities);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Transaction oTransaction = oResult.Entity;
			// **************************************************

			return (View(model: oTransaction));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.DisplayDataToUser)]
		public virtual System.Web.Mvc.ActionResult Display(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Transaction oTransaction = oResult.Entity;
			// **************************************************

			if (oTransaction.UserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			return (View(model: oTransaction));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.ECommerce.Transaction oTransaction =
				new Models.ECommerce.Transaction();
			// **************************************************

			return (View(oTransaction));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.ECommerce.Transaction transaction)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				if (transaction.PlanId.HasValue)
				{
					if (transaction.UserId.HasValue == false)
					{
						transaction.PlanId = null;
					}
					else
					{
						Models.User oUser =
							UnitOfWork.UserRepository
							.GetById(transaction.UserId.Value);


						Models.ECommerce.Plan oPlan =
							UnitOfWork.ECommerceUnitOfWork.PlanRepository
							.GetById(transaction.PlanId.Value);

						BLL.Plan.SetPlanForUser(oUser, oPlan);

						UnitOfWork.UserRepository.Update(oUser);
					}
				}
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				transaction.CreatorOrModifierUserId =
					Infrastructure.Sessions.AuthenticatedUser.Id;

				transaction.InsertDateTime.Value = Models.Utility.Now;
				transaction.UpdateDateTime.Value = Models.Utility.Now;
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Insert(transaction);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ECommerce.Transaction.DisplayDataForAdmin()));
			}

			return (View(model: transaction));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Transaction oTransaction = oResult.Entity;
			// **************************************************

			return (View(model: oTransaction));
		}

		/// <summary>
		/// Version: 1.1.5
		/// Update Date: 1394/02/19
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.ECommerce.Transaction transaction)
		{
			// **************************************************
			Models.ECommerce.Transaction oOriginalTransaction =
				UnitOfWork.ECommerceUnitOfWork.TransactionRepository
				.GetById(transaction.Id);

			if (oOriginalTransaction == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				if (transaction.PlanId.HasValue)
				{
					if (transaction.UserId.HasValue == false)
					{
						transaction.PlanId = null;
					}
					else
					{
						Models.User oUser =
							UnitOfWork.UserRepository
							.GetById(oOriginalTransaction.UserId.Value);

						if (oUser.PlanId != transaction.PlanId)
						{
							Models.ECommerce.Plan oPlan =
								UnitOfWork.ECommerceUnitOfWork.PlanRepository
								.GetById(oOriginalTransaction.PlanId.Value);

							BLL.Plan.SetPlanForUser(oUser, oPlan);

							UnitOfWork.UserRepository.Update(oUser);
						}
					}
				}
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalTransaction.PlanId = transaction.PlanId;
				oOriginalTransaction.UserId = transaction.UserId;
				oOriginalTransaction.ParentTransactionId = transaction.ParentTransactionId;

				oOriginalTransaction.Debit = transaction.Debit;
				oOriginalTransaction.Credit = transaction.Credit;

				oOriginalTransaction.SiteDebit = transaction.SiteDebit;
				oOriginalTransaction.SiteCredit = transaction.SiteCredit;

				oOriginalTransaction.UpdateDateTime.Value = Models.Utility.Now;

				oOriginalTransaction.IsPending = transaction.IsPending;
				oOriginalTransaction.IsPaidOnline = transaction.IsPaidOnline;

				oOriginalTransaction.CreatorOrModifierUserId =
					Infrastructure.Sessions.AuthenticatedUser.Id;

				oOriginalTransaction.MerchantIdentityGet =
					transaction.MerchantIdentityGet;

				oOriginalTransaction.MerchantTransactionIdentity =
					transaction.MerchantTransactionIdentity;

				oOriginalTransaction.Description =
					transaction.Description;

				oOriginalTransaction.AdministratorDescription =
					transaction.AdministratorDescription;
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Update(oOriginalTransaction);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ECommerce.Transaction.DisplayDataForAdmin()));
			}

			return (View(model: transaction));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Transaction oTransaction = oResult.Entity;
			// **************************************************

			return (View(model: oTransaction));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Transaction>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Transaction oTransaction = oResult.Entity;
			// **************************************************

			UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Delete(oTransaction);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.ECommerce.Transaction.DisplayDataForAdmin()));
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.IncreaseCreditByOnlinePayment)]
		public virtual System.Web.Mvc.ActionResult IncreaseCreditByOnlinePayment()
		{
			// **************************************************
			// *** Check Business *******************************
			// **************************************************
			if (Infrastructure.ApplicationSettings.Instance.IsECommerceSystemEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.ECommerce.Settings.Instance.IsOnlinePaymentEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}
			// **************************************************
			// *** /Check Business ******************************
			// **************************************************

			// **************************************************
			ViewModels.Areas.ECommerce.Transaction.IncreaseCreditByOnlinePaymentViewModel oViewModel =
				new ViewModels.Areas.ECommerce.Transaction.IncreaseCreditByOnlinePaymentViewModel();

			oViewModel.Amount = 500000;

			return (View(model: oViewModel));
			// **************************************************
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.IncreaseCreditByOnlinePayment)]
		public virtual System.Web.Mvc.ActionResult IncreaseCreditByOnlinePayment
			(ViewModels.Areas.ECommerce.Transaction.IncreaseCreditByOnlinePaymentViewModel viewModel)
		{
			string strErrorMessage = string.Empty;

			// **************************************************
			// *** Check Business *******************************
			// **************************************************
			if (Infrastructure.ApplicationSettings.Instance.IsECommerceSystemEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.ECommerce.Settings.Instance.IsOnlinePaymentEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			long lngMinAmount = 10000;
			long lngMaxAmount = 10000000;

			if (viewModel.Amount < lngMinAmount)
			{
				strErrorMessage =
					string.Format("مبلغ مورد نظر، برای افزايش اعتبار، نمی‌تواند از {0} ريال کمتر باشد!",
					Dtx.Text.Convert.DisplayFormattedNumber(lngMinAmount));

				ModelState.AddModelError("Amount", strErrorMessage);

				return (View(model: viewModel));
			}

			if (viewModel.Amount > lngMaxAmount)
			{
				strErrorMessage =
					string.Format("مبلغ مورد نظر، برای افزايش اعتبار، نمی‌تواند از {0} ريال بيشتر باشد!",
					Dtx.Text.Convert.DisplayFormattedNumber(lngMaxAmount));

				ModelState.AddModelError("Amount", strErrorMessage);

				return (View(model: viewModel));
			}
			// **************************************************
			// *** /Check Business ******************************
			// **************************************************

			if (ModelState.IsValid)
			{
				int intIdentityGet = 0;

				try
				{
					Models.ECommerce.Merchant oMerchant =
						Infrastructure.ECommerce.Settings.Instance.Merchant;

					intIdentityGet =
						System.Convert.ToInt32
						(Infrastructure.MerchantUtility.SendRequestForPayment(oMerchant, viewModel.Amount));

					if (intIdentityGet > 0)
					{
						// **************************************************
						// **************************************************
						// **************************************************
						Models.ECommerce.Transaction oTransaction =
							new Models.ECommerce.Transaction();

						// **************************************************
						oTransaction.IsPending = true;
						oTransaction.IsPaidOnline = true;

						oTransaction.Debit = 0;
						oTransaction.Credit = viewModel.Amount;

						oTransaction.SiteDebit = 0;
						oTransaction.SiteCredit = 0;

						oTransaction.PlanId = null;
						oTransaction.ParentTransactionId = null;

						oTransaction.CreatorOrModifierUserId = null;

						oTransaction.UserId =
							Infrastructure.Sessions.AuthenticatedUser.Id;

						oTransaction.AdministratorDescription = null;
						oTransaction.Description = "افزايش اعتبار، به صورت آنلاين";

						oTransaction.InsertDateTime.Value = Models.Utility.Now;
						oTransaction.UpdateDateTime.Value = Models.Utility.Now;

						oTransaction.MerchantTransactionIdentity = null;
						oTransaction.MerchantIdentityGet = intIdentityGet.ToString();
						// **************************************************

						UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Insert(oTransaction);

						UnitOfWork.Save();
						// **************************************************
						// **************************************************
						// **************************************************

						string strRedirectUrl =
							string.Format("{0}{1}",
							oMerchant.SecondSendUrl, intIdentityGet);

						return (Redirect(url: strRedirectUrl));
					}

					switch (intIdentityGet)
					{
						case -1:
						{
							strErrorMessage =
								"API ارسالی با نوع API تعريف شده در درگاه خريد، سازگار نمی‌باشد!";

							break;
						}

						case -2:
						{
							strErrorMessage =
								"مبلغ کل سبد محصول يا عددی نبوده و يا کمتر از هزار ريال می‌باشد!";

							break;
						}

						case -3:
						{
							strErrorMessage =
								"نشانی بازگشت از درگاه اينترنتی مشخص نشده است!";

							break;
						}

						case -4:
						{
							strErrorMessage =
								"درگاهی با اطلاعات ارسالی شما يافت نشده و يا در حالت انتظار می‌باشد!";

							break;
						}

						default:
						{
							strErrorMessage =
								"خطای ناشناخته‌ای صورت گرفته است، لطفا با مسوولين فنی پايگاه تماس حاصل فرماييد.";

							break;
						}
					}
				}
				catch
				{
					strErrorMessage =
						"خطای ناشناخته‌ای صورت گرفته است، لطفا با مسوولين فنی پايگاه تماس حاصل فرماييد.";
				}
			}

			if (string.IsNullOrWhiteSpace(strErrorMessage) == false)
			{
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));
			}

			return (View(model: viewModel));
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		// Note: باشد Public بايد
		// Note: باشد HttpPost بايد
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.CheckOnlinePayment)]
		public virtual System.Web.Mvc.ActionResult CheckOnlinePayment()
		{
			// **************************************************
			Models.ECommerce.Merchant oMerchant =
				Infrastructure.ECommerce.Settings.Instance.Merchant;

			string strIdentityGet =
				Request.Params[oMerchant.IdentityGetName];

			string strTransactionIdentity =
				Request.Params[oMerchant.TransactionIdentityName];
			// **************************************************

			Infrastructure.JsonData oJsonData =
				CheckOnlinePayment(strIdentityGet, strTransactionIdentity, justForText: false);

			switch (oJsonData.State)
			{
				case Infrastructure.Enums.JsonResultStates.Error:
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, oJsonData.MessageText));

					break;
				}

				case Infrastructure.Enums.JsonResultStates.Success:
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Information, oJsonData.MessageText));

					break;
				}

				case Infrastructure.Enums.JsonResultStates.Warning:
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, oJsonData.MessageText));

					break;
				}
			}

			return (View(viewName: MVC.Shared.Views.DisplayMessage));
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		// Note: باشد Public بايد
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.CheckOnlinePaymentForTest)]
		public virtual System.Web.Mvc.ActionResult CheckOnlinePaymentForTest()
		{
			// **************************************************
			// **************************************************
			// **************************************************
			string strPassword =
				Request.Params["Pass"];

			if (string.IsNullOrWhiteSpace(strPassword))
			{
				return (null);
			}

			// Fib...
			if (string.Compare(Dtx.Security.Hashing.GetSha1(strPassword),
				"A2E69488CA217BD7EA47D2059CDEAA46F27EF987", ignoreCase: true) != 0)
			{
				return (null);
			}
			// **************************************************
			// **************************************************
			// **************************************************

			// **************************************************
			Models.ECommerce.Merchant oMerchant =
				Infrastructure.ECommerce.Settings.Instance.Merchant;

			string strIdentityGet =
				Request.Params[oMerchant.IdentityGetName];

			string strTransactionIdentity =
				Request.Params[oMerchant.TransactionIdentityName];
			// **************************************************

			Infrastructure.JsonData oJsonData =
				CheckOnlinePayment(strIdentityGet, strTransactionIdentity, justForText: true);

			switch (oJsonData.State)
			{
				case Infrastructure.Enums.JsonResultStates.Error:
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, oJsonData.MessageText));

					break;
				}

				case Infrastructure.Enums.JsonResultStates.Success:
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Information, oJsonData.MessageText));

					break;
				}

				case Infrastructure.Enums.JsonResultStates.Warning:
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, oJsonData.MessageText));

					break;
				}
			}

			return (View(viewName: MVC.Shared.Views.DisplayMessage));
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		// Note: باشد Public بايد
		/// </summary>
		private Infrastructure.JsonData CheckOnlinePayment
			(string identityGet, string transactionIdentity, bool justForText = false)
		{
			string strErrorMessage = string.Empty;
			Infrastructure.JsonData oJsonData = null;

			Models.ECommerce.Merchant oMerchant =
				Infrastructure.ECommerce.Settings.Instance.Merchant;

			if ((string.IsNullOrWhiteSpace(identityGet)) ||
				(string.IsNullOrWhiteSpace(transactionIdentity)))
			{
				strErrorMessage =
					"از طرف درگاه پرداخت الکترونيکی، پارامترها به صورت ناقص ارسال شده‌اند!" + " " +
					"لذا پرداخت آنلاين شما، با مشکل مواجه شده است." + " " +
					"در صورتی که از حساب بانکی شما مبلغی کسر گرديده است،" + " " +
					"لطفا در اسرع وقت، مراتب را به مسوولين فنی پايگاه اطلاع دهيد.";

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			// **************************************************
			string strResult = "1";

			if (justForText == false)
			{
				strResult =
					Infrastructure.MerchantUtility.SendRequestForCheckingPayment
					(oMerchant, transactionIdentity, identityGet);
			}
			// **************************************************

			if (strResult == "1")
			{
				try
				{
					// در صورتی که پرداخت آنلاين با موفقيت صورت گرفت

					Models.ECommerce.Transaction oTransaction =
						UnitOfWork.ECommerceUnitOfWork.TransactionRepository
						.Get()
						.Where(current => string.Compare(current.MerchantIdentityGet, identityGet, true) == 0)
						.FirstOrDefault();

					if (oTransaction == null)
					{
						strErrorMessage =
							"متاسفانه خطايی با کد 1000 صورت گرفته است!" + " " +
							"لطفا در اين خصوص، در اسرع وقت، با مسوولين پايگاه، تماس حاصل فرماييد ";

						oJsonData =
							new Infrastructure.JsonData()
							{
								MessageText = strErrorMessage,
								State = Infrastructure.Enums.JsonResultStates.Error,
							};

						return (oJsonData);
					}

					oTransaction.PlanId = null;
					oTransaction.ParentTransactionId = null;

					oTransaction.IsPending = false;
					oTransaction.UpdateDateTime.Value = Models.Utility.Now;
					oTransaction.MerchantTransactionIdentity = transactionIdentity;

					UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Update(oTransaction);

					UnitOfWork.Save();

					// **************************************************
					BLL.CreditAndDebitSumery oCreditAndDebitSumery =
						BLL.User.GetUserCreditAndDebitSumery(Infrastructure.Sessions.AuthenticatedUser.Id);

					string strInformationMessage =
						string.Format("تراکنش با موفقيت صورت گرفت و مبلغ {0} ريال به اعتبار شما اضافه گرديد." + " " +
						"در حال حاضر اعتبار شما برابر {1} ريال می‌باشد.",
						Dtx.Text.Convert.DisplayFormattedNumber(oTransaction.Credit),
						Dtx.Text.Convert.DisplayFormattedNumber(oCreditAndDebitSumery.Balance));
					// **************************************************

					oJsonData =
						new Infrastructure.JsonData()
						{
							MessageText = strInformationMessage,
							State = Infrastructure.Enums.JsonResultStates.Success,
						};

					return (oJsonData);
				}
				catch
				{
					strErrorMessage =
						string.Format("تراکنش مربوط به پرداخت آنلاين با کد سفارش [{0}] و کد تراکنش [{1}] با موفقيت صورت گرفت،" + " " +
						"ولی متاسفانه در ثبت نهايی خطايی صورت گرفت!" + " " +
						"لطفا در اسرع وقت، مراتب را، به همراه کد سفارش و کد تراکنش، به مسوولين فنی پايگاه اعلام نماييد.",
						identityGet, transactionIdentity);

					oJsonData =
						new Infrastructure.JsonData()
						{
							MessageText = strErrorMessage,
							State = Infrastructure.Enums.JsonResultStates.Error,
						};

					return (oJsonData);
				}
			}

			switch (strResult)
			{
				case "-1":
				{
					strErrorMessage =
						"API ارسالی با نوع API تعريف شده در درگاه خريد، سازگار نمی‌باشد!";

					break;
				}

				case "-2":
				{
					strErrorMessage =
						"مقدار trans_id ارسال شده، معتبر نمی‌باشد!";

					break;
				}

				case "-3":
				{
					strErrorMessage =
						"مقدار id_get ارسال شده، معتبر نمی‌باشد!";

					break;
				}

				case "-4":
				{
					strErrorMessage =
						"چنين تراکنشی در سيستم وجود ندارد و يا موفقيت آميز نبوده است!";

					break;
				}

				default:
				{
					strErrorMessage =
						"خطای ناشناخته‌ای صورت گرفته است، لطفا با مسوولين فنی پايگاه تماس حاصل فرماييد.";

					break;
				}
			}

			oJsonData =
				new Infrastructure.JsonData()
				{
					MessageText = strErrorMessage,
					State = Infrastructure.Enums.JsonResultStates.Error,
				};

			return (oJsonData);
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.TransferCreditToAnotherUser)]
		public virtual System.Web.Mvc.ActionResult TransferCreditToAnotherUser()
		{
			// **************************************************
			// *** Check Business *******************************
			// **************************************************
			if (Infrastructure.ApplicationSettings.Instance.IsECommerceSystemEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.ECommerce.Settings.Instance.IsCreditTransferEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}
			// **************************************************
			// *** /Check Business ******************************
			// **************************************************

			// **************************************************
			ViewModels.Areas.ECommerce.Transaction.TransferCreditToAnotherUserViewModel oViewModel =
				new ViewModels.Areas.ECommerce.Transaction.TransferCreditToAnotherUserViewModel();

			oViewModel.Amount = 10000;

			return (View(model: oViewModel));
			// **************************************************
		}

		/// <summary>
		/// Version: 1.1.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.TransferCreditToAnotherUser)]
		public virtual System.Web.Mvc.ActionResult TransferCreditToAnotherUser
			(ViewModels.Areas.ECommerce.Transaction.TransferCreditToAnotherUserViewModel viewModel)
		{
			string strErrorMessage = string.Empty;

			// **************************************************
			// *** Check Business *******************************
			// **************************************************
			if (Infrastructure.ApplicationSettings.Instance.IsECommerceSystemEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.ECommerce.Settings.Instance.IsCreditTransferEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			long lngMinAmount = 10000;
			long lngMaxAmount = 10000000;

			if (viewModel.Amount < lngMinAmount)
			{
				strErrorMessage =
					string.Format("مبلغ مورد نظر، برای انتقال، نمی‌تواند از {0} ريال کمتر باشد!",
					Dtx.Text.Convert.DisplayFormattedNumber(lngMinAmount));

				ModelState.AddModelError("Amount", strErrorMessage);
			}
			else
			{
				if (viewModel.Amount > lngMaxAmount)
				{
					strErrorMessage =
						string.Format("مبلغ مورد نظر، برای انتقال، نمی‌تواند از {0} ريال بيشتر باشد!",
						Dtx.Text.Convert.DisplayFormattedNumber(lngMaxAmount));

					ModelState.AddModelError("Amount", strErrorMessage);
				}
				else
				{
					BLL.CreditAndDebitSumery oCreditAndDebitSumery =
						BLL.User.GetUserCreditAndDebitSumery(Infrastructure.Sessions.AuthenticatedUser.Id);

					if (viewModel.Amount > oCreditAndDebitSumery.Balance)
					{
						strErrorMessage =
							string.Format("متاسفانه مبلغ مورد نظر، از ميزان اعتبار شما بيشتر است! !" + " " +
							"ميزان اعتبار شما در حال حاضر {0} ريال می‌باشد.",
							Dtx.Text.Convert.DisplayFormattedNumber(oCreditAndDebitSumery.Balance));

						ModelState.AddModelError("Amount", strErrorMessage);
					}
				}
			}

			Models.User oRecipientUser =
				UnitOfWork.UserRepository
				.GetById(viewModel.RecipientUserId);

			if (oRecipientUser == null)
			{
				strErrorMessage =
					"متاسفانه کاربری با چنين کدی يافت نشد!";

				ModelState.AddModelError("RecipientUserId", strErrorMessage);
			}
			else
			{
				if (oRecipientUser.Id == Infrastructure.Sessions.AuthenticatedUser.Id)
				{
					strErrorMessage =
						"متاسفانه امکان انتقال اعتبار، به حساب خودتان، وجود ندارد!";

					ModelState.AddModelError("RecipientUserId", strErrorMessage);
				}
				else
				{
					if (oRecipientUser.Role.IsSiteResponsible)
					{
						strErrorMessage =
							"متاسفانه امکان انتقال اعتبار، به حساب مسوولين پايگاه، وجود ندارد!";

						ModelState.AddModelError("RecipientUserId", strErrorMessage);
					}
				}
			}
			// **************************************************
			// *** /Check Business ******************************
			// **************************************************

			if (ModelState.IsValid)
			{
				decimal decSiteCommissionRateForCreditTransfer =
					Infrastructure.ECommerce.Settings.Instance.SiteCommissionRateForCreditTransfer;

				long lngSiteCredit =
					(long)((decimal)viewModel.Amount * decSiteCommissionRateForCreditTransfer / (decimal)100);

				// **************************************************
				Models.ECommerce.Transaction oSenderTransaction =
					new Models.ECommerce.Transaction();

				oSenderTransaction.IsPending = false;
				oSenderTransaction.IsPaidOnline = false;

				oSenderTransaction.Debit = viewModel.Amount;
				oSenderTransaction.Credit = 0;

				oSenderTransaction.SiteDebit = 0;
				oSenderTransaction.SiteCredit = lngSiteCredit;

				oSenderTransaction.PlanId = null;
				oSenderTransaction.ParentTransactionId = null;

				oSenderTransaction.CreatorOrModifierUserId = null;

				oSenderTransaction.UserId =
					Infrastructure.Sessions.AuthenticatedUser.Id;

				oSenderTransaction.AdministratorDescription = null;
				oSenderTransaction.Description =
					string.Format("انتقال اعتبار به {0}.", oRecipientUser.FullName);

				oSenderTransaction.InsertDateTime.Value = Models.Utility.Now;
				oSenderTransaction.UpdateDateTime.Value = Models.Utility.Now;

				oSenderTransaction.MerchantIdentityGet = null;
				oSenderTransaction.MerchantTransactionIdentity = null;

				UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Insert(oSenderTransaction);
				// **************************************************

				// **************************************************
				Models.ECommerce.Transaction oRecipientTransaction =
					new Models.ECommerce.Transaction();

				oRecipientTransaction.IsPending = false;
				oRecipientTransaction.IsPaidOnline = false;

				oRecipientTransaction.Debit = 0;
				oRecipientTransaction.Credit = viewModel.Amount - lngSiteCredit;

				oRecipientTransaction.SiteDebit = 0;
				oRecipientTransaction.SiteCredit = 0;

				oRecipientTransaction.PlanId = null;
				oRecipientTransaction.ParentTransactionId = oSenderTransaction.Id;

				oRecipientTransaction.CreatorOrModifierUserId = null;

				oRecipientTransaction.UserId = oRecipientUser.Id;

				oRecipientTransaction.AdministratorDescription = null;
				oRecipientTransaction.Description =
					string.Format("دريافت اعتبار از {0}.",
					Infrastructure.Sessions.AuthenticatedUser.FullName);

				oRecipientTransaction.InsertDateTime.Value = Models.Utility.Now;
				oRecipientTransaction.UpdateDateTime.Value = Models.Utility.Now;

				oRecipientTransaction.MerchantIdentityGet = null;
				oRecipientTransaction.MerchantTransactionIdentity = null;

				UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Insert(oRecipientTransaction);
				// **************************************************

				UnitOfWork.Save();

				// **************************************************
				string strInformationMessage =
					string.Format("انتقال اعتبار، به ميزان {0} ريال، به {1}، با موفقيت صورت گرفت.",
					Dtx.Text.Convert.DisplayFormattedNumber(viewModel.Amount), oRecipientUser.FullName);

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
				// **************************************************

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}

			return (View(model: viewModel));
		}

		/// <summary>
		/// Version: 1.1.6
		/// Update Date: 1394/02/21
		/// Changed In Version: 1.9.4
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.BuyPlanByCredit)]
		public virtual System.Web.Mvc.ActionResult BuyPlanByCredit(System.Guid? id)
		{
			// **************************************************
			// *** Check Business *******************************
			// **************************************************
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ECommerce.Plan oPlan =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetById(id.Value);

			if (oPlan == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((oPlan.IsDeleted) || (oPlan.IsActive == false))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			string strErrorMessage = string.Empty;

			if (Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
			{
				strErrorMessage =
					"فقط کاربران عادی پايگاه، قادر به خريد طرح می‌باشند!" + " " +
					"امکان خريد طرح، برای مسوولين پايگاه وجود ندارد.";

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}

			if (oPlan.Price == 0)
			{
				strErrorMessage =
					"خريد طرح رايگان، بی معنا است!" + " " +
					"طرح‌های رايگان، صرفا قابل انتخاب بوده و قابل خريد نمی‌باشند.";

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}

			BLL.CreditAndDebitSumery oCreditAndDebitSumery =
				BLL.User.GetUserCreditAndDebitSumery(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oPlan.Price > oCreditAndDebitSumery.Balance)
			{
				// **************************************************
				strErrorMessage =
					string.Format("اعتبار شما {0} ريال بوده و از قيمت طرح ({1} ريال) کمتر می‌باشد!" + " " +
					"لذا شما قادر به خريد طرح ({2})، نمی‌باشيد.",
					Dtx.Text.Convert.DisplayFormattedNumber(oCreditAndDebitSumery.Balance),
					Dtx.Text.Convert.DisplayFormattedNumber(oPlan.Price),
					oPlan.Name);

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
				// **************************************************
			}
			// **************************************************
			// *** /Check Business ******************************
			// **************************************************

			// **************************************************
			Models.ECommerce.Transaction oTransaction =
				new Models.ECommerce.Transaction();

			oTransaction.IsPending = false;
			oTransaction.IsPaidOnline = false;

			oTransaction.Debit = oPlan.Price;
			oTransaction.Credit = 0;

			oTransaction.SiteDebit = 0;
			oTransaction.SiteCredit = oPlan.Price;

			oTransaction.PlanId = oPlan.Id;
			oTransaction.ParentTransactionId = null;

			oTransaction.CreatorOrModifierUserId = null;

			oTransaction.UserId =
				Infrastructure.Sessions.AuthenticatedUser.Id;

			oTransaction.AdministratorDescription = null;

			oTransaction.Description =
				"خريد طرح، از طريق اعتبار حساب کاربری";

			oTransaction.MerchantIdentityGet = null;
			oTransaction.MerchantTransactionIdentity = null;

			oTransaction.InsertDateTime.Value = Models.Utility.Now;
			oTransaction.UpdateDateTime.Value = Models.Utility.Now;
			// **************************************************

			UnitOfWork.ECommerceUnitOfWork.TransactionRepository.Insert(oTransaction);
			// **************************************************

			// **************************************************
			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			BLL.Plan.SetPlanForUser(oUser, oPlan);

			UnitOfWork.UserRepository.Update(oUser);
			// **************************************************

			UnitOfWork.Save();

			Infrastructure.Sessions.AuthenticatedUser =
				new Infrastructure.AuthenticatedUser(oUser);

			// **************************************************
			string strInformationMessage =
				string.Format("خريد طرح ({0}) با موفقيت انجام گرديد.",
				oPlan.Name);

			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));

			return (View(viewName: MVC.Shared.Views.DisplayMessage));
			// **************************************************
		}

		/// <summary>
		/// Version: 1.1.7
		/// Update Date: 1394/03/02
		/// Changed In Version: 1.9.5
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.DeletePendingTransactions)]
		public virtual System.Web.Mvc.ActionResult DeletePendingTransactions()
		{
			// **************************************************
			ViewModels.Areas.ECommerce.Transaction.DeletePendingTransactionsViewModel oViewModel =
				new ViewModels.Areas.ECommerce.Transaction.DeletePendingTransactionsViewModel();

			oViewModel.Hours = 72;
			// **************************************************

			return (View(model: oViewModel));
		}

		/// <summary>
		/// Version: 1.1.7
		/// Update Date: 1394/03/02
		/// Changed In Version: 1.9.5
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.ECommerce.Transaction),
			resourceName: Resources.Models.ECommerce.Strings.TransactionKeys.DeletePendingTransactions)]
		public virtual System.Web.Mvc.ActionResult DeletePendingTransactions
			(ViewModels.Areas.ECommerce.Transaction.DeletePendingTransactionsViewModel viewModel)
		{
			// **************************************************
			if (viewModel.Hours < 1)
			{
				string strErrorMessage =
					"تراکنش‌های مالی که کمتر از يک ساعت صورت گرفته‌اند را نمی‌توانيد حذف نماييد!";

				ModelState.AddModelError("Hours", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				string strQuery =
					string.Format("DELETE FROM {0}.Transactions WHERE IsPaidOnline = 1 AND IsPending = 1 AND InsertDateTime_Value < '{1:yyyy/MM/dd HH:mm:ss}'",
					Models.Constants.SCHEMA_ECOMMERCE, Models.Utility.Now.AddHours(-viewModel.Hours));

				try
				{
					int intResult =
						UnitOfWork.ExecuteSqlCommand(strQuery);

					string strInformationMessage =
						string.Format("تعداد [{0}] تراکنش مالی که به صورت آنلاين صورت گرفته و در حال انتظار بوده‌اند، با موفقيت حذف گرديدند.",
						Dtx.Text.Convert.DisplayFormattedNumber(intResult));

					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
				}
				catch (System.Exception ex)
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, ex.Message));
				}
			}

			return (View(model: viewModel));
		}
	}
}
