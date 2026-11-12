using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.ECommerce.Controllers
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1394/01/22
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.ECommerce.Plan),
		resourceName: Resources.Models.ECommerce.Strings.PlanKeys.EntitiesName)]
	public partial class PlanController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PlanController()
		{
		}

		#region General
		private Infrastructure.AccessToEntityResult<Models.ECommerce.Plan>
			CheckAccessToEntity(System.Guid? id)
		{
			Infrastructure.AccessToEntityResult<Models.ECommerce.Plan> oResult =
				new Infrastructure.AccessToEntityResult<Models.ECommerce.Plan>();

			if (id.HasValue == false)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest));

				return (oResult);
			}

			Models.ECommerce.Plan oPlan =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetById(id.Value);

			if (oPlan == null)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.NotFound));

				return (oResult);
			}

			oResult.Entity = oPlan;

			return (oResult);
		}
		#endregion /General

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varPlans =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varPlans));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Plan>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Plan oPlan = oResult.Entity;
			// **************************************************

			return (View(model: oPlan));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.ECommerce.Plan oPlan =
				new Models.ECommerce.Plan();
			// **************************************************

			return (View(model: oPlan));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.ECommerce.Plan plan)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				plan.CultureLcid = CultureLcid;

				plan.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				plan.SetIsActive
					(plan.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				//plan.SetIsDeleted
				//	(plan.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ECommerceUnitOfWork.PlanRepository.Insert(plan);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ECommerce.Plan.Index()));
			}

			return (View(model: plan));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Plan>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Plan oPlan = oResult.Entity;
			// **************************************************

			return (View(model: oPlan));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.ECommerce.Plan plan)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				plan.CultureLcid = CultureLcid;

				plan.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				plan.SetIsActive
					(plan.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				//plan.SetIsDeleted
				//	(plan.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ECommerceUnitOfWork.PlanRepository.Update(plan);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ECommerce.Plan.Index()));
			}

			return (View(plan));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.ECommerce.Plan>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Plan oPlan = oResult.Entity;
			// **************************************************

			return (View(model: oPlan));
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
			Infrastructure.AccessToEntityResult<Models.ECommerce.Plan>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.ECommerce.Plan oPlan = oResult.Entity;
			// **************************************************

			try
			{
				UnitOfWork.ECommerceUnitOfWork.PlanRepository.Delete(oPlan);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ECommerce.Plan.Index()));
			}
			catch
			{
				string strErrorMessage =
					"از اين اطلاعات در جای ديگری استفاده شده است! لذا شما قادر به حذف آن نمی‌باشيد.";

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(model: oPlan));
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.ECommerce.Plan),
			resourceName: Resources.Models.ECommerce.Strings.PlanKeys.DisplayPlans)]
		public virtual System.Web.Mvc.ActionResult DisplayPlans()
		{
			if (Infrastructure.ApplicationSettings.Instance.IsECommerceSystemEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var varPlans =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetActiveByCultureLcid(cultureLcid: CultureLcid)
				.ToList()
				;

			return (View(model: varPlans));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.Models.ECommerce.Plan),
			resourceName: Resources.Models.ECommerce.Strings.PlanKeys.DisplayPlan)]
		public virtual System.Web.Mvc.ActionResult DisplayPlan(System.Guid? id)
		{
			if (Infrastructure.ApplicationSettings.Instance.IsECommerceSystemEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.ECommerce.Plan oPlan =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetById(id.Value);

			if ((oPlan == null) || (oPlan.IsDeleted))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPlan));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceType: typeof(Resources.Models.ECommerce.Plan),
			resourceName: Resources.Models.ECommerce.Strings.PlanKeys.SelectFreePlan)]
		public virtual System.Web.Mvc.ActionResult SelectFreePlan(System.Guid? id)
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

			if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User)
			{
				// فقط کاربران عادی پايگاه، قادر به خريد طرح می‌باشند
				// امکان خريد طرح، برای مسوولين پايگاه وجود ندارد
				strErrorMessage =
					Resources.Models.ECommerce.Plan.Error001;

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}

			if (oPlan.Price != 0)
			{
				// انتخاب طرح، صرفا برای طرح‌های رايگان امکان‌پذير است
				// برای استفاده از طرح‌های غير رايگان، بايد آنها را خريداری نماييد
				strErrorMessage =
					Resources.Models.ECommerce.Plan.Error003;

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}
			// **************************************************
			// *** /Check Business ******************************
			// **************************************************

			try
			{
				Models.User oUser =
					UnitOfWork.UserRepository
					.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

				oUser.PlanId = oPlan.Id;
				oUser.StartPlanDateTime.Value = Models.Utility.Now;
				oUser.FinishPlanDateTime.Value = Models.Utility.Now.AddMonths(oPlan.DurationInMonth);

				UnitOfWork.UserRepository.Update(oUser);

				UnitOfWork.Save();

				Infrastructure.Sessions.AuthenticatedUser =
					new Infrastructure.AuthenticatedUser(oUser);

				// از اين لحظه شما می‌توانيد تا مدت {0} ماه
				// از طرح انتخابی خود {1} استفاده نماييد
				string strInformationMessage =
					string.Format(Resources.Models.ECommerce.Plan.Information001,
					oPlan.DurationInMonth, oPlan.Name);

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}
			catch
			{
				strErrorMessage =
					Resources.Messages.UnexpectedError;

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(viewName: MVC.Shared.Views.DisplayMessage));
			}
		}
	}
}
