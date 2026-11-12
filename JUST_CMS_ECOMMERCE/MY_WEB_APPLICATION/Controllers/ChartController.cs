using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/04/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Chart)]
	public partial class ChartController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ChartController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			resourceName: Resources.Strings.ActionsKeys.GetGendersCount)]
		public virtual System.Web.Mvc.JsonResult GetGendersCount()
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			var varResult =
				UnitOfWork.GenderRepository.Get()
				.Where(current => current.CultureLcid == CultureLcid)
				.Select(current => new
				{
					Name = current.Name3,
					UserCount = current.Users.Count
				})
				.ToList()
				;

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
