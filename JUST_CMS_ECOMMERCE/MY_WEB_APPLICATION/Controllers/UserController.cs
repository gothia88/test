using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1394/01/05
	/// Changed In Version: 1.8.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.User)]
	public partial class UserController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.GetUsers)]
		public virtual System.Web.Mvc.JsonResult GetData
			(ViewModels.User.SearchViewModel viewModel)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varUsers =
				UnitOfWork.UserRepository
				.GetActiveByCultureLcid(CultureLcid)
				;

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

			var varNewUsers =
				varUsers

				.OrderByDescending(current => current.LastLoginDateTime.Value)

				.Select(current =>
					new ViewModels.User.GetDataViewModel()
					{
						Id = current.Id,

						RoleName = current.Role.Name,
						GenderName = current.Gender.Name2,

						LastName = current.LastName,
						FirstName = current.FirstName,

						HasAvatar = current.HasAvatar,
						IsVerified = current.IsVerified,
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.User.GetDataViewModel>(varNewUsers);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
