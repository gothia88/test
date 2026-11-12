using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/12/09
	/// Changed In Version: 1.8.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Resources.Models.UserAddress),
		resourceName: Resources.Models.Strings.UserAddressKeys.EntitiesName)]
	public partial class UserAddressController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserAddressController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varUserAddresses =
				UnitOfWork.UserAddressRepository
				.GetByUserId(Infrastructure.Sessions.AuthenticatedUser.Id)
				.ToList()
				;

			return (View(model: varUserAddresses));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.UserAddress oUserAddress =
				UnitOfWork.UserAddressRepository
				.GetById(id.Value);

			if (oUserAddress == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oUserAddress.UserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			return (View(model: oUserAddress));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.UserAddress oUserAddress =
				new Models.UserAddress();
			// **************************************************

			return (View(model: oUserAddress));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.UserAddress userAddress)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				userAddress.UserId =
					Infrastructure.Sessions.AuthenticatedUser.Id;

				userAddress.Title =
					Infrastructure.Utility
					.SimpleFixSuchAsAddress(userAddress.Title);

				userAddress.Address =
					Infrastructure.Utility
					.SimpleFixSuchAsAddress(userAddress.Address);

				userAddress.PhoneNumbers =
					Infrastructure.Utility
					.SimpleFixSuchAsAddress(userAddress.PhoneNumbers);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserAddressRepository.Insert(userAddress);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.UserAddress.Index()));
			}

			return (View(model: userAddress));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.UserAddress oUserAddress =
				UnitOfWork.UserAddressRepository
				.GetById(id.Value);

			if (oUserAddress == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oUserAddress.UserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			oUserAddress.Address =
				oUserAddress.Address
				.Replace("<br />", System.Environment.NewLine);

			if (string.IsNullOrWhiteSpace(oUserAddress.PhoneNumbers) == false)
			{
				oUserAddress.PhoneNumbers =
					oUserAddress.PhoneNumbers
					.Replace("<br />", System.Environment.NewLine);
			}

			return (View(model: oUserAddress));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.UserAddress userAddress)
		{
			// **************************************************
			Models.UserAddress oOriginalUserAddress =
				UnitOfWork.UserAddressRepository
				.GetById(userAddress.Id);

			if (oOriginalUserAddress == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oOriginalUserAddress.UserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalUserAddress.IsPublic = userAddress.IsPublic;

				oOriginalUserAddress.Title =
					Infrastructure.Utility
					.SimpleFixSuchAsAddress(userAddress.Title);

				oOriginalUserAddress.Address =
					Infrastructure.Utility
					.SimpleFixSuchAsAddress(userAddress.Address);

				oOriginalUserAddress.PhoneNumbers =
					Infrastructure.Utility
					.SimpleFixSuchAsAddress(userAddress.PhoneNumbers);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserAddressRepository.Update(oOriginalUserAddress);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.UserAddress.Index()));
			}

			return (View(userAddress));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.UserAddress oUserAddress =
				UnitOfWork.UserAddressRepository
				.GetById(id.Value);

			if (oUserAddress == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oUserAddress.UserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			return (View(model: oUserAddress));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.UserAddress oUserAddress =
				UnitOfWork.UserAddressRepository
				.GetById(id.Value);

			if (oUserAddress == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oUserAddress.UserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			UnitOfWork.UserAddressRepository.Delete(oUserAddress);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.UserAddress.Index()));
		}
	}
}
