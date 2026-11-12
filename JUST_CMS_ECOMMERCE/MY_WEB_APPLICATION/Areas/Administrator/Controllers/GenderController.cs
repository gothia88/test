using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.Gender)]
	public partial class GenderController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public GenderController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varGenders =
				UnitOfWork.GenderRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varGenders));
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

			Models.Gender oGender =
				UnitOfWork.GenderRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oGender == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGender));
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

			Models.Gender oGender =
				UnitOfWork.GenderRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oGender == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGender));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,IsActive,Ordering,MasterDataCode,Name1,Name2,Name3,Name4")]
			Models.Gender gender)
		{
			// **************************************************
			if (gender.MasterDataCode.HasValue)
			{
				Models.Gender oGender =
					UnitOfWork.GenderRepository
					.GetByCultureLcidAndMasterDataCodeExceptId
					(CultureLcid, gender.MasterDataCode.Value, gender.Id)
					;

				if (oGender != null)
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
			Models.Gender oOriginalGender =
				UnitOfWork.GenderRepository.GetById(gender.Id);

			if (oOriginalGender == null)
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
				oOriginalGender.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalGender.Name1 = gender.Name1;
				oOriginalGender.Name2 = gender.Name2;
				oOriginalGender.Name3 = gender.Name3;
				oOriginalGender.Name4 = gender.Name4;
				oOriginalGender.Ordering = gender.Ordering;
				oOriginalGender.MasterDataCode = gender.MasterDataCode;
				// **************************************************

				// **************************************************
				oOriginalGender.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oOriginalGender.SetIsActive(gender.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GenderRepository.Update(oOriginalGender);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Gender.Index()));
			}

			return (View(gender));
		}
	}
}
