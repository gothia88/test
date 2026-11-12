using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Administrator.WebDownloader)]
	public partial class WebDownloaderController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public WebDownloaderController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Download)]
		public virtual System.Web.Mvc.ViewResult Download()
		{
			string strPath = string.Empty;

			strPath =
				Server.MapPath("~/App_Data_Public");

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			strPath =
				string.Format("{0}\\Downloads", strPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);
			}

			ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel oViewModel =
				new ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel();

			return (View(oViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Download)]
		public virtual System.Web.Mvc.ActionResult Download
			(ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				if (string.IsNullOrWhiteSpace(viewModel.TargetFileName))
				{
					viewModel.TargetFileName =
						System.IO.Path.GetFileName(viewModel.SourceFileUrl);
				}

				string strPath = string.Empty;

				strPath =
					Server.MapPath("~/App_Data_Public");

				Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

				strPath =
					string.Format("{0}\\Downloads", strPath);

				if (System.IO.Directory.Exists(strPath) == false)
				{
					System.IO.Directory.CreateDirectory(strPath);
				}

				viewModel.TargetPathName =
					string.Format("{0}\\{1}", strPath, viewModel.TargetFileName);

				System.DateTime dtmStart = System.DateTime.Now;

				try
				{
					bool blnResult = false;

					if (viewModel.SourceFileUrl.StartsWith
						("FTP://", System.StringComparison.InvariantCultureIgnoreCase))
					{
						blnResult =
							SaveFtpRemoteFile(viewModel);
					}
					else
					{
						blnResult =
							SaveHttpRemoteFile(viewModel);
					}

					System.DateTime dtmEnd = System.DateTime.Now;

					viewModel.DownLoadAndSaveToFileDuration = dtmEnd - dtmStart;

					string strDownLoadAndSaveToFileDuration =
						viewModel.DownLoadAndSaveToFileDuration.Value.ToString(@"hh\:mm\:ss");

					string strInformationMessage =
						string.Format(Resources.Messages.Information006, strDownLoadAndSaveToFileDuration);

					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
				}
				catch (System.Exception ex)
				{
					System.DateTime dtmEnd = System.DateTime.Now;

					viewModel.DownLoadAndSaveToFileDuration = dtmEnd - dtmStart;

					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, ex.Message));
				}
			}

			return (View(viewModel));
		}

		private bool SaveHttpRemoteFile
			(ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel viewModel)
		{
			bool blnResult = false;

			System.IO.Stream oTargetStream = null;
			System.IO.Stream oSourceStream = null;
			System.Net.HttpWebRequest oHttpWebRequest = null;
			System.Net.HttpWebResponse oHttpWebResponse = null;

			try
			{
				oHttpWebRequest =
					(System.Net.HttpWebRequest)
					System.Net.WebRequest.Create(viewModel.SourceFileUrl);

				if (oHttpWebRequest == null)
				{
					throw (new System.Exception("Request Failed!"));
				}

				oHttpWebRequest.Timeout =
					viewModel.SourceTimeout * 1000;

				oHttpWebRequest.ReadWriteTimeout =
					viewModel.TargetTimeout * 1000;

				oHttpWebResponse =
					(System.Net.HttpWebResponse)oHttpWebRequest.GetResponse();

				if (oHttpWebResponse == null)
				{
					throw (new System.Exception("Response Timeout!"));
				}

				oSourceStream =
					oHttpWebResponse.GetResponseStream();

				oTargetStream =
					System.IO.File.Create(viewModel.TargetPathName);

				int intTemp = 0;
				int intBytesRead = 0;
				int intBytesProcessed = 0;

				int intPacketSize =
					viewModel.PocketSize * 1024;

				byte[] buffer =
					new byte[intPacketSize];

				do
				{
					intBytesRead =
						oSourceStream.Read(buffer, 0, buffer.Length);

					oTargetStream.Write(buffer, 0, intBytesRead);

					intTemp += intBytesRead;
					intBytesProcessed += intBytesRead;

					if (viewModel.Flush)
					{
						if (intTemp >= viewModel.FlushLength * 1024 * 1024)
						{
							intTemp = 0;
							oTargetStream.Flush();
						}
					}
				}
				while (intBytesRead > 0);

				blnResult = true;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (oHttpWebResponse != null)
				{
					oHttpWebResponse.Close();
					oHttpWebResponse = null;
				}

				if (oHttpWebRequest != null)
				{
					oHttpWebRequest = null;
				}

				if (oTargetStream != null)
				{
					oTargetStream.Dispose();
					oTargetStream = null;
				}

				if (oSourceStream != null)
				{
					oSourceStream.Dispose();
					oSourceStream = null;
				}
			}

			return (blnResult);
		}

		private bool SaveFtpRemoteFile
			(ViewModels.Areas.Administrator.WebDownloader.DownloadViewModel viewModel)
		{
			bool blnResult = false;

			System.IO.Stream oTargetStream = null;
			System.IO.Stream oSourceStream = null;
			System.Net.FtpWebRequest oFtpWebRequest = null;
			System.Net.FtpWebResponse oFtpWebResponse = null;

			try
			{
				oFtpWebRequest =
					(System.Net.FtpWebRequest)
					System.Net.WebRequest.Create(viewModel.SourceFileUrl);

				if (oFtpWebRequest == null)
				{
					throw (new System.Exception("Request Failed!"));
				}

				oFtpWebRequest.Timeout =
					viewModel.SourceTimeout * 1000;

				oFtpWebRequest.ReadWriteTimeout =
					viewModel.TargetTimeout * 1000;

				oFtpWebResponse =
					(System.Net.FtpWebResponse)oFtpWebRequest.GetResponse();

				if (oFtpWebResponse == null)
				{
					throw (new System.Exception("Response Timeout!"));
				}

				oSourceStream =
					oFtpWebResponse.GetResponseStream();

				oTargetStream =
					System.IO.File.Create(viewModel.TargetPathName);

				int intTemp = 0;
				int intBytesRead = 0;
				int intBytesProcessed = 0;

				int intPacketSize =
					viewModel.PocketSize * 1024;

				byte[] buffer =
					new byte[intPacketSize];

				do
				{
					intBytesRead =
						oSourceStream.Read(buffer, 0, buffer.Length);

					oTargetStream.Write(buffer, 0, intBytesRead);

					intTemp += intBytesRead;
					intBytesProcessed += intBytesRead;

					if (viewModel.Flush)
					{
						if (intTemp >= viewModel.FlushLength * 1024 * 1024)
						{
							oTargetStream.Flush();
						}
					}
				}
				while (intBytesRead > 0);

				blnResult = true;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (oFtpWebResponse != null)
				{
					oFtpWebResponse.Close();
					oFtpWebResponse = null;
				}

				if (oFtpWebRequest != null)
				{
					oFtpWebRequest = null;
				}

				if (oTargetStream != null)
				{
					oTargetStream.Dispose();
					oTargetStream = null;
				}

				if (oSourceStream != null)
				{
					oSourceStream.Dispose();
					oSourceStream = null;
				}
			}

			return (blnResult);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			resourceName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete
			(System.Web.Mvc.FormCollection formCollection)
		{
			string strRequest =
				formCollection["chkSelect"];

			if (string.IsNullOrWhiteSpace(strRequest) == false)
			{
				string strPath = string.Empty;

				strPath =
					Server.MapPath("~/App_Data_Public/Downloads");

				Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

				string[] strSelects = strRequest.Split(',');

				foreach (string strSelect in strSelects)
				{
					string strPathName =
						string.Format("{0}\\{1}", strPath, strSelect);

					if (System.IO.File.Exists(strPathName))
					{
						try
						{
							System.IO.File.Delete(strPathName);
						}
						catch { }
					}
				}
			}

			return (RedirectToAction(MVC.Administrator.WebDownloader.Download()));
		}
	}
}
