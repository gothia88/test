using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/06/05
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Cms.Layout)]
	public partial class LayoutController : Infrastructure.BaseControllerWithUnitOfWork
	{
		#region Nested Class(es)
		private class Paths
		{
			public Paths()
			{
			}

			public string LayoutPath { get; set; }
			public string ContentPath { get; set; }
		}
		#endregion Nested Class(es)

		public LayoutController()
		{
		}

		private string RootRelativeLayoutPath
		{
			get
			{
				return ("~/Views/Shared/_Layouts");
			}
		}

		private string RootRelativeContentPath
		{
			get
			{
				return ("~/Content/_Layouts");
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varLayouts =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varLayouts));
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

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oLayout));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.Cms.Layout oLayout =
				new Models.Cms.Layout();
			// **************************************************

			return (View(oLayout));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.Layout layout)
		{
			// **************************************************
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetByCultureLcidAndName(CultureLcid, layout.Name);

			if (oLayout != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				if (CreateLayout(layout) == null)
				{
					ModelState.AddModelError
						(string.Empty, Resources.Messages.UnexpectedError);

					return (View(layout));
				}

				// **************************************************
				layout.CultureLcid = CultureLcid;
				layout.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);

				if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer)
				{
					layout.IsSystem = false;
				}
				// **************************************************

				UnitOfWork.CmsUnitOfWork.LayoutRepository.Insert(layout);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Layout.Index()));
			}

			return (View(layout));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Copy)]
		public virtual System.Web.Mvc.ActionResult Copy(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oLayout));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Copy)]
		public virtual System.Web.Mvc.ActionResult Copy(Models.Cms.Layout layout)
		{
			// **************************************************
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetByCultureLcidAndName(CultureLcid, layout.Name);

			if (oLayout != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			Models.Cms.Layout oOriginalLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.GetById(layout.Id);

			if (oOriginalLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				if (CopyLayout(oOriginalLayout, layout) == false)
				{
					ModelState.AddModelError
						(string.Empty, Resources.Messages.UnexpectedError);

					return (View(layout));
				}

				oLayout = new Models.Cms.Layout();

				// **************************************************
				// **************************************************
				// **************************************************
				oLayout.Name = layout.Name;
				oLayout.Title = layout.Title;
				oLayout.Ordering = layout.Ordering;
				oLayout.IsSystem = layout.IsSystem;
				oLayout.Description = layout.Description;
				// **************************************************

				// **************************************************
				oLayout.CultureLcid = CultureLcid;

				oLayout.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				oLayout.SetIsActive(layout.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.LayoutRepository.Insert(oLayout);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Layout.Index()));
			}

			return (View(layout));
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

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oLayout));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.Layout layout)
		{
			// **************************************************
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetByCultureLcidAndNameExceptId(CultureLcid, layout.Name, layout.Id);

			if (oLayout != null)
			{
				// مقدار فيلد نام تکراری است! لطفا از مقدار ديگری استفاده نماييد
				string strErrorMessage =
					string.Format(Resources.Messages.DuplicateError,
					Resources.Models.General.Name);

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			Models.Cms.Layout oOriginalLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.GetById(layout.Id);

			if (oOriginalLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				bool blnEverythingIsOk = true;

				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					// اين دستور بايد اول نوشته شود
					blnEverythingIsOk =
						RenameLayout(oOriginalLayout, layout);

					oOriginalLayout.Name = layout.Name;
					oOriginalLayout.IsSystem = layout.IsSystem;
				}
				else
				{
					if (oOriginalLayout.IsSystem == false)
					{
						// اين دستور بايد اول نوشته شود
						blnEverythingIsOk = RenameLayout(oOriginalLayout, layout);

						oOriginalLayout.Name = layout.Name;
					}
				}
				// **************************************************

				if (blnEverythingIsOk == false)
				{
					ModelState.AddModelError(string.Empty, Resources.Messages.UnexpectedError);
				}
				else
				{
					// **************************************************
					// **************************************************
					// **************************************************
					oOriginalLayout.CultureLcid = CultureLcid;
					// **************************************************

					// **************************************************
					oOriginalLayout.Title = layout.Title;
					oOriginalLayout.Ordering = layout.Ordering;
					oOriginalLayout.Description = layout.Description;
					// **************************************************

					// **************************************************
					oOriginalLayout.SetUpdateDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
					oOriginalLayout.SetIsActive(layout.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id);
					// **************************************************
					// **************************************************
					// **************************************************

					UnitOfWork.CmsUnitOfWork.LayoutRepository.Update(oOriginalLayout);

					UnitOfWork.Save();

					return (RedirectToAction(MVC.Cms.Layout.Index()));
				}
			}

			return (View(layout));
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

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oLayout));
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
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			if (oLayout.IsSystem)
			{
				// ‌شما قادر به حذف قالب صفحه سيستمی نمی‌باشيد
				string strErrorMessage =
					Resources.Models.Cms.Layout.Error003;

				ModelState.AddModelError(string.Empty, strErrorMessage);

				return (View(oLayout));
			}
			// **************************************************

			// **************************************************
			// TODO: سر فرصت بهينه شود
			int intTotalPageCount =
				oLayout.Pages.Count + oLayout.ProjectActions.Count;

			if (intTotalPageCount != 0)
			{
				// تعدادی صفحه از اين قالب استفاده می‌کنند، لذا شما قادر به حذف اين قالب صفحه نمی‌باشيد
				string strErrorMessage =
					string.Format(Resources.Models.Cms.Layout.Error002, intTotalPageCount);

				ModelState.AddModelError(string.Empty, strErrorMessage);

				return (View(oLayout));
			}
			// **************************************************

			if (DeleteLayout(oLayout) == false)
			{
				ModelState.AddModelError
					(string.Empty, Resources.Messages.UnexpectedError);

				return (View(oLayout));
			}
			else
			{
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Delete(oLayout);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Layout.Index()));
			}
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private Paths CreateLayout(Models.Cms.Layout layout)
		{
			if (layout == null)
			{
				return (null);
			}

			string strPath = null;
			Paths oPaths = new Paths();

			// **************************************************
			strPath = CreateDirectory
				(RootRelativeLayoutPath, layout.Name);

			if (strPath == null)
			{
				return (null);
			}
			else
			{
				oPaths.LayoutPath = strPath;
			}
			// **************************************************

			// **************************************************
			strPath = CreateDirectory
				(RootRelativeContentPath, layout.Name);

			if (strPath == null)
			{
				return (null);
			}
			else
			{
				oPaths.ContentPath = strPath;
			}
			// **************************************************

			return (oPaths);
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private string CreateDirectory
			(string rootRelativeParentPath, string directoryName)
		{
			if (string.IsNullOrEmpty(directoryName) ||
				string.IsNullOrEmpty(rootRelativeParentPath))
			{
				return (null);
			}

			directoryName = directoryName.Trim();
			rootRelativeParentPath =
				rootRelativeParentPath.Trim();

			if ((directoryName == string.Empty) ||
				(rootRelativeParentPath == string.Empty))
			{
				return (null);
			}

			try
			{
				string strParentPath =
					Server.MapPath(rootRelativeParentPath);

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

				string strPath =
					string.Format("{0}\\{1}.{2}",
					strParentPath, directoryName, CultureName);

				if (System.IO.Directory.Exists(strPath) == false)
				{
					System.IO.Directory.CreateDirectory(strPath);
				}

				return (strPath);
			}
			catch
			{
				return (null);
			}
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (false) فقط در صورت بروز خطا
		/// </summary>
		private bool RenameLayout
			(Models.Cms.Layout oldLayout, Models.Cms.Layout newLayout)
		{
			if ((newLayout == null) ||
				(oldLayout == null) ||
				(string.IsNullOrWhiteSpace(newLayout.Name)) ||
				(string.IsNullOrWhiteSpace(oldLayout.Name)))
			{
				return (false);
			}

			// نيازی به تغيير نام نيست
			if (string.Compare(oldLayout.Name, newLayout.Name, ignoreCase: true) == 0)
			{
				return (true);
			}

			bool blnResult =
				DeleteLayout(newLayout);

			if (blnResult == false)
			{
				return (false);
			}

			// **************************************************
			blnResult = RenameDirectory
				(RootRelativeLayoutPath, oldLayout.Name, newLayout.Name);

			if (blnResult == false)
			{
				return (false);
			}
			// **************************************************

			// **************************************************
			blnResult = RenameDirectory
				(RootRelativeContentPath, oldLayout.Name, newLayout.Name);

			if (blnResult == false)
			{
				return (false);
			}
			// **************************************************

			return (true);
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private bool RenameDirectory
			(string rootRelativeParentPath,
			string sourceDirectoryName, string targetDirectoryName)
		{
			if (string.IsNullOrEmpty(sourceDirectoryName) ||
				string.IsNullOrEmpty(targetDirectoryName) ||
				string.IsNullOrEmpty(rootRelativeParentPath))
			{
				return (false);
			}

			sourceDirectoryName = sourceDirectoryName.Trim();
			targetDirectoryName = targetDirectoryName.Trim();
			rootRelativeParentPath =
				rootRelativeParentPath.Trim();

			if ((sourceDirectoryName == string.Empty) ||
				(targetDirectoryName == string.Empty) ||
				(rootRelativeParentPath == string.Empty))
			{
				return (false);
			}

			// نيازی به تغيير نام نيست
			if (string.Compare(sourceDirectoryName, targetDirectoryName, ignoreCase: true) == 0)
			{
				return (true);
			}

			try
			{
				string strParentPath =
					Server.MapPath(rootRelativeParentPath);

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

				string strSourcePath =
					string.Format("{0}\\{1}.{2}",
					strParentPath, sourceDirectoryName, CultureName);

				string strTargetPath =
					string.Format("{0}\\{1}.{2}",
					strParentPath, targetDirectoryName, CultureName);

				// مسير سورس برای تغيير نام وجود ندارد
				if (System.IO.Directory.Exists(strSourcePath) == false)
				{
					return (true);
				}

				System.IO.Directory.Move(strSourcePath, strTargetPath);

				return (true);
			}
			catch
			{
				return (false);
			}
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private bool CopyLayout
			(Models.Cms.Layout sourceLayout, Models.Cms.Layout targetLayout)
		{
			if ((sourceLayout == null) ||
				(targetLayout == null) ||
				(string.IsNullOrWhiteSpace(sourceLayout.Name)) ||
				(string.IsNullOrWhiteSpace(targetLayout.Name)))
			{
				return (false);
			}

			// نيازی به کپی نمی باشد
			if (string.Compare(sourceLayout.Name, targetLayout.Name, ignoreCase: true) == 0)
			{
				return (true);
			}

			bool blnResult =
				DeleteLayout(targetLayout);

			if (blnResult == false)
			{
				return (false);
			}

			// **************************************************
			blnResult = CopyDirectory
				(RootRelativeLayoutPath, sourceLayout.Name, targetLayout.Name);

			if (blnResult == false)
			{
				return (false);
			}
			// **************************************************

			// **************************************************
			blnResult =
				CopyDirectory
				(RootRelativeContentPath, sourceLayout.Name, targetLayout.Name);

			if (blnResult == false)
			{
				return (false);
			}
			// **************************************************

			return (true);
		}

		/// <summary>
		/// چک شود
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private bool CopyDirectory
			(string rootRelativeParentPath,
			string sourceDirectoryName, string targetDirectoryName)
		{
			if (string.IsNullOrEmpty(sourceDirectoryName) ||
				string.IsNullOrEmpty(targetDirectoryName) ||
				string.IsNullOrEmpty(rootRelativeParentPath))
			{
				return (false);
			}

			sourceDirectoryName = sourceDirectoryName.Trim();
			targetDirectoryName = targetDirectoryName.Trim();
			rootRelativeParentPath =
				rootRelativeParentPath.Trim();

			if ((sourceDirectoryName == string.Empty) ||
				(targetDirectoryName == string.Empty) ||
				(rootRelativeParentPath == string.Empty))
			{
				return (false);
			}

			try
			{
				string strParentPath =
					Server.MapPath(rootRelativeParentPath);

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

				string strSourcePath =
					string.Format("{0}\\{1}.{2}",
					strParentPath, sourceDirectoryName, CultureName);

				string strTargetPath =
					string.Format("{0}\\{1}.{2}",
					strParentPath, targetDirectoryName, CultureName);

				// سورس برای کپی وجود ندارد
				// ولی بايد بررسی شود
				if (System.IO.Directory.Exists(strSourcePath) == false)
				{
					return (true);
				}

				Dtx.IO.Directory.Copy
					(strSourcePath, strTargetPath, copySubDirectories: true);

				return (true);
			}
			catch
			{
				return (false);
			}
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private bool DeleteLayout(Models.Cms.Layout layout)
		{
			if (layout == null)
			{
				return (false);
			}

			bool blnResult = false;

			// **************************************************
			blnResult = DeleteDirectory
				(RootRelativeLayoutPath, layout.Name);

			if (blnResult == false)
			{
				return (false);
			}
			// **************************************************

			// **************************************************
			blnResult = DeleteDirectory
				(RootRelativeContentPath, layout.Name);

			if (blnResult == false)
			{
				return (false);
			}
			// **************************************************

			return (true);
		}

		/// <summary>
		/// چک شد
		/// بر می گردانيم (null) فقط در صورت بروز خطا
		/// </summary>
		private bool DeleteDirectory
			(string rootRelativeParentPath, string directoryName)
		{
			if ((string.IsNullOrEmpty(directoryName)) ||
				(string.IsNullOrEmpty(rootRelativeParentPath)))
			{
				return (false);
			}

			directoryName = directoryName.Trim();
			rootRelativeParentPath =
				rootRelativeParentPath.Trim();

			if ((directoryName == string.Empty) ||
				(rootRelativeParentPath == string.Empty))
			{
				return (false);
			}

			try
			{
				string strParentPath =
					Server.MapPath(rootRelativeParentPath);

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

				string strPath =
					string.Format("{0}\\{1}.{2}",
					strParentPath, directoryName, CultureName);

				if (System.IO.Directory.Exists(strPath))
				{
					System.IO.Directory.Delete(strPath, recursive: true);
				}

				return (true);
			}
			catch
			{
				return (false);
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.EditLayoutContent)]
		public virtual System.Web.Mvc.ActionResult EditLayoutContent(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			Paths oPaths = CreateLayout(oLayout);

			if (oPaths == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.MethodNotAllowed)));
			}

			// Note: (_) for the layout name is necessary!
			string strPathName =
				string.Format("{0}\\_Site.cshtml", oPaths.LayoutPath);

			string strContent = Dtx.IO.File.Read(strPathName);
			// **************************************************

			// **************************************************
			ViewModels.Areas.Cms.Layout.EditLayoutContentViewModel oEditLayoutContentViewModel =
				new ViewModels.Areas.Cms.Layout.EditLayoutContentViewModel();

			oEditLayoutContentViewModel.Layout = oLayout;
			oEditLayoutContentViewModel.Content = strContent;
			// **************************************************

			return (View(oEditLayoutContentViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.EditLayoutContent)]
		public virtual System.Web.Mvc.ActionResult EditLayoutContent
			(ViewModels.Areas.Cms.Layout.EditLayoutContentViewModel editLayoutContentViewModel)
		{
			// **************************************************
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository
				.GetById(editLayoutContentViewModel.Layout.Id);

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			Paths oPaths = CreateLayout(oLayout);

			if (oPaths == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.MethodNotAllowed)));
			}

			// Note: (_) for the layout name is necessary!
			string strPathName =
				string.Format("{0}\\_Site.cshtml", oPaths.LayoutPath);

			Dtx.IO.File.Overwrite
				(strPathName, editLayoutContentViewModel.Content);
			// **************************************************

			oLayout.SetUpdateDateTime
				(Infrastructure.Sessions.AuthenticatedUser.Id);

			UnitOfWork.CmsUnitOfWork.LayoutRepository.Update(oLayout);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.Layout.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectProjectActionsForLayout)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Cms/Layout'>{0}</a>",
				Resources.Controllers.Cms_Layout);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oLayout.Id,
					name: oLayout.FullName,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Resources.Models.Cms.Layout.EntityName,
					selectedProjectActions: oLayout.ProjectActions);

			o_Partial_SelectProjectActionsViewModel.DisplayAllActions = true;

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectProjectActionsForLayout)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var varProjectActions =
				UnitOfWork.ProjectActionRepository.Get()
				.ToList()
				;

			oLayout.ProjectActions.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					Models.ProjectAction oProjectAction =
						varProjectActions
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (oProjectAction != null)
					{
						oLayout.ProjectActions.Add(oProjectAction);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Cms/Layout'>{0}</a>",
				Resources.Controllers.Cms_Layout);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oLayout.Id,
					name: oLayout.FullName,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Resources.Models.Cms.Layout.EntityName,
					selectedProjectActions: oLayout.ProjectActions);

			o_Partial_SelectProjectActionsViewModel.DisplayAllActions = true;

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectPagesForLayout)]
		public virtual System.Web.Mvc.ActionResult SelectPages(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()

				.Where(current => current.CultureLcid == CultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)

				.ToList()
				;

			return (View(oLayout));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.SelectPagesForLayout)]
		public virtual System.Web.Mvc.ActionResult SelectPages(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var varPages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.ToList()
				;

			oLayout.Pages.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					Models.Cms.Page oPage =
						varPages
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (oPage != null)
					{
						oLayout.Pages.Add(oPage);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()

				.Where(current => current.CultureLcid == CultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)

				.ToList()
				;

			return (View(oLayout));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.ManageFoldersAndFiles)]
		public virtual System.Web.Mvc.ActionResult ManageFoldersAndFiles(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Layout oLayout =
				UnitOfWork.CmsUnitOfWork.LayoutRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oLayout == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			string strRootRelativePath =
				string.Format("{0}/{1}.{2}",
				RootRelativeContentPath, oLayout.Name, CultureName);

			return (RedirectToAction(MVC.Administrator.FileManager.List(strRootRelativePath)));
		}
	}
}
