namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1393/12/17
	/// Changed In Version: 1.8.2
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class DownloadAndUploadManager
	{
		#region Enumeration(s)
		public enum ActionResultStates : int
		{
			Error = 0,
			Success = 1,
			Warning = 2,
		}
		#endregion /Enumeration(s)

		#region Class(es)
		public class ActionResult : System.Object
		{
			public ActionResult()
			{
			}

			public string Path { get; set; }

			public string Message { get; set; }

			public string PathName { get; set; }

			public string FileName { get; set; }

			public string FileExtension { get; set; }

			public ActionResultStates State { get; set; }
		}
		#endregion /Class(es)

		static DownloadAndUploadManager()
		{
		}

		public static ActionResult DeleteFile(string pathName)
		{
			ActionResult oActionResult = new ActionResult();

			// **************************************************
			if (string.IsNullOrWhiteSpace(pathName))
			{
				string strErrorMessage =
					"مسير و نام فايل، در جهت حذف، مشخص نشده است!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			if (System.IO.Directory.Exists(pathName))
			{
				string strErrorMessage =
					"به جای فايل، پوشه‌ای به اين نام وجود دارد!" + " " +
					"لذا سيستم قادر به حذف آن نمی‌باشد!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			if (System.IO.File.Exists(pathName) == false)
			{
				string strWarningMessage =
					"چنين فايلی، در جهت حذف، وجود ندارد!";

				oActionResult.State =
					ActionResultStates.Warning;

				oActionResult.Message = strWarningMessage;

				return (oActionResult);
			}
			// **************************************************

			try
			{
				System.IO.File.Delete(pathName);

				// **************************************************
				string strInformationMessage =
					"فايل با موفقيت حذف گرديد.";

				oActionResult.State =
					ActionResultStates.Success;

				oActionResult.Message = strInformationMessage;

				return (oActionResult);
				// **************************************************
			}
			catch
			{
				// **************************************************
				string strErrorMessage =
					"سيستم قادر به حذف اين فايل نيست!" + " " +
					"احتمالا اين فايل، توسط شخص ديگری، در حال استفاده می‌باشد.";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
				// **************************************************
			}
		}

		/// <param name="path">مسيری که بايد فايل ارسال شده، ذخيره شود</param>
		/// <param name="fileNameWithoutExtension">نام فايلی که بايد ذخيره شود. در صورتی که نام فايل مشخص نشود، نام فايل ارسالی در نظر گرفته خواهد شد</param>
		/// <param name="maxFileLengthInKB">حداکثر حجم فايل ارسالی. در صورتی که حداکثر حجم فايل ارسالی مشخص نشود، محدوديتی در حجم ارسالی نخواهد بود</param>
		public static ActionResult UploadFile
			(string path, string fileNameWithoutExtension = null, long maxFileLengthInKB = 0)
		{
			ActionResult oActionResult = new ActionResult();

			// **************************************************
			if (path == null)
			{
				string strErrorMessage =
					"مسير مقصد مشخص نشده است!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}

			path = path.Trim();

			if (path == string.Empty)
			{
				string strErrorMessage =
					"مسير مقصد مشخص نشده است!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}

			while (path.Contains("\\\\"))
			{
				path =
					path.Replace("\\\\", "\\");
			}
			// **************************************************

			// **************************************************
			if (maxFileLengthInKB < 0)
			{
				maxFileLengthInKB = 0;
			}
			// **************************************************

			System.Web.HttpRequest oRequest =
				System.Web.HttpContext.Current.Request;

			// **************************************************
			if (oRequest == null)
			{
				string strErrorMessage =
					"خطای ناشناخته‌ای صورت گرفته است!" + " " +
					"لطفا مجددا اقدام به ارسال فايل نموده و يا با مسوولين پايگاه تماس حاصل فرماييد.";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			if ((oRequest.Files == null) ||
				(oRequest.Files.Count == 0) ||
				(string.IsNullOrWhiteSpace(oRequest.Files[0].FileName)))
			{
				string strErrorMessage =
					"فايلی برای ارسال انتخاب نکرده‌ايد!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			System.Web.HttpPostedFile oPostedFile = oRequest.Files[0];

			// **************************************************
			if (oPostedFile.ContentLength == 0)
			{
				string strErrorMessage =
					"فايل به درستی ارسال نشده است! لطفا مجددا اقدام نماييد.";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			// اگر برابر صفر باشد، يعنی محدوديتی در سايز فايل در زمان آپلود وجود ندارد
			if (maxFileLengthInKB > 0)
			{
				if (oPostedFile.ContentLength > maxFileLengthInKB * 1024)
				{
					string strErrorMessage =
						string.Format("حجم فايل ارسالی نمی‌تواند از [{0:#,##0}] کيلوبايت بيشتر باشد!",
						maxFileLengthInKB);

					oActionResult.State =
						ActionResultStates.Error;

					oActionResult.Message = strErrorMessage;

					return (oActionResult);
				}
			}
			// **************************************************

			string strFileExtension =
				System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

			// **************************************************
			if (strFileExtension.Length > 5)
			{
				string strErrorMessage =
					"پسوند فايل ارسالی نمی‌تواند از 4 حرف بيشتر باشد!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			if (string.Compare(strFileExtension, ".exe", ignoreCase: true) == 0)
			{
				string strErrorMessage =
					"شما اجازه ارسال فايل‌های اجرايی به پايگاه را نداريد!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			string strContentType =
				oPostedFile.ContentType.ToLower();

			// **************************************************
			if ((string.Compare(strContentType, "application/exe", ignoreCase: true) == 0) ||
				(string.Compare(strContentType, "application/x-msdownload", ignoreCase: true) == 0))
			{
				string strErrorMessage =
					"شما اجازه ارسال فايل‌های اجرايی به پايگاه را نداريد!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			string strFileName =
				oPostedFile.FileName.ToLower();

			if (string.IsNullOrWhiteSpace(fileNameWithoutExtension) == false)
			{
				strFileName =
					string.Format("{0}{1}",
					fileNameWithoutExtension, strFileExtension);
			}
			// **************************************************

			Dtx.IO.Directory.SetFullControlForEveryOne(path);

			// **************************************************
			string strPathName = string.Empty;

			if (path.EndsWith(@"\"))
			{
				strPathName =
					string.Format("{0}{1}", path, strFileName);
			}
			else
			{
				strPathName =
					string.Format(@"{0}\{1}", path, strFileName);
			}
			// **************************************************

			// **************************************************
			if (System.IO.Directory.Exists(strPathName))
			{
				string strErrorMessage =
					"پوشه‌ای دقيقا هم نام فايل، در مقصد وجود ندارد!";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			// **************************************************
			oActionResult = DeleteFile(strPathName);

			if (oActionResult.State == ActionResultStates.Error)
			{
				string strErrorMessage =
					"فايلی با همين نام در مقصد وجود دارد" + " " +
					"لذا سيستم قادر به حذف آن، برای جايگزينی، نمی‌باشد!";

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
			}
			// **************************************************

			try
			{
				// **************************************************
				oPostedFile.SaveAs(strPathName);

				string strInformationMessage =
					string.Format("فايل با موفقيت به پايگاه ارسال و با نام [{0}] ذخيره گرديد.",
					strFileName);

				oActionResult.State =
					ActionResultStates.Success;

				oActionResult.Path = path;
				oActionResult.FileName = strFileName;
				oActionResult.PathName = strPathName;
				oActionResult.Message = strInformationMessage;
				oActionResult.FileExtension = strFileExtension;

				return (oActionResult);
				// **************************************************
			}
			catch
			{
				// **************************************************
				string strErrorMessage =
					"در لحظه ذخيره فايل ارسالی در پايگاه، خطای ناشناخته‌ای صورت گرفته است!" + " " +
					"لطفا مجددا اقدام به ارسال فايل نموده و يا با مسوولين پايگاه تماس حاصل فرماييد.";

				oActionResult.State =
					ActionResultStates.Error;

				oActionResult.Message = strErrorMessage;

				return (oActionResult);
				// **************************************************
			}
		}

		public static bool DownloadFile
			(string path, string fileName, int maxDownloadSpeedInKB = 0)
		{
			// **************************************************
			if (path == null)
			{
				return (false);
			}

			path = path.Trim();

			if (path == string.Empty)
			{
				return (false);
			}
			// **************************************************

			// **************************************************
			if (fileName == null)
			{
				return (false);
			}

			fileName = fileName.Trim();

			if (fileName == string.Empty)
			{
				return (false);
			}
			// **************************************************

			// **************************************************
			if (maxDownloadSpeedInKB < 0)
			{
				maxDownloadSpeedInKB = 0;
			}

			if (maxDownloadSpeedInKB > 128)
			{
				maxDownloadSpeedInKB = 128;
			}
			// **************************************************

			// **************************************************
			string strPathName = string.Empty;

			if (path.EndsWith("\\"))
			{
				strPathName =
					string.Format("{0}{1}", path, fileName);
			}
			else
			{
				strPathName =
					string.Format("{0}\\{1}", path, fileName);
			}

			if (System.IO.File.Exists(strPathName) == false)
			{
				return (false);
			}
			// **************************************************

			System.IO.Stream oStream = null;

			try
			{
				// Open the file
				oStream =
					new System.IO.FileStream
						(path: strPathName,
						mode: System.IO.FileMode.Open,
						share: System.IO.FileShare.Read,
						access: System.IO.FileAccess.Read);

				// **************************************************
				System.Web.HttpContext.Current.Response.Buffer = false;

				// Setting the unknown [ContentType]
				// will display the saving dialog for the user
				System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";

				// With setting the file name,
				// in the saving dialog, user will see
				// the [strFileName] name instead of [download]!
				System.Web.HttpContext.Current.Response
					.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

				long lngFileLength = oStream.Length;

				// Notify user (client) the total file length
				System.Web.HttpContext.Current.Response
					.AddHeader("Content-Length", lngFileLength.ToString());
				// **************************************************

				// Total bytes that should be read
				long lngDataToRead = lngFileLength;

				// If the file length is equal to zero!
				if (lngDataToRead == 0)
				{
					System.Web.HttpContext.Current.Response.Flush();

					return (true);
				}

				// Read the bytes of file
				while (lngDataToRead > 0)
				{
					if (maxDownloadSpeedInKB > 0)
					{
						System.Threading.Thread.Sleep(1000);
					}

					// Verify that the client is connected or not?
					if (System.Web.HttpContext.Current.Response.IsClientConnected)
					{
						// **************************************************
						int intBufferSize = 0;

						if (maxDownloadSpeedInKB == 0)
						{
							// 8KB
							intBufferSize = 8 * 1024;
						}
						else
						{
							intBufferSize = maxDownloadSpeedInKB * 1024;
						}
						// **************************************************

						// Create buffer for reading [intBufferSize] bytes from file
						byte[] bytBuffers =
							new System.Byte[intBufferSize];

						// Read the data and put it in the buffer.
						int intTheBytesThatReallyHasBeenReadFromTheStream =
							oStream.Read(buffer: bytBuffers, offset: 0, count: intBufferSize);

						// Write the data from buffer to the current output stream.
						System.Web.HttpContext.Current.Response.OutputStream.Write
							(buffer: bytBuffers, offset: 0,
							count: intTheBytesThatReallyHasBeenReadFromTheStream);

						// Flush (Send) the data to output
						// (Don't buffer in server's RAM!)
						System.Web.HttpContext.Current.Response.Flush();

						lngDataToRead =
							lngDataToRead - intTheBytesThatReallyHasBeenReadFromTheStream;
					}
					else
					{
						// Prevent infinite loop if user disconnected!
						lngDataToRead = -1;
					}
				}
			}
			catch
			{
				return (false);
			}
			finally
			{
				if (oStream != null)
				{
					// Close the file.
					oStream.Close();
					oStream.Dispose();
					oStream = null;
				}

				System.Web.HttpContext.Current.Response.Close();
			}

			return (true);
		}
	}
}
