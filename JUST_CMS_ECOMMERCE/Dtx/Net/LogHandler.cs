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

			if ((System.Web.HttpContext.Current != null) &&
				(System.Web.HttpContext.Current.Request != null))
			{
				// **************************************************
				string strUserHostAddress =
					System.Web.HttpContext.Current.Request.UserHostAddress;

				if (string.IsNullOrEmpty(strUserHostAddress) == false)
				{
					strMessage =
						string.Format("User IP: {0}", strUserHostAddress);

					oResult.Append(System.Environment.NewLine);
					oResult.Append(System.Environment.NewLine);
					oResult.Append(strMessage);
					oResult.Append(System.Environment.NewLine);
					oResult.Append("**********");
				}
				// **************************************************

				// **************************************************
				string strAbsoluteUri =
					System.Web.HttpContext.Current.Request.Url.AbsoluteUri;

				if (string.IsNullOrEmpty(strAbsoluteUri) == false)
				{
					strMessage =
						string.Format("Absolute Uri: {0}", strAbsoluteUri);

					oResult.Append(System.Environment.NewLine);
					oResult.Append(System.Environment.NewLine);
					oResult.Append(strMessage);
					oResult.Append(System.Environment.NewLine);
					oResult.Append("**********");
				}
				// **************************************************

				// **************************************************
				string strHttpReferer =
					System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

				if (string.IsNullOrEmpty(strHttpReferer) == false)
				{
					strMessage =
						string.Format("Http Referer: {0}", strHttpReferer);

					oResult.Append(System.Environment.NewLine);
					oResult.Append(System.Environment.NewLine);
					oResult.Append(strMessage);
					oResult.Append(System.Environment.NewLine);
					oResult.Append("**********");
				}
				// **************************************************
			}

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
			System.Exception oException = null;

			oException = ex;

			System.Data.Entity.Validation.DbEntityValidationException oDbEntityValidationException =
				oException as System.Data.Entity.Validation.DbEntityValidationException;

			if (oDbEntityValidationException == null)
			{
				if (string.IsNullOrEmpty(ex.Message) == false)
				{
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs + "Exception Message:");
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs + oException.Message);
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs + "**********");
				}

				if (string.IsNullOrEmpty(ex.StackTrace) == false)
				{
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs + "Stack Trace:");
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs + oException.StackTrace);
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs + "**********");
				}
			}
			else
			{
				string strTab = "\t";

				errorMessage.Append(System.Environment.NewLine);

				foreach (var varEntityValidationError in oDbEntityValidationException.EntityValidationErrors)
				{
					errorMessage.Append(System.Environment.NewLine);
					errorMessage.Append(tabs);

					errorMessage.Append(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						varEntityValidationError.Entry.Entity.GetType().Name, varEntityValidationError.Entry.State));

					foreach (var varValidationError in varEntityValidationError.ValidationErrors)
					{
						errorMessage.Append(System.Environment.NewLine);
						errorMessage.Append(tabs);
						errorMessage.Append(strTab);

						errorMessage.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"",
							varValidationError.PropertyName, varValidationError.ErrorMessage));
					}
				}
			}
		}

		private static bool LogToFile(string message)
		{
			bool blnResult = false;

			string strLogPathName =
				Dtx.ApplicationSettings.GetValue("ApplicationLogRootRelativePathName");

			if (string.IsNullOrEmpty(strLogPathName) == false)
			{
				if ((System.Web.HttpContext.Current != null) &&
					(System.Web.HttpContext.Current.Server != null) &&
					(System.Web.HttpContext.Current.Application != null))
				{
					strLogPathName =
						System.Web.HttpContext.Current.Server.MapPath(strLogPathName);

					System.Web.HttpContext.Current.Application.Lock();
				}

				blnResult =
					Dtx.IO.File.Append(strLogPathName, message);

				if ((System.Web.HttpContext.Current != null) &&
					(System.Web.HttpContext.Current.Server != null) &&
					(System.Web.HttpContext.Current.Application != null))
				{
					System.Web.HttpContext.Current.Application.UnLock();
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
