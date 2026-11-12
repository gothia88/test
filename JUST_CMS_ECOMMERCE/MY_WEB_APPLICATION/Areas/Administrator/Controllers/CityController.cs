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
		resourceType: typeof(Resources.Models.City),
		resourceName: Resources.Models.Strings.CityKeys.EntitiesName)]
	public partial class CityController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public CityController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index(System.Guid? stateId)
		{
			// **************************************************
			if (stateId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == stateId.Value)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.State = oState;
			// **************************************************

			var varCities =
				UnitOfWork.CityRepository
				.GetByStateId(stateId.Value)
				.ToList()
				;

			return (View(varCities));
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

			Models.City oCity =
				UnitOfWork.CityRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCity == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCity));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(System.Guid? stateId)
		{
			// **************************************************
			if (stateId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == stateId.Value)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.State = oState;
			// **************************************************

			// **************************************************
			Models.City oCity =
				new Models.City();

			oCity.StateId = stateId.Value;
			// **************************************************

			return (View(oCity));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "IsActive,Ordering,MasterDataCode,StateId,Name")]
			Models.City city)
		{
			Models.City oCity = null;

			// **************************************************
			oCity =
				UnitOfWork.CityRepository
				.GetByStateIdAndName(city.StateId, city.Name)
				;

			if (oCity != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			if (city.MasterDataCode.HasValue)
			{
				oCity =
					UnitOfWork.CityRepository
					.GetByStateIdAndMasterDataCode
					(city.StateId, city.MasterDataCode.Value)
					;

				if (oCity != null)
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
				city.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				city.SetIsActive(city.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.CityRepository.Insert(city);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.City.Index(city.StateId)));
			}

			// **************************************************
			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == city.StateId)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.State = oState;
			// **************************************************

			return (View(city));
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

			Models.City oCity =
				UnitOfWork.CityRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCity == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			ViewBag.State = oCity.State;
			// **************************************************

			return (View(oCity));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,IsActive,Ordering,MasterDataCode,StateId,Name")]
			Models.City city)
		{
			Models.City oCity = null;

			// **************************************************
			oCity =
				UnitOfWork.CityRepository
				.GetByStateIdAndNameExceptId
				(city.StateId, city.Name, city.Id)
				;

			if (oCity != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			if (city.MasterDataCode.HasValue)
			{
				oCity =
					UnitOfWork.CityRepository
					.GetByStateIdAndMasterDataCodeExceptId
					(city.StateId, city.MasterDataCode.Value, city.Id)
					;

				if (oCity != null)
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
			Models.City oOriginalCity =
				UnitOfWork.CityRepository
				.GetById(city.Id)
				;

			if (oOriginalCity == null)
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
				oOriginalCity.Name = city.Name;
				oOriginalCity.Ordering = city.Ordering;
				oOriginalCity.MasterDataCode = city.MasterDataCode;
				// **************************************************

				// **************************************************
				oOriginalCity.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalCity.SetIsActive(city.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CityRepository.Update(oOriginalCity);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.City.Index(city.StateId)));
			}

			// **************************************************
			Models.State oState =
				UnitOfWork.StateRepository.Get()
				.Where(current => current.Id == city.StateId)
				.FirstOrDefault();

			if (oState == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.State = oState;
			// **************************************************

			return (View(city));
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

			Models.City oCity =
				UnitOfWork.CityRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCity == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCity));
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
			Models.City oCity =
				UnitOfWork.CityRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oCity == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.CityRepository.Delete(oCity);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.City.Index(oCity.StateId)));
		}
	}
}
