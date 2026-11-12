using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/04/25
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.State),
		resourceName: Resources.Models.Strings.StateKeys.EntitiesName)]
	public partial class StateController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public StateController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index(System.Guid? countryId)
		{
			// **************************************************
			if (countryId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == countryId.Value)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Country = oCountry;
			// **************************************************

			var varStates =
				UnitOfWork.StateRepository
				.GetByCountryId(countryId.Value)
				.ToList()
				;

			return (View(varStates));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oState));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(System.Guid? countryId)
		{
			// **************************************************
			if (countryId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == countryId.Value)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Country = oCountry;
			// **************************************************

			// **************************************************
			Models.State oState =
				new Models.State();

			oState.CountryId = countryId.Value;
			// **************************************************

			return (View(oState));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "CountryId,IsActive,Ordering,MasterDataCode,Name")]
			Models.State state)
		{
			Models.State oState = null;

			// **************************************************
			oState =
				UnitOfWork.StateRepository
				.GetByCountryIdAndName(state.CountryId, state.Name)
				;

			if (oState != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			if (state.MasterDataCode.HasValue)
			{
				oState =
					UnitOfWork.StateRepository
					.GetByCountryIdAndMasterDataCode
					(state.CountryId, state.MasterDataCode.Value)
					;

				if (oState != null)
				{
					// مقدار فيلد کد تکراری است! لطفا از مقدار ديگری استفاده نماييد
					string strErrorMessage =
						string.Format(Resources.Messages.DuplicateError,
						Resources.Models.General.MasterDataCode);

					ModelState.AddModelError("MasterDataCode", strErrorMessage);
				}
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				state.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				state.SetIsActive(state.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.StateRepository.Insert(state);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.State.Index(state.CountryId)));
			}

			// **************************************************
			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == state.CountryId)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Country = oCountry;
			// **************************************************

			return (View(state));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			ViewBag.Country = oState.Country;
			// **************************************************

			return (View(oState));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,IsActive,Ordering,MasterDataCode,CountryId,Name")]
			Models.State state)
		{
			Models.State oState = null;

			// **************************************************
			oState =
				UnitOfWork.StateRepository
				.GetByCountryIdAndNameExceptId
				(state.CountryId, state.Name, state.Id)
				;

			if (oState != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			if (state.MasterDataCode.HasValue)
			{
				oState =
					UnitOfWork.StateRepository
					.GetByCountryIdAndMasterDataCodeExceptId
					(state.CountryId, state.MasterDataCode.Value, state.Id)
					;

				if (oState != null)
				{
					// مقدار فيلد کد تکراری است! لطفا از مقدار ديگری استفاده نماييد
					string strErrorMessage =
						string.Format(Resources.Messages.DuplicateError,
						Resources.Models.General.MasterDataCode);

					ModelState.AddModelError("MasterDataCode", strErrorMessage);
				}
			}
			// **************************************************

			// **************************************************
			Models.State oOriginalState =
				UnitOfWork.StateRepository
				.GetById(state.Id)
				;

			if (oOriginalState == null)
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
				oOriginalState.Name = state.Name;
				oOriginalState.Ordering = state.Ordering;
				oOriginalState.MasterDataCode = state.MasterDataCode;
				// **************************************************

				// **************************************************
				oOriginalState.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalState.SetIsActive(state.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.StateRepository.Update(oOriginalState);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.State.Index(state.CountryId)));
			}

			// **************************************************
			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == state.CountryId)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Country = oCountry;
			// **************************************************

			return (View(state));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oState));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
		{
			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.StateRepository.Delete(oState);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.State.Index(oState.CountryId)));
		}
	}
}
