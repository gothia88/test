using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/04/25
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.Country),
		resourceName: Resources.Models.Strings.CountryKeys.EntitiesName)]
	public partial class CountryController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public CountryController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varCountries =
				UnitOfWork.CountryRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varCountries));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.FixText)]
		public virtual System.Web.Mvc.ActionResult FixText()
		{
			var varCountries =
				UnitOfWork.CountryRepository.Get()
				.ToList()
				;

			foreach (Models.Country oCountry in varCountries)
			{
				oCountry.Name =
					Infrastructure.Utility.FixText(oCountry.Name);

				foreach(Models.State oState in oCountry.States)
				{
					oState.Name =
						Infrastructure.Utility.FixText(oState.Name);

					foreach (Models.City oCity in oState.Cities)
					{
						oCity.Name =
							Infrastructure.Utility.FixText(oCity.Name);
					}
				}
			}

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.Country.Index()));
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

			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCountry));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.Country oCountry =
				new Models.Country();
			// **************************************************

			return (View(oCountry));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "IsActive,Ordering,MasterDataCode,Name")]
			Models.Country country)
		{
			Models.Country oCountry = null;

			// **************************************************
			oCountry =
				UnitOfWork.CountryRepository
				.GetByCultureLcidAndName(CultureLcid, country.Name)
				;

			if (oCountry != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			if (country.MasterDataCode.HasValue)
			{
				oCountry =
					UnitOfWork.CountryRepository
					.GetByCultureLcidAndMasterDataCode
					(CultureLcid, country.MasterDataCode.Value)
					;

				if (oCountry != null)
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
				country.CultureLcid = CultureLcid;

				country.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				country.SetIsActive(country.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.CountryRepository.Insert(country);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Country.Index()));
			}

			return (View(country));
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

			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCountry));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,IsActive,Ordering,MasterDataCode,Name")]
			Models.Country country)
		{
			Models.Country oCountry = null;

			// **************************************************
			oCountry =
				UnitOfWork.CountryRepository
				.GetByCultureLcidAndNameExceptId
				(CultureLcid, country.Name, country.Id)
				;

			if (oCountry != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			if (country.MasterDataCode.HasValue)
			{
				oCountry =
					UnitOfWork.CountryRepository
					.GetByCultureLcidAndMasterDataCodeExceptId
					(CultureLcid, country.MasterDataCode.Value, country.Id)
					;

				if (oCountry != null)
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
			Models.Country oOriginalCountry =
				UnitOfWork.CountryRepository
				.GetById(country.Id)
				;

			if (oOriginalCountry == null)
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
				oOriginalCountry.Name = country.Name;
				oOriginalCountry.Ordering = country.Ordering;
				oOriginalCountry.MasterDataCode = country.MasterDataCode;
				// **************************************************

				// **************************************************
				oOriginalCountry.CultureLcid = CultureLcid;

				oOriginalCountry.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalCountry.SetIsActive(country.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CountryRepository.Update(oOriginalCountry);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Country.Index()));
			}

			return (View(country));
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

			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCountry));
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
			Models.Country oCountry =
				UnitOfWork.CountryRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oCountry == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.CountryRepository.Delete(oCountry);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.Country.Index()));
		}
	}
}
