using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1394/01/09
	/// Changed In Version: 1.8.5
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.AutoComplete)]
	public partial class AutoCompleteController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public AutoCompleteController()
		{
		}

		private System.Collections.Generic.List<string> GetKeywords()
		{
			System.Collections.Generic.List<string> oKeywords =
				new System.Collections.Generic.List<string>();

			if (Request == null)
			{
				return (oKeywords);
			}

			if (Request.Params == null)
			{
				return (oKeywords);
			}

			if (Request.Params.Count == 0)
			{
				return (oKeywords);
			}

			string strFilter =
				Request.Params["filter[filters][0][value]"];

			if (string.IsNullOrWhiteSpace(strFilter) == false)
			{
				strFilter = strFilter.Trim();

				while (strFilter.Contains("  "))
				{
					strFilter =
						strFilter.Replace("  ", " ");
				}

				string[] strKeywords =
					strFilter.Split(' ');

				foreach (string strKeyword in strKeywords)
				{
					oKeywords.Add(strKeyword);
				}
			}

			return (oKeywords);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.AutoCompleteActions),
			resourceName: Resources.Strings.AutoCompleteActionsKeys.GetAllUsersForAdmin)]
		public virtual System.Web.Mvc.JsonResult GetAllUsersForAdmin()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			System.Collections.Generic.List<string> oKeywords = GetKeywords();

			var varEntities =
				UnitOfWork.UserRepository
				.Get()
				.Where(current => current.CultureLcid == CultureLcid)
				;

			foreach (string strKeyword in oKeywords)
			{
				varEntities =
					varEntities
					.Where(current => current.FirstName.Contains(strKeyword) ||
						current.LastName.Contains(strKeyword))
						;
			}

			varEntities =
				varEntities
				.OrderBy(current => current.FirstName)
				.ThenBy(current => current.LastName)
				;

			var varResult =
				varEntities
				.Select(current => new
				{
					Id = current.Id,
					DisplayName =
						current.Gender.Name2 + " " + current.FirstName + " " + current.LastName +
						" - " + current.CellPhoneNumber +
						" - " + current.EmailAddress.ToUpper(),
				})
				.Take(10)
				;

			Infrastructure.JsonResult oJsonResult =
				new Infrastructure.JsonResult(data: varResult);

			return (oJsonResult);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.AutoCompleteActions),
			resourceName: Resources.Strings.AutoCompleteActionsKeys.GetAllPlansForAdmin)]
		public virtual System.Web.Mvc.JsonResult GetAllPlansForAdmin()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			System.Collections.Generic.List<string> oKeywords = GetKeywords();

			var varEntities =
				UnitOfWork.ECommerceUnitOfWork.PlanRepository
				.GetByCultureLcid(CultureLcid)
				;

			foreach (string strKeyword in oKeywords)
			{
				varEntities =
					varEntities
					.Where(current => current.Name.Contains(strKeyword))
					;
			}

			varEntities =
				varEntities
				.OrderBy(current => current.IsDeleted)
				.ThenByDescending(current => current.IsActive)
				.ThenBy(current => current.Name)
				;

			var varResult =
				varEntities
				.Select(current => new ViewModels.AutoComplete.AdminAutoCompleteViewModel
				{
					Id = current.Id.ToString(),

					Name = current.Name,

					IsActive = current.IsActive,
					IsDeleted = current.IsDeleted,
				})
				.Take(20)
				;

			Infrastructure.JsonResult oJsonResult =
				new Infrastructure.JsonResult(data: varResult);

			return (oJsonResult);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.AutoCompleteActions),
			resourceName: Resources.Strings.AutoCompleteActionsKeys.GetAllCulturesForAdmin)]
		public virtual System.Web.Mvc.JsonResult GetAllCulturesForAdmin()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			System.Collections.Generic.List<string> oKeywords = GetKeywords();

			var varEntities =
				UnitOfWork.CultureRepository
				.Get()
				;

			foreach (string strKeyword in oKeywords)
			{
				varEntities =
					varEntities
					.Where(current => current.Name.Contains(strKeyword) ||
						current.DisplayName.Contains(strKeyword) ||
						current.NativeName.Contains(strKeyword))
						;
			}

			varEntities =
				varEntities
				.OrderBy(current => current.IsDeleted)
				.ThenByDescending(current => current.IsActive)
				.ThenBy(current => current.DisplayName)
				;

			var varResult =
				varEntities
				.Select(current => new ViewModels.AutoComplete.AdminAutoCompleteViewModel
				{
					Id = current.Lcid.ToString(),

					Name = current.DisplayName,

					IsActive = current.IsActive,
					IsDeleted = current.IsDeleted,
				})
				.Take(20)
				;

			Infrastructure.JsonResult oJsonResult =
				new Infrastructure.JsonResult(data: varResult);

			return (oJsonResult);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.AutoCompleteActions),
			resourceName: Resources.Strings.AutoCompleteActionsKeys.GetAllRolesForAdmin)]
		public virtual System.Web.Mvc.JsonResult GetAllRolesForAdmin()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			System.Collections.Generic.List<string> oKeywords = GetKeywords();

			var varEntities =
				UnitOfWork.RoleRepository
				.GetByCultureLcidAndLessThanOrEqualToCode
					(CultureLcid, (int)Infrastructure.Sessions.AuthenticatedUser.Role)
				.AsQueryable()
				;

			foreach (string strKeyword in oKeywords)
			{
				varEntities =
					varEntities
					.Where(current => current.Name.Contains(strKeyword))
					;
			}

			varEntities =
				varEntities
				.OrderBy(current => current.IsDeleted)
				.ThenByDescending(current => current.IsActive)
				.ThenBy(current => current.Name)
				;

			var varResult =
				varEntities
				.Select(current => new ViewModels.AutoComplete.AdminAutoCompleteViewModel
				{
					Id = current.Id.ToString(),

					Name = current.Name,

					IsActive = current.IsActive,
					IsDeleted = current.IsDeleted,
				})
				.Take(20)
				;

			Infrastructure.JsonResult oJsonResult =
				new Infrastructure.JsonResult(data: varResult);

			return (oJsonResult);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Resources.AutoCompleteActions),
			resourceName: Resources.Strings.AutoCompleteActionsKeys.GetAllGendersForAdmin)]
		public virtual System.Web.Mvc.JsonResult GetAllGendersForAdmin()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			System.Collections.Generic.List<string> oKeywords = GetKeywords();

			var varEntities =
				UnitOfWork.GenderRepository
				.GetByCultureLcid(CultureLcid)
				.AsQueryable()
				;

			foreach (string strKeyword in oKeywords)
			{
				varEntities =
					varEntities
					.Where(current => current.Name1.Contains(strKeyword))
					;
			}

			varEntities =
				varEntities
				.OrderBy(current => current.IsDeleted)
				.ThenByDescending(current => current.IsActive)
				.ThenBy(current => current.Name1)
				;

			var varResult =
				varEntities
				.Select(current => new ViewModels.AutoComplete.AdminAutoCompleteViewModel
				{
					Id = current.Id.ToString(),

					Name = current.Name1,

					IsActive = current.IsActive,
					IsDeleted = current.IsDeleted,
				})
				.Take(20)
				;

			Infrastructure.JsonResult oJsonResult =
				new Infrastructure.JsonResult(data: varResult);

			return (oJsonResult);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceType: typeof(Resources.AutoCompleteActions),
			resourceName: Resources.Strings.AutoCompleteActionsKeys.GetActiveGenders)]
		public virtual System.Web.Mvc.JsonResult GetActiveGenders()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			System.Collections.Generic.List<string> oKeywords = GetKeywords();

			var varEntities =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid)
				.AsQueryable()
				;

			foreach (string strKeyword in oKeywords)
			{
				varEntities =
					varEntities
					.Where(current => current.Name1.Contains(strKeyword))
					;
			}

			varEntities =
				varEntities
				.OrderBy(current => current.Name1)
				;

			var varResult =
				varEntities
				.Select(current => new ViewModels.AutoComplete.UserAutoCompleteViewModel
				{
					Id = current.Id.ToString(),

					Name = current.Name1,
				})
				.Take(20)
				;

			Infrastructure.JsonResult oJsonResult =
				new Infrastructure.JsonResult(data: varResult);

			return (oJsonResult);
		}
	}
}
