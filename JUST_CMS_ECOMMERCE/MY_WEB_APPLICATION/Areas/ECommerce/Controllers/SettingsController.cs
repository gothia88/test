using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.ECommerce.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.ECommerce.Settings),
		resourceName: Resources.Models.ECommerce.Strings.SettingsKeys.EntityName)]
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
			Models.ECommerce.ECommerceSettings oSettings =
				UnitOfWork.ECommerceUnitOfWork.SettingsRepository
				.GetByCultureLcid(CultureLcid);

			if (oSettings == null)
			{
				oSettings = new Models.ECommerce.ECommerceSettings();

				UnitOfWork.ECommerceUnitOfWork.SettingsRepository.Insert(oSettings);

				UnitOfWork.Save();
			}

			return (View(model: oSettings));
		}

		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.ECommerce.ECommerceSettings settings)
		{
			// **************************************************
			Models.ECommerce.ECommerceSettings oOriginalSettings =
				UnitOfWork.ECommerceUnitOfWork.SettingsRepository
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

				oOriginalSettings.IsPlansServiceEnabled = settings.IsPlansServiceEnabled;
				oOriginalSettings.IsOnlinePaymentEnabled = settings.IsOnlinePaymentEnabled;
				oOriginalSettings.IsCreditTransferEnabled = settings.IsCreditTransferEnabled;
				oOriginalSettings.SiteCommissionRateForCreditTransfer = settings.SiteCommissionRateForCreditTransfer;
				// **************************************************

				// **************************************************
				oOriginalSettings.Merchant.ApiValue = settings.Merchant.ApiValue;
				oOriginalSettings.Merchant.RedirectValue = settings.Merchant.RedirectValue;

				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalSettings.Merchant.Name = settings.Merchant.Name;
					oOriginalSettings.Merchant.GetUrl = settings.Merchant.GetUrl;
					oOriginalSettings.Merchant.ApiName = settings.Merchant.ApiName;
					oOriginalSettings.Merchant.GetMethod = settings.Merchant.GetMethod;
					oOriginalSettings.Merchant.AmountName = settings.Merchant.AmountName;
					oOriginalSettings.Merchant.FirstSendUrl = settings.Merchant.FirstSendUrl;
					oOriginalSettings.Merchant.RedirectName = settings.Merchant.RedirectName;
					oOriginalSettings.Merchant.SecondSendUrl = settings.Merchant.SecondSendUrl;
					oOriginalSettings.Merchant.FirstSendMethod = settings.Merchant.FirstSendMethod;
					oOriginalSettings.Merchant.IdentityGetName = settings.Merchant.IdentityGetName;
					oOriginalSettings.Merchant.TransactionIdentityName = settings.Merchant.TransactionIdentityName;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ECommerceUnitOfWork.SettingsRepository.Update(oOriginalSettings);

				UnitOfWork.Save();

				// با توجه به آخرين تغييرات، ديگر نيازی به دستور ذيل نمی‌باشد
				//Infrastructure.ApplicationSettings.GetInstanceByCultureLcid();

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));
			}

			return (View(model: settings));
		}
	}
}
