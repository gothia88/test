namespace Dtx
{
	/// <summary>
	/// ثبت و/يا ارسال خطا به مسوولين سيستم و/يا پايگاه
	/// </summary>
	/// <remarks>
	/// Version: 4.3 - Date: 1393/01/23
	/// </remarks>
	public static class LogHandler
	{
		#region Enumeration(s)
		public enum LogTypes
		{
			Both = 0,
			LogToFile = 1,
			SendByEmail = 2
		}
		#endregion /Enumeration(s)

		// Replaces the ASP.NET System.Web.HttpContext.Current.Application.Lock/UnLock
		// used in the .NET Framework original.
		private static readonly object _lockObject = new object();

		/// <summary>
		/// Resolves a root-relative path name to an absolute physical path.
		/// </summary>
		/// <remarks>
		/// The .NET Framework original used System.Web.HttpContext.Current.Server.MapPath.
		/// </remarks>
		private static string ResolveRootRelativePath(string rootRelativePathName)
		{
			if (rootRelativePathName == null)
			{
				return (string.Empty);
			}

			string strPath =
				rootRelativePathName
				.Replace("~/", string.Empty)
				.Replace("~\\", string.Empty)
				.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString())
				.Replace("\\", System.IO.Path.DirectorySeparatorChar.ToString());

			if (System.IO.Path.IsPathRooted(strPath))
			{
				return (System.IO.Path.GetFullPath(strPath));
			}

			return (System.IO.Path.GetFullPath(
				System.IO.Path.Combine(System.AppContext.BaseDirectory, strPath)));
		}

		private static string GetErrorMessage
			(
				System.Type type,
				System.Collections.Hashtable parameters,
				System.Exception ex
			)
		{
			// **************************************************
			string strMessage = string.Empty;

			System.Text.StringBuilder oResult =
				new System.Text.StringBuilder();
			// **************************************************

			// **************************************************
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**************************************************");
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**************************************************");
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**************************************************");
			// **************************************************

			// **************************************************
			strMessage =
				string.Format("Timestamp: {0:yyyy/MM/dd - HH:mm:ss}", System.DateTime.Now);

			oResult.Append(System.Environment.NewLine);
			oResult.Append(System.Environment.NewLine);
			oResult.Append(strMessage);
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**********");
			// **************************************************

			// NOTE: In the .NET Framework original, this method also appended the
			// request IP address, absolute URI and HTTP referer from
			// System.Web.HttpContext.Current.Request. That web-specific enrichment
			// was removed in the .NET 10 port.

			if (type != null)
			{
				strMessage =
					string.Format("Type: {0}", type);

				oResult.Append(System.Environment.NewLine);
				oResult.Append(System.Environment.NewLine);
				oResult.Append(strMessage);
				oResult.Append(System.Environment.NewLine);
				oResult.Append("**********");
			}

			if ((parameters != null) && (parameters.Count != 0))
			{
				// **************************************************
				oResult.Append(System.Environment.NewLine);
				oResult.Append(System.Environment.NewLine);
				oResult.Append("********************");
				oResult.Append(System.Environment.NewLine);
				oResult.Append("Parameter(s)");
				// **************************************************

				foreach (System.Collections.DictionaryEntry oEntry in parameters)
				{
					oResult.Append(System.Environment.NewLine);
					oResult.Append(System.Environment.NewLine);

					if (oEntry.Key != null)
					{
						strMessage =
							string.Format("\tKey: {0}", oEntry.Key);

						oResult.Append(strMessage);

						if (oEntry.Value == null)
						{
							strMessage = "\tValue: null";

							oResult.Append(System.Environment.NewLine);
							oResult.Append(strMessage);
						}
						else
						{
							strMessage =
								string.Format("\tValue: {0}", oEntry.Value);

							oResult.Append(System.Environment.NewLine);
							oResult.Append(strMessage);
						}
					}

					oResult.Append(System.Environment.NewLine);
					oResult.Append("\t**********");
				}

				// **************************************************
				oResult.Append(System.Environment.NewLine);
				oResult.Append(System.Environment.NewLine);
				oResult.Append("/Parameter(s)");
				oResult.Append(System.Environment.NewLine);
				oResult.Append("********************");
				// **************************************************
			}

			// **************************************************
			oResult.Append(System.Environment.NewLine);
			oResult.Append(System.Environment.NewLine);
			oResult.Append("********************");
			oResult.Append(System.Environment.NewLine);
			oResult.Append("Exception(s)");
			// **************************************************

			// **************************************************
			string strTabs = string.Empty;
			System.Exception oException = ex;
			while (oException != null)
			{
				LogException(oException, oResult, strTabs);

				strTabs += "\t";
				oException = oException.InnerException;
			}
			// **************************************************

			// **************************************************
			oResult.Append(System.Environment.NewLine);
			oResult.Append(System.Environment.NewLine);
			oResult.Append("/Exception(s)");
			oResult.Append(System.Environment.NewLine);
			oResult.Append("********************");
			// **************************************************

			// **************************************************
			oResult.Append(System.Environment.NewLine);
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**************************************************");
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**************************************************");
			oResult.Append(System.Environment.NewLine);
			oResult.Append("**************************************************");
			oResult.Append(System.Environment.NewLine);
			// **************************************************

			return (oResult.ToString());
		}

		private static void LogException
			(System.Exception ex, System.Text.StringBuilder errorMessage, string tabs)
		{
			// NOTE: The .NET Framework original special-cased
			// System.Data.Entity.Validation.DbEntityValidationException here. The
			// Entity Framework dependency was dropped in the .NET 10 port.

			if (string.IsNullOrEmpty(ex.Message) == false)
			{
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(tabs + "Exception Message:");
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(tabs + ex.Message);
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(tabs + "**********");
			}

			if (string.IsNullOrEmpty(ex.StackTrace) == false)
			{
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(tabs + "Stack Trace:");
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(tabs + ex.StackTrace);
				errorMessage.Append(System.Environment.NewLine);
				errorMessage.Append(tabs + "**********");
			}
		}

		private static bool LogToFile(string message)
		{
			bool blnResult = false;

			string strLogPathName =
				Dtx.ApplicationSettings.GetValue("ApplicationLogRootRelativePathName");

			if (string.IsNullOrEmpty(strLogPathName) == false)
			{
				strLogPathName =
					ResolveRootRelativePath(strLogPathName);

				lock (_lockObject)
				{
					blnResult =
						Dtx.IO.File.Append(strLogPathName, message);
				}
			}

			return (blnResult);
		}

		private static bool SendByEmail(string message)
		{
			message =
				Dtx.Net.Mail.MailMessage.ConvertTextForEmailBody(message);

			// **************************************************
			string strBody = string.Empty;

			strBody += "<div style='color: Blue;background-color: Yellow;direction: ltr;text-align: left;font-size: 10pt;font-family: Verdana;margin: 10px;padding: 10px;border: thin outset Gray;'>";
			strBody += message;
			strBody += "</div>";
			// **************************************************

			string strSubject = "Error Notification!";

			try
			{
				Dtx.Net.Mail.MailMessage.Send(strSubject, strBody);

				return (true);
			}
			catch
			{
				return (false);
			}
		}

		public static string Report
			(
				System.Type type,
				System.Collections.Hashtable parameters,
				System.Exception ex
			)
		{
			LogTypes enmLogType = LogTypes.Both;

			try
			{
				byte bytLogType =
					System.Convert.ToByte(Dtx.ApplicationSettings.GetValue("DefaultLogType", "0"));

				enmLogType = (LogTypes)bytLogType;
			}
			catch
			{
			}

			return (Report(type, parameters, ex, enmLogType));
		}

		public static string Report
			(
				System.Type type,
				System.Collections.Hashtable parameters,
				System.Exception ex,
				Dtx.LogHandler.LogTypes logType
			)
		{
			string strErrorMessage =
				GetErrorMessage(type, parameters, ex);

			switch (logType)
			{
				case LogTypes.Both:
				{
					LogToFile(strErrorMessage);
					SendByEmail(strErrorMessage);

					break;
				}

				case LogTypes.SendByEmail:
				{
					if (SendByEmail(strErrorMessage) == false)
					{
						LogToFile(strErrorMessage);
					}

					break;
				}

				case LogTypes.LogToFile:
				{
					// **************************************************
					// Note: Never Write Below Codes!
					//if (LogToFile(strErrorMessage) == false)
					//{
					//	SendByEmail(strErrorMessage);
					//}
					// **************************************************

					LogToFile(strErrorMessage);

					break;
				}
			}

			return (strErrorMessage);
		}
	}
}
