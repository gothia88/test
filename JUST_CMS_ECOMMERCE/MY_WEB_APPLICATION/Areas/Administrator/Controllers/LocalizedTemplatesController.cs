using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/07/09
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.LocalizedTemplates)]
	public partial class LocalizedTemplatesController : Infrastructure.BaseController
	{
		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.PartialViewResult
			Edit(string directoryName, string fileName)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			string strRootRelativePath =
				string.Format("~/App_Data/LocalizedTemplates/{0}", directoryName);

			string strRootRelativePathName =
				string.Format("{0}/{1}", strRootRelativePath, fileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			string strContent =
				Dtx.IO.File.Read(strPathName);

			ViewModels.Areas.Administrator.LocalizedTemplates.EditViewModel oViewModel =
				new ViewModels.Areas.Administrator.LocalizedTemplates.EditViewModel();

			oViewModel.FileName = fileName;
			oViewModel.Content = strContent;
			oViewModel.DirectoryName = directoryName;

			return (PartialView(oViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Save)]
		public virtual System.Web.Mvc.JsonResult Save
			(ViewModels.Areas.Administrator.LocalizedTemplates.EditViewModel viewModel)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			string strRootRelativePath =
				string.Format("~/App_Data/LocalizedTemplates/{0}",
				viewModel.DirectoryName);

			string strPath =
				Server.MapPath(strRootRelativePath);

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			string strPathName =
				string.Format("{0}\\{1}", strPath, viewModel.FileName);

			Dtx.IO.File.Overwrite(strPathName, viewModel.Content);

			return (null);
		}
	}
}
