using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.ApplicationLog)]
	public partial class ApplicationLogController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ApplicationLogController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit()
		{
			ViewModels.Areas.Administrator.ApplicationLog.EditViewModel oViewModel =
				new ViewModels.Areas.Administrator.ApplicationLog.EditViewModel();

			string strApplicationLogRootRelativePathName =
				"~/App_Data/Logs/Application.log";

			string strApplicationLogPathName =
				Server.MapPath(strApplicationLogRootRelativePathName);

			oViewModel.Log =
				Dtx.IO.File.Read(strApplicationLogPathName);

			return (View(oViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			(ViewModels.Areas.Administrator.ApplicationLog.EditViewModel viewModel)
		{
			string strPath = string.Empty;
			string strRootRelativePath = string.Empty;

			strRootRelativePath = "~/App_Data";

			strPath =
				Server.MapPath(strRootRelativePath);

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			strPath =
				string.Format("{0}\\Logs", strPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);
			}

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			string strPathName =
				string.Format("{0}\\Application.log", strPath);

			Dtx.IO.File.Overwrite(strPathName, viewModel.Log);

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			return (View(viewModel));
		}
	}
}
