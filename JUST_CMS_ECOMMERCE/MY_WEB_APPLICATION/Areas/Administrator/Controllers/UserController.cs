using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.4.5
	/// Update Date: 1394/03/06
	/// Changed In Version: 1.9.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.User)]
	public partial class UserController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserController()
		{
		}

		#region General
		private Infrastructure.AccessToEntityResult<Models.User>
			CheckAccessToEntity(System.Guid? id)
		{
			Infrastructure.AccessToEntityResult<Models.User> oResult =
				new Infrastructure.AccessToEntityResult<Models.User>();

			if (id.HasValue == false)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest));

				return (oResult);
			}

			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(id.Value);

			if (oUser == null)
			{
				oResult.ActionResult =
					RedirectToAction(MVC.Error.Display(System.Net.HttpStatusCode.NotFound));

				return (oResult);
			}

			oResult.Entity = oUser;

			return (oResult);
		}

		/// <summary>
		/// Version: 1.4.4
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		private void SetViewBagsForDropDownLists(Models.User user = null)
		{
			// **************************************************
			object objPlanId = null;

			if (user != null)
			{
				objPlanId = user.PlanId;
			}

			var varPlans =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetForDropDownList(CultureLcid,
				Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
				;

			ViewBag.PlanId =
				new System.Web.Mvc.SelectList
					(varPlans, "Id", "ListItemTextForSiteResponsibles", objPlanId);
			// **************************************************

			// **************************************************
			object objRoleId = null;

			if (user != null)
			{
				objRoleId = user.RoleId;
			}

			var varRoles =
				UnitOfWork.RoleRepository
				.GetForDropDownList(CultureLcid,
				Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible,
				Infrastructure.Sessions.AuthenticatedUser.Role)
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(varRoles, "Id", "ListItemTextForSiteResponsibles", objRoleId);
			// **************************************************

			// **************************************************
			object objGenderId = null;

			if (user != null)
			{
				objGenderId = user.GenderId;
			}

			var varGenders =
				UnitOfWork.GenderRepository
				.GetForDropDownList(CultureLcid,
				Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
				;

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "ListItemTextForSiteResponsibles", objGenderId);
			// **************************************************

			// **************************************************
			object objCultureLcid = null;

			if (user != null)
			{
				objCultureLcid = user.CultureLcid;
			}

			var varCultures =
				UnitOfWork.CultureRepository
				.GetForDropDownList(Infrastructure.Sessions.AuthenticatedUser.IsSiteResponsible)
				;

			ViewBag.CultureLcid =
				new System.Web.Mvc.SelectList
					(varCultures, "Lcid", "ListItemTextForSiteResponsibles", objCultureLcid);
			// **************************************************
		}
		#endregion /General

		/// <summary>
		/// Version: 1.4.4
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			SetViewBagsForDropDownLists();

			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.GetUsers)]
		public virtual System.Web.Mvc.JsonResult GetData
			(ViewModels.Areas.Administrator.User.SearchViewModel viewModel)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varUsers =
				UnitOfWork.UserRepository
				.Get()
				.Where(current => current.Role.Code <= (int)Infrastructure.Sessions.AuthenticatedUser.Role)
				;

			// **************************************************
			if (viewModel.PlanId.HasValue)
			{
				varUsers =
					varUsers
					.Where(current => current.PlanId == viewModel.PlanId.Value)
					;
			}
			// **************************************************

			// **************************************************
			if (viewModel.RoleId.HasValue)
			{
				varUsers =
					varUsers
					.Where(current => current.RoleId == viewModel.RoleId.Value)
					;
			}
			// **************************************************

			// **************************************************
			if (viewModel.GenderId.HasValue)
			{
				varUsers =
					varUsers
					.Where(current => current.GenderId == viewModel.GenderId.Value)
					;
			}
			// **************************************************

			// **************************************************
			if (viewModel.CultureLcid.HasValue)
			{
				varUsers =
					varUsers
					.Where(current => current.CultureLcid == viewModel.CultureLcid.Value)
					;
			}
			// **************************************************

			// **************************************************
			viewModel.FirstName =
				Dtx.Text.Utility.FixText(viewModel.FirstName);

			if (string.IsNullOrWhiteSpace(viewModel.FirstName) == false)
			{
				varUsers =
					varUsers
					.Where(current => current.FirstName.Contains(viewModel.FirstName))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.LastName =
				Dtx.Text.Utility.FixText(viewModel.LastName);

			if (string.IsNullOrWhiteSpace(viewModel.LastName) == false)
			{
				varUsers =
					varUsers
					.Where(current => current.LastName.Contains(viewModel.LastName))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.EmailAddress =
				Dtx.Text.Utility.FixText(viewModel.EmailAddress);

			if (string.IsNullOrWhiteSpace(viewModel.EmailAddress) == false)
			{
				varUsers =
					varUsers
					.Where(current => current.EmailAddress.Contains(viewModel.EmailAddress))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.CellPhoneNumber =
				Dtx.Text.Utility.FixText(viewModel.CellPhoneNumber);

			if (string.IsNullOrWhiteSpace(viewModel.CellPhoneNumber) == false)
			{
				varUsers =
					varUsers
					.Where(current => current.CellPhoneNumber.Contains(viewModel.CellPhoneNumber))
					;
			}
			// **************************************************

			var varNewUsers =
				varUsers

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Select(current =>
					new ViewModels.Areas.Administrator.User.GetDataViewModel()
					{
						Id = current.Id,

						RoleName = current.Role.Name,
						GenderName = current.Gender.Name2,

						PlanId = current.PlanId,
						PlanName = current.Plan.Name,

						LastName = current.LastName,
						FirstName = current.FirstName,

						IsActive = current.IsActive,
						HasAvatar = current.HasAvatar,
						IsDeleted = current.IsDeleted,
						IsVerified = current.IsVerified,
						IsDirectContactable = current.IsDirectContactable,
						IsAccountTerminated = current.IsAccountTerminated,
						IsEmailAddressVerified = current.IsEmailAddressVerified,
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.User.GetDataViewModel>(varNewUsers);

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
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			return (View(model: oUser));
		}

		/// <summary>
		/// Version: 1.4.4
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.User oUser =
				new Models.User();
			// **************************************************

			SetViewBagsForDropDownLists(oUser);

			return (View(oUser));
		}

		/// <summary>
		/// Version: 1.4.4
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.User user)
		{
			Models.User oUser = null;

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByEmailAddress(user.EmailAddress);

			if (oUser != null)
			{
				// مقدار فيلد نشانی پست الکترونيکی تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.User.EmailAddress);

				ModelState.AddModelError("EmailAddress", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByCellPhoneNumber(user.CellPhoneNumber);

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
				// **************************************************
				// **************************************************
				// **************************************************
				Models.User oNewUser =
					Models.User.CreateUser
					(user.EmailAddress, user.CellPhoneNumber, user.Password);

				oNewUser.CultureLcid = user.CultureLcid;

				oNewUser.RoleId = user.RoleId;
				oNewUser.GenderId = user.GenderId;

				oNewUser.JobTitle = user.JobTitle;
				oNewUser.LastName = user.LastName;
				oNewUser.FirstName = user.FirstName;
				oNewUser.AdministratorDescription = user.AdministratorDescription;

				oNewUser.IsAccountExpirable = user.IsAccountExpirable;
				oNewUser.AccountExpireDate.Value = user.AccountExpireDate.Value;

				oNewUser.IsInBlackList = user.IsInBlackList;
				oNewUser.IsDirectContactable = user.IsDirectContactable;
				oNewUser.IsEmailAddressVerified = user.IsEmailAddressVerified;
				oNewUser.IsCellPhoneNumberVerified = user.IsCellPhoneNumberVerified;

				oNewUser.IsAccountTerminated = user.IsAccountTerminated;

				if (user.IsAccountTerminated)
				{
					oNewUser.AccountTerminateDateTime.Value = Models.Utility.Now;
				}

				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oNewUser.IsSystem = user.IsSystem;
				}

				// **************************************************
				Models.Role oRole =
					UnitOfWork.RoleRepository
					.GetById(user.RoleId);

				if (oRole.IsSiteResponsible)
				{
					oNewUser.PlanId = null;
					oNewUser.StartPlanDateTime.Value = null;
					oNewUser.FinishPlanDateTime.Value = null;
				}
				else
				{
					Models.ECommerce.Plan oFreePlan =
						MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.GetFreePlan();

					MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.SetPlanForUser(oNewUser, oFreePlan);
				}
				// **************************************************

				oNewUser.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oNewUser.SetIsActive
					(user.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				oNewUser.SetIsDeleted
					(user.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);

				oNewUser.SetIsVerified
					(user.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserRepository.Insert(oNewUser);

				UnitOfWork.Save();

				if (oNewUser.IsCellPhoneNumberVerified == false)
				{
					Infrastructure.NotificationService
						.SendCellPhoneNumberVerificationKey(oNewUser);
				}

				if (oNewUser.IsEmailAddressVerified == false)
				{
					Infrastructure.NotificationService
						.SendEmailAddressVerificationKey(oNewUser, user.Password);
				}

				return (RedirectToAction(MVC.Administrator.User.Index()));
			}

			SetViewBagsForDropDownLists(user);

			return (View(model: user));
		}

		/// <summary>
		/// Version: 1.4.4
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			SetViewBagsForDropDownLists(oUser);

			return (View(model: oUser));
		}

		/// <summary>
		/// Version: 1.4.4
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.User user)
		{
			Models.User oUser = null;

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByEmailAddressExceptId(user.EmailAddress, user.Id);

			if (oUser != null)
			{
				// مقدار فيلد نشانی پست الکترونيکی تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.User.EmailAddress);

				ModelState.AddModelError("EmailAddress", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByCellPhoneNumberExceptId(user.CellPhoneNumber, user.Id);

			if (oUser != null)
			{
				// مقدار فيلد شماره تلفن همراه تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.User.CellPhoneNumber);

				ModelState.AddModelError("CellPhoneNumber", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			Models.User oOriginalUser =
				UnitOfWork.UserRepository.GetById(user.Id);

			if (oOriginalUser == null)
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
				oOriginalUser.RoleId = user.RoleId;
				oOriginalUser.GenderId = user.GenderId;
				oOriginalUser.CultureLcid = user.CultureLcid;

				oOriginalUser.JobTitle = user.JobTitle;
				oOriginalUser.LastName = user.LastName;
				oOriginalUser.FirstName = user.FirstName;
				oOriginalUser.EmailAddress = user.EmailAddress;
				oOriginalUser.CellPhoneNumber = user.CellPhoneNumber;
				oOriginalUser.AdministratorDescription = user.AdministratorDescription;

				oOriginalUser.IsAccountExpirable = user.IsAccountExpirable;
				oOriginalUser.AccountExpireDate.Value = user.AccountExpireDate.Value;

				oOriginalUser.IsInBlackList = user.IsInBlackList;
				oOriginalUser.IsDirectContactable = user.IsDirectContactable;
				oOriginalUser.IsEmailAddressVerified = user.IsEmailAddressVerified;
				oOriginalUser.IsCellPhoneNumberVerified = user.IsCellPhoneNumberVerified;

				if (user.IsAccountTerminated == false)
				{
					oOriginalUser.IsAccountTerminated = false;
					oOriginalUser.AccountTerminateDateTime.Value = null;
				}
				else
				{
					if (oOriginalUser.IsAccountTerminated == false)
					{
						oOriginalUser.IsAccountTerminated = true;
						oOriginalUser.AccountTerminateDateTime.Value = Models.Utility.Now;
					}
				}

				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalUser.IsSystem = user.IsSystem;
				}

				// **************************************************
				Models.Role oRole =
					UnitOfWork.RoleRepository
					.GetById(user.RoleId);

				if (oRole.IsSiteResponsible)
				{
					oOriginalUser.PlanId = null;
					oOriginalUser.StartPlanDateTime.Value = null;
					oOriginalUser.FinishPlanDateTime.Value = null;
				}
				else
				{
					if (oOriginalUser.PlanId.HasValue == false)
					{
						Models.ECommerce.Plan oFreePlan =
							MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.GetFreePlan();

						MY_WEB_APPLICATION.Areas.ECommerce.BLL.Plan.SetPlanForUser(oOriginalUser, oFreePlan);
					}
				}
				// **************************************************

				oOriginalUser.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalUser.SetIsActive
					(user.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalUser.SetIsDeleted
					(user.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalUser.SetIsVerified
					(user.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserRepository.Update(oOriginalUser);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.User.Index()));
			}

			SetViewBagsForDropDownLists(user);

			return (View(model: user));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			return (View(model: oUser));
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
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			if (oUser.IsSystem)
			{
				if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer)
				{
					string strErrorMessage =
						"فقط کاربرانی که نقش کاربری آنها، برنامه‌نويس است، قادر به حذف کاربران سيستمی می‌باشند!";

					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

					return (View(model: oUser));
				}
			}

			if (oUser.Id == Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				string strErrorMessage =
					"شما قادر به حذف اطلاعات خودتان نمی‌باشيد!";

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(model: oUser));
			}

			try
			{
				UnitOfWork.UserRepository.Delete(oUser);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.User.Index()));
			}
			catch
			{
				string strErrorMessage =
					"از اطلاعات اين کاربر، در جای ديگری استفاده شده است! لذا شما قادر به حذف آن نمی‌باشيد.";

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error, strErrorMessage));

				return (View(model: oUser));
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			ViewBag.Groups =
				UnitOfWork.GroupRepository.Get()
				.OrderBy(current => current.Name)
				.ToList()
				;

			switch (oUser.Role.CodeEnum)
			{
				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Programmer:
				case Models.Enums.Roles.Administrator:
				{
					// حق دسترسی اين کاربر، به کل سيستم، کامل بوده
					// و نيازی به تعريف حق دسترسی برای وی وجود ندارد
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, Resources.Models.User.Warning001));

					break;
				}
			}

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups
			(System.Guid? id, System.Guid[] checkBoxes)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			var oGroups =
				UnitOfWork.GroupRepository.Get()
				.OrderBy(current => current.Name)
				.ToList()
				;

			oUser.Groups.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varGroup =
						oGroups
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varGroup != null)
					{
						oUser.Groups.Add(varGroup);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Groups = oGroups;

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectProjectActionsForUser)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			switch (oUser.Role.CodeEnum)
			{
				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Programmer:
				case Models.Enums.Roles.Administrator:
				{
					// حق دسترسی اين کاربر، به کل سيستم، کامل بوده
					// و نيازی به تعريف حق دسترسی برای وی وجود ندارد
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, Resources.Models.User.Warning001));

					break;
				}
			}

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/User'>{0}</a>",
				Resources.Controllers.Administrator_User);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oUser.Id,
					name: oUser.FullName,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Resources.Models.User.EntityName,
					selectedProjectActions: oUser.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectProjectActionsForUser)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions
			(System.Guid? id, System.Guid[] checkBoxes)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			var oProjectActions =
				UnitOfWork.ProjectActionRepository.Get()
				.ToList()
				;

			oUser.ProjectActions.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varProjectAction =
						oProjectActions
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varProjectAction != null)
					{
						oUser.ProjectActions.Add(varProjectAction);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/User'>{0}</a>",
				Resources.Controllers.Administrator_User);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oUser.Id,
					name: oUser.FullName,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Resources.Models.User.EntityName,
					selectedProjectActions: oUser.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
					Models.Enums.AccessTypes.Special)
				.OrderBy(current => current.Title)
				.ToList()
				;

			switch (oUser.Role.CodeEnum)
			{
				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Programmer:
				case Models.Enums.Roles.Administrator:
				{
					// حق دسترسی اين کاربر، به کل سيستم، کامل بوده
					// و نيازی به تعريف حق دسترسی برای وی وجود ندارد
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, Resources.Models.User.Warning001));

					break;
				}
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages
			(System.Guid? id, System.Guid[] checkBoxes)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			var varPages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.ToList()
				;

			oUser.Pages.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varPage =
						varPages
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varPage != null)
					{
						oUser.Pages.Add(varPage);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
					Models.Enums.AccessTypes.Special)
				.OrderBy(current => current.Title)
				.ToList()
				;

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword(System.Guid? id)
		{
			// **************************************************
			Infrastructure.AccessToEntityResult<Models.User>
				oResult = CheckAccessToEntity(id);

			if (oResult.ActionResult != null)
			{
				return (oResult.ActionResult);
			}

			Models.User oUser = oResult.Entity;
			// **************************************************

			// **************************************************
			ViewModels.Areas.Administrator.User.ChangePasswordViewModel oChangePasswordViewModel =
				new ViewModels.Areas.Administrator.User.ChangePasswordViewModel();

			oChangePasswordViewModel.UserId = oUser.Id;
			// **************************************************

			return (View(oChangePasswordViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword
			(ViewModels.Areas.Administrator.User.ChangePasswordViewModel viewModel)
		{
			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(viewModel.UserId);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			string strHashOfNewPassword =
				Dtx.Security.Hashing.GetSha1(viewModel.NewPassword);

			oUser.Password = strHashOfNewPassword;

			UnitOfWork.Save();

			// گذرواژه جديد برای کاربر ثبت گرديد
			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information,
				Resources.Models.User.Information004));

			// **************************************************
			ViewModels.Areas.Administrator.User.ChangePasswordViewModel oChangePasswordViewModel =
				new ViewModels.Areas.Administrator.User.ChangePasswordViewModel();

			oChangePasswordViewModel.UserId = oUser.Id;
			// **************************************************

			return (View(oChangePasswordViewModel));
		}

		/// <summary>
		/// Version: 1.4.5
		/// Update Date: 1394/03/06
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.User),
			resourceName: Resources.Models.Strings.UserKeys.DeleteNotVerifiedEmailAddressUsers)]
		public virtual System.Web.Mvc.ActionResult DeleteNotVerifiedEmailAddressUsers()
		{
			// **************************************************
			ViewModels.Areas.Administrator.User.DeleteNotVerifiedEmailAddressUsersViewModel oViewModel =
				new ViewModels.Areas.Administrator.User.DeleteNotVerifiedEmailAddressUsersViewModel();

			oViewModel.Hours = 72;
			// **************************************************

			return (View(model: oViewModel));
		}

		/// <summary>
		/// Version: 1.4.5
		/// Update Date: 1394/03/06
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.Models.User),
			resourceName: Resources.Models.Strings.UserKeys.DeleteNotVerifiedEmailAddressUsers)]
		public virtual System.Web.Mvc.ActionResult DeleteNotVerifiedEmailAddressUsers
			(ViewModels.Areas.Administrator.User.DeleteNotVerifiedEmailAddressUsersViewModel viewModel)
		{
			// **************************************************
			if (viewModel.Hours < 1)
			{
				string strErrorMessage =
					"کاربرانی که کمتر از يک ساعت، در پايگاه ثبت‌نام نموده‌اند را نمی‌توانيد حذف نماييد!";

				ModelState.AddModelError("Hours", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				string strQuery =
					string.Format("DELETE FROM {0}.Users WHERE IsEmailAddressVerified = 0 AND InsertDateTime_Value < '{1:yyyy/MM/dd HH:mm:ss}'",
					Models.Constants.SCHEMA_GENERAL, Models.Utility.Now.AddHours(-viewModel.Hours));

				try
				{
					int intResult =
						UnitOfWork.ExecuteSqlCommand(strQuery);

					string strInformationMessage =
						string.Format("تعداد [{0}] کاربر با موفقيت حذف گرديدند.",
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
