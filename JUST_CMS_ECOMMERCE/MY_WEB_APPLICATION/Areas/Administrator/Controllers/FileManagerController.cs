using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/10/15
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: true,
		resourceName: Resources.Strings.ControllersKeys.Administrator.FileManager)]
	public partial class FileManagerController : Infrastructure.BaseController
	{
		public FileManagerController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Dtx.Resources.FileManager),
			resourceName: Dtx.Resources.Strings.FileManagerKeys.List)]
		public virtual System.Web.Mvc.ActionResult List(string rootRelativePath = "~")
		{
			// بسيار اهميت دارد model: توجه: نوشتن عبارت
			// در نظر می‌گيرد View در غير اين صورت آنرا به عنوان نام
			return (View(model: rootRelativePath));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Dtx.Resources.FileManager),
			resourceName: Dtx.Resources.Strings.FileManagerKeys.Delete)]
		public virtual System.Web.Mvc.JsonResult
			Delete(Dtx.FileManager.ViewModels.DeleteViewModel model)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			Infrastructure.JsonData oJsonData = null;

			if (string.IsNullOrWhiteSpace(model.RootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error002;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			if ((model.CheckBoxes == null) ||
				(model.CheckBoxes.Length == 0))
			{
				// فايل و/يا پوشه‌ای برای حذف انتخاب نشده است
				string strErrorMessage =
					Dtx.Resources.FileManager.Error001;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			string strPath =
				Server.MapPath(model.RootRelativePath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error002;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			foreach (string strFileName in model.CheckBoxes)
			{
				string strPathOrPathName =
					string.Format("{0}\\{1}", strPath, strFileName);

				if (System.IO.Directory.Exists(strPathOrPathName))
				{
					try
					{
						System.IO.Directory.Delete
							(strPathOrPathName, recursive: true);
					}
					catch { }
				}
				else
				{
					if (System.IO.File.Exists(strPathOrPathName))
					{
						try
						{
							System.IO.File.Delete(strPathOrPathName);
						}
						catch { }
					}
				}
			}

			// فايل(ها) و/يا پوشه(ها) با موفقيت حذف شد(ند)
			string strInformationMessage =
				Dtx.Resources.FileManager.Information001;

			oJsonData =
				new Infrastructure.JsonData()
				{
					MessageText = strInformationMessage,
					State = Infrastructure.Enums.JsonResultStates.Success,
				};

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Dtx.Resources.FileManager),
			resourceName: Dtx.Resources.Strings.FileManagerKeys.Compress)]
		public virtual System.Web.Mvc.JsonResult
			Compress(Dtx.FileManager.ViewModels.DeleteViewModel model)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			Infrastructure.JsonData oJsonData = null;

			if (string.IsNullOrWhiteSpace(model.RootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error002;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			if ((model.CheckBoxes == null) ||
				(model.CheckBoxes.Length == 0))
			{
				// فايل و/يا پوشه‌ای برای حذف انتخاب نشده است
				string strErrorMessage =
					Dtx.Resources.FileManager.Error001;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			string strPath =
				Server.MapPath(model.RootRelativePath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error002;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			Ionic.Zip.ZipFile oZipFile = null;

			string strZipFileName =
				string.Format("Backup_{0}.zip",
				System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));

			string strZipPathName =
				string.Format("{0}\\{1}", strPath, strZipFileName);

			try
			{
				oZipFile =
					new Ionic.Zip.ZipFile(strZipPathName);

				oZipFile.AlternateEncoding = oZipFile.AlternateEncoding;
				oZipFile.AlternateEncodingUsage = Ionic.Zip.ZipOption.Default; // Default: [Default]

				oZipFile.Strategy = Ionic.Zlib.CompressionStrategy.Default; // Default: [Default]
				oZipFile.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Default; // Default: [Default]
				oZipFile.CompressionMethod = Ionic.Zip.CompressionMethod.Deflate; // Default: [Deflate]
				oZipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // Default: [Default]

				oZipFile.CodecBufferSize = 0; // Default: [0]
				oZipFile.BufferSize = 32768; // Default: [32768]
				oZipFile.MaxOutputSegmentSize = 0; // Default: [0]
				oZipFile.ParallelDeflateMaxBufferPairs = 16; // Default: [16]
				oZipFile.ParallelDeflateThreshold = 524288; // Default: [524288]

				oZipFile.Comment = null; // Default: [null]
				oZipFile.FullScan = false; // Default: [false]
				oZipFile.TempFileFolder = null; // Default: [null]
				oZipFile.CaseSensitiveRetrieval = false; // Default: [false]
				oZipFile.FlattenFoldersOnExtract = false; // Default: [false]
				oZipFile.SortEntriesBeforeSaving = false; // Default: [false]

				oZipFile.EmitTimesInUnixFormatWhenSaving = false; // Default: [false]
				oZipFile.EmitTimesInWindowsFormatWhenSaving = true; // Default: [true]

				oZipFile.Password = null; // Default: [null]
				oZipFile.Encryption = Ionic.Zip.EncryptionAlgorithm.None; // Default: [None]

				oZipFile.ZipErrorAction = Ionic.Zip.ZipErrorAction.Skip; // Default: [Throw]
				oZipFile.ExtractExistingFile = Ionic.Zip.ExtractExistingFileAction.OverwriteSilently; // Default: [Throw]

				foreach (string strFileName in model.CheckBoxes)
				{
					string strPathOrPathName =
						string.Format("{0}\\{1}", strPath, strFileName);

					if (System.IO.Directory.Exists(strPathOrPathName))
					{
						try
						{
							oZipFile.AddDirectory
								(directoryName: strPathOrPathName,
								directoryPathInArchive: string.Empty);
							oZipFile.Save();
						}
						catch { }
					}
					else
					{
						if (System.IO.File.Exists(strPathOrPathName))
						{
							try
							{
								oZipFile.AddFile
									(fileName: strPathOrPathName,
									directoryPathInArchive: string.Empty);
								oZipFile.Save();
							}
							catch { }
						}
					}
				}

				System.IO.FileInfo oFileInfo =
					new System.IO.FileInfo(strZipPathName);

				Dtx.FileManager.ViewModels.DisplayFileRowViewModel oDisplayFileRowViewModel =
					new Dtx.FileManager.ViewModels.DisplayFileRowViewModel(model.RootRelativePath, oFileInfo);

				string strView =
					Infrastructure.Utility.RenderPartialViewToString
						(context: ControllerContext,
						model: oDisplayFileRowViewModel,
						viewName: MVC.Administrator.FileManager.Views._Partial_DisplayFileRow);

				// فايل(ها) و/يا پوشه(ها) با موفقيت در داخل فايل [{0}] فشرده شد(ند)
				string strInformationMessage =
					string.Format(Dtx.Resources.FileManager.Information002,
					strZipFileName);

				oJsonData =
					new Infrastructure.JsonData()
					{
						//View = strView,
						MessageText = strInformationMessage,
						State = Infrastructure.Enums.JsonResultStates.Success,
					};

				return (oJsonData.GetJsonResult());
			}
			catch { }
			finally
			{
				if (oZipFile != null)
				{
					oZipFile.Dispose();
					oZipFile = null;
				}
			}

			return (null);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Dtx.Resources.FileManager),
			resourceName: Dtx.Resources.Strings.FileManagerKeys.CreateFolder)]
		public virtual System.Web.Mvc.JsonResult
			CreateFolder(string rootRelativePath, string folderName)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			Infrastructure.JsonData oJsonData = null;

			if (string.IsNullOrWhiteSpace(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error002;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			if (string.IsNullOrWhiteSpace(folderName))
			{
				// نام پوشه را وارد نکرده‌ايد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error005;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			string strRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, folderName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			if ((System.IO.File.Exists(strPathName)) ||
				(System.IO.Directory.Exists(strPathName)))
			{
				// فايل يا پوشه‌ای با اين نام قبلا ايجاد شده است! لطفا از نام ديگری استفاده نماييد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error006;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			try
			{
				System.IO.DirectoryInfo oDirectoryInfo =
					System.IO.Directory.CreateDirectory(strPathName);

				Dtx.FileManager.ViewModels.DisplayDirectoryRowViewModel oDisplayDirectoryRowViewModel =
					new Dtx.FileManager.ViewModels.DisplayDirectoryRowViewModel(rootRelativePath, oDirectoryInfo);

				string strView =
					Infrastructure.Utility.RenderPartialViewToString
						(context: ControllerContext,
						model: oDisplayDirectoryRowViewModel,
						viewName: MVC.Administrator.FileManager.Views._Partial_DisplayDirectoryRow);

				// پوشه با موفقيت ايجاد گرديد
				string strInformationMessage =
					Dtx.Resources.FileManager.Information003;

				oJsonData =
					new Infrastructure.JsonData()
					{
						//View = strView,
						MessageText = strInformationMessage,
						State = Infrastructure.Enums.JsonResultStates.Success,
					};

				return (oJsonData.GetJsonResult());
			}
			catch
			{
				// ايجاد پوشه با مشکل مواجه شده است
				string strErrorMessage =
					Dtx.Resources.FileManager.Error009;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceType: typeof(Dtx.Resources.FileManager),
			resourceName: Dtx.Resources.Strings.FileManagerKeys.Rename)]
		public virtual System.Web.Mvc.JsonResult Rename
			(string rootRelativePath, string oldName, string newName)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			Infrastructure.JsonData oJsonData = null;

			if (string.IsNullOrWhiteSpace(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error002;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			if (string.IsNullOrWhiteSpace(oldName))
			{
				// نام فايل يا پوشه‌ای برای تغيير نام دادن، مشخص نشده است
				string strErrorMessage =
					Dtx.Resources.FileManager.Error003;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			if (string.IsNullOrWhiteSpace(newName))
			{
				// شما نام فايل يا پوشه جديد را وارد نکرده‌ايد
				string strErrorMessage =
					Dtx.Resources.FileManager.Error004;

				oJsonData =
					new Infrastructure.JsonData()
					{
						MessageText = strErrorMessage,
						State = Infrastructure.Enums.JsonResultStates.Error,
					};

				return (oJsonData.GetJsonResult());
			}

			string strOldRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, oldName);

			string strOldPathName =
				Server.MapPath(strOldRootRelativePathName);

			if (System.IO.File.Exists(strOldPathName))
			{
				string strNewRootRelativePathName =
					string.Format("{0}/{1}", rootRelativePath, newName);

				string strNewPathName =
					Server.MapPath(strNewRootRelativePathName);

				try
				{
					System.IO.File.Move
						(strOldPathName, strNewPathName);

					System.IO.FileInfo oFileInfo =
						new System.IO.FileInfo(strNewPathName);

					Dtx.FileManager.ViewModels.DisplayFileRowViewModel oDisplayFileRowViewModel =
						new Dtx.FileManager.ViewModels.DisplayFileRowViewModel(rootRelativePath, oFileInfo);

					string strView =
						Infrastructure.Utility.RenderPartialViewToString
							(context: ControllerContext,
							model: oDisplayFileRowViewModel,
							viewName: MVC.Administrator.FileManager.Views._Partial_DisplayFileRow);

					// نام فايل با موفقيت تغيير کرد
					string strErrorMessage =
						Dtx.Resources.FileManager.Information004;

					oJsonData =
						new Infrastructure.JsonData()
						{
							//View = strView,
							MessageText = strErrorMessage,
							State = Infrastructure.Enums.JsonResultStates.Success,
						};

					return (oJsonData.GetJsonResult());
				}
				catch
				{
					// در زمان تغيير نام فايل، خطای ناشناخته‌ای صورت گرفته است
					string strErrorMessage =
						Dtx.Resources.FileManager.Error011;

					oJsonData =
						new Infrastructure.JsonData()
						{
							MessageText = strErrorMessage,
							State = Infrastructure.Enums.JsonResultStates.Error,
						};

					return (oJsonData.GetJsonResult());
				}
			}
			else
			{
				if (System.IO.Directory.Exists(strOldPathName))
				{
					string strNewRootRelativePathName =
						string.Format("{0}/{1}", rootRelativePath, newName);

					string strNewPathName =
						Server.MapPath(strNewRootRelativePathName);

					try
					{
						System.IO.Directory.Move
							(strOldPathName, strNewPathName);

						System.IO.DirectoryInfo oDirectoryInfo =
							System.IO.Directory.CreateDirectory(strNewPathName);

						Dtx.FileManager.ViewModels.DisplayDirectoryRowViewModel oDisplayDirectoryRowViewModel =
							new Dtx.FileManager.ViewModels.DisplayDirectoryRowViewModel(rootRelativePath, oDirectoryInfo);

						string strView =
							Infrastructure.Utility.RenderPartialViewToString
								(context: ControllerContext,
								model: oDisplayDirectoryRowViewModel,
								viewName: MVC.Administrator.FileManager.Views._Partial_DisplayDirectoryRow);

						// نام پوشه با موفقيت تغيير کرد
						string strErrorMessage =
							Dtx.Resources.FileManager.Information005;

						oJsonData =
							new Infrastructure.JsonData()
							{
								//View = strView,
								MessageText = strErrorMessage,
								State = Infrastructure.Enums.JsonResultStates.Success,
							};

						return (oJsonData.GetJsonResult());
					}
					catch
					{
						// در زمان تغيير نام پوشه، خطای ناشناخته‌ای صورت گرفته است
						string strErrorMessage =
							Dtx.Resources.FileManager.Error012;

						oJsonData =
							new Infrastructure.JsonData()
							{
								MessageText = strErrorMessage,
								State = Infrastructure.Enums.JsonResultStates.Error,
							};

						return (oJsonData.GetJsonResult());
					}
				}
				else
				{
					// فايل يا پوشه‌ای با نام {0} برای تغيير نام وجود ندارد
					string strErrorMessage =
						string.Format(Dtx.Resources.FileManager.Error010,
						oldName);

					oJsonData =
						new Infrastructure.JsonData()
						{
							MessageText = strErrorMessage,
							State = Infrastructure.Enums.JsonResultStates.Error,
						};

					return (oJsonData.GetJsonResult());

				}
			}
		}










		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Download)]
		public virtual System.Web.Mvc.FileResult Download(string rootRelativePathName)
		{
			string strPathName =
				Server.MapPath(rootRelativePathName);

			string strFileName =
				System.IO.Path.GetFileName(strPathName);

			return (File(strPathName,
				contentType: "googooli/magooli", fileDownloadName: strFileName));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.UploadFile)]
		public virtual System.Web.Mvc.JsonResult UploadFile(string rootRelativePath)
		{
			Infrastructure.Utility.IsApplicationInDemoVersion();

			if (string.IsNullOrWhiteSpace(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strMessage =
					Dtx.Resources.FileManager.Error002;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			// **************************************************
			System.Web.HttpPostedFileBase oPostedFile = null;

			if (Request.Files.Count == 0)
			{
				// فايلی برای آپلود مشخص نشده است!
				string strMessage =
					Dtx.Resources.FileManager.Error007;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			oPostedFile = Request.Files[0];
			// **************************************************

			string strFileName =
				System.IO.Path.GetFileName(oPostedFile.FileName);

			string strParentPath =
				Server.MapPath(rootRelativePath);

			Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

			string strRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			oPostedFile.SaveAs(strPathName);

			try
			{
				System.IO.FileInfo oFileInfo =
					new System.IO.FileInfo(strPathName);

				string strData = string.Empty;

				strData += "<tr>";
				strData += "	<td>";
				strData += "		<a href='#' id='xxxxx' rename='directory'><span class='glyphicon glyphicon-pencil'></span></a>";
				strData += "	</td>";
				strData += "	<td>";
				strData += "		<input type='checkbox' name='checkBoxes' value='xxxxx' />";
				strData += "	</td>";
				strData += "	<td>";
				strData += "		<span class='glyphicon glyphicon-folder-close'></span>";
				strData += "	</td>";
				strData += "	<td style='direction: ltr'>";
				strData += "		<a href='/Administrator/FileManager/List?rootRelativePath=~/Views/Shared/_Layouts/default.fa-IR/xxxxx'>xxxxx</a>";
				strData += "			</td>";
				strData += "			<td></td>";
				strData += "			<td>";
				strData += "				" +
					Dtx.Calendar.Utility.DisplayDateTime(oFileInfo.CreationTime, true);
				strData += "			</td>";
				strData += "			<td>";
				strData += "				" +
					Dtx.Calendar.Utility.DisplayDateTime(oFileInfo.LastWriteTime, true);
				strData += "			</td>";
				strData += "		</tr>";

				strData = strData.Replace("xxxxx", strFileName);

				Result oResult =
					new Result(Result.Types.OK, string.Empty, strData);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}
			catch (System.Exception ex)
			{
				Result oResult =
					new Result(Result.Types.ERROR, ex.Message);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}
		}
	}
}

public class Result : System.Object
{
	public enum Types
	{
		OK,
		ERROR
	}

	//public Result()
	//{
	//}

	public Result(Types type, string message)
	{
		Type = type;
		Message = message;
	}

	public Result(Types type, string message, string data)
		: this(type, message)
	{
		Data = data;
	}

	public Types Type { get; set; }

	public string Data { get; set; }
	public string Message { get; set; }
}
