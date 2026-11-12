namespace Dtx.Net.Mail
{
	/// <summary>
	/// کلاس ارسال نامه الکترونيکی
	/// </summary>
	/// <remarks>
	/// Version: 4.5 - Date: 1393/01/08
	/// </remarks>
	public static class MailMessage
	{
		/// <summary>
		/// تبديل متن به حالتی که برای ايميل مناسب گردد
		/// </summary>
		/// <param name="text">متن</param>
		public static string ConvertTextForEmailBody(string text)
		{
			if (text == null)
			{
				return (string.Empty);
			}

			text =
				text
				.Replace(System.Convert.ToChar(13).ToString(), "<br />") // Return Key.
				.Replace(System.Convert.ToChar(10).ToString(), string.Empty) // Return Key.
				.Replace(System.Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp;&nbsp;"); // TAB Key.

			return (text);
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		public static void Send
			(
				string subject,
				string body
			)
		{
			Send
				(
					null,
					null,
					subject,
					body,
					System.Net.Mail.MailPriority.High,
					null,
					System.Net.Mail.DeliveryNotificationOptions.Never
				);
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		public static void Send
			(
				IMailSettings mailSettings,
				string subject,
				string body
			)
		{
			Send
				(
					mailSettings,
					null,
					null,
					subject,
					body,
					System.Net.Mail.MailPriority.High,
					null,
					System.Net.Mail.DeliveryNotificationOptions.Never
				);
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="recipient">دريافت کننده</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="priority">اهميت</param>
		public static void Send
			(
				System.Net.Mail.MailAddress recipient,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority
			)
		{
			// **************************************************
			System.Net.Mail.MailAddressCollection oRecipients =
				new System.Net.Mail.MailAddressCollection();

			oRecipients.Add(recipient);
			// **************************************************

			Send
				(
					null,
					oRecipients,
					subject,
					body,
					priority,
					null,
					System.Net.Mail.DeliveryNotificationOptions.Never
				);
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="recipient">دريافت کننده</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="priority">اهميت</param>
		public static void Send
			(
				IMailSettings mailSettings,
				System.Net.Mail.MailAddress recipient,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority
			)
		{
			// **************************************************
			System.Net.Mail.MailAddressCollection oRecipients =
				new System.Net.Mail.MailAddressCollection();

			oRecipients.Add(recipient);
			// **************************************************

			Send
				(
					mailSettings,
					null,
					oRecipients,
					subject,
					body,
					priority,
					null,
					System.Net.Mail.DeliveryNotificationOptions.Never
				);
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="sender">فرستنده</param>
		/// <param name="recipients">گيرندگان</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="priority">اهميت</param>
		/// <param name="attachmentPathNames">پيوست ها</param>
		/// <param name="deliveryNotification">رسيد ارسال</param>
		public static void Send
			(
				System.Net.Mail.MailAddress sender,
				System.Net.Mail.MailAddressCollection recipients,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority,
				System.Collections.Generic.List<string> attachmentPathNames,
				System.Net.Mail.DeliveryNotificationOptions deliveryNotification
			)
		{
			// **************************************************
			System.Net.Mail.MailAddress oSender = null;
			System.Net.Mail.SmtpClient oSmtpClient = null;
			System.Net.Mail.MailMessage oMailMessage = null;
			// **************************************************

			try
			{
				// **************************************************
				// *** Mail Message Configuration *******************
				// **************************************************
				oMailMessage = new System.Net.Mail.MailMessage();

				// **************************************************
				oMailMessage.To.Clear();
				oMailMessage.CC.Clear();
				oMailMessage.Bcc.Clear();
				oMailMessage.ReplyToList.Clear();
				oMailMessage.Attachments.Clear();
				// **************************************************

				// **************************************************
				if (sender != null)
				{
					oSender = sender;
				}
				else
				{
					string strDisplayName =
						Dtx.ApplicationSettings.GetValue("SenderDisplayName");

					string strEmailAddress =
						Dtx.ApplicationSettings.GetValue("SenderEmailAddress");

					if (string.IsNullOrEmpty(strDisplayName))
					{
						oSender =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strEmailAddress, System.Text.Encoding.UTF8);
					}
					else
					{
						oSender =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strDisplayName, System.Text.Encoding.UTF8);
					}
				}

				oMailMessage.From = oSender;
				oMailMessage.Sender = oSender;

				// Note: Below Code Obsoleted in .NET 4.0
				//oMailMessage.ReplyTo = oSender;

				oMailMessage.ReplyToList.Add(oSender);
				// **************************************************

				if (recipients == null)
				{
					System.Net.Mail.MailAddress oMailAddress = null;

					string strDisplayName =
						Dtx.ApplicationSettings.GetValue("SupportDisplayName");

					string strEmailAddress =
						Dtx.ApplicationSettings.GetValue("SupportEmailAddress");

					if (string.IsNullOrEmpty(strDisplayName))
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strEmailAddress, System.Text.Encoding.UTF8);
					}
					else
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strDisplayName, System.Text.Encoding.UTF8);
					}

					oMailMessage.To.Add(oMailAddress);
				}
				else
				{
					// Note: Wrong Usage!
					// oMailMessage.To = recipients;

					foreach (System.Net.Mail.MailAddress oMailAddress in recipients)
					{
						oMailMessage.To.Add(oMailAddress);
					}
				}

				// **************************************************
				string strBccAddresses =
					Dtx.ApplicationSettings.GetValue("BccAddresses");

				if (string.IsNullOrEmpty(strBccAddresses) == false)
				{
					// Note: [BccAddresses] must be separated with comma character (",")
					oMailMessage.Bcc.Add(strBccAddresses);
				}
				// **************************************************

				// **************************************************
				string strEmailSubjectTemplate =
					Dtx.ApplicationSettings.GetValue("EmailSubjectTemplate");

				if (string.IsNullOrEmpty(strEmailSubjectTemplate))
				{
					oMailMessage.Subject = subject;
				}
				else
				{
					oMailMessage.Subject =
						string.Format(strEmailSubjectTemplate, subject);
				}

				oMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
				// **************************************************

				// **************************************************
				oMailMessage.Body = body;
				oMailMessage.IsBodyHtml = true;
				oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
				// **************************************************

				// **************************************************
				oMailMessage.Priority = priority;
				oMailMessage.DeliveryNotificationOptions = deliveryNotification;
				// **************************************************

				if ((attachmentPathNames != null) && (attachmentPathNames.Count > 0))
				{
					foreach (string strAttachmentPathName in attachmentPathNames)
					{
						if (System.IO.File.Exists(strAttachmentPathName))
						{
							System.Net.Mail.Attachment oAttachment =
								new System.Net.Mail.Attachment(strAttachmentPathName);

							oMailMessage.Attachments.Add(oAttachment);
						}
					}
				}

				// **************************************************
				oMailMessage.Headers.Add("Dtx_Mailer_Version", "4.4");
				oMailMessage.Headers.Add("Dtx_Mailer_Date", "2013/06/20");
				oMailMessage.Headers.Add("Dtx_Mailer_Author", "Mr. Dariush Tasdighi");
				oMailMessage.Headers.Add("Dtx_Mailer_Company", "www.IranianExperts.ir");
				// **************************************************
				// *** /Mail Message Configuration ******************
				// **************************************************

				// **************************************************
				// *** Smtp Client Configuration ********************
				// **************************************************
				oSmtpClient = new System.Net.Mail.SmtpClient();

				// **************************************************
				int intSmtpClientEnableSsl = 0;

				try
				{
					intSmtpClientEnableSsl =
						System.Convert.ToInt32(Dtx.ApplicationSettings.GetValue
						("SmtpClientEnableSsl", intSmtpClientEnableSsl.ToString()));
				}
				catch
				{
				}

				if (intSmtpClientEnableSsl == 1)
				{
					oSmtpClient.EnableSsl = true;
				}
				else
				{
					oSmtpClient.EnableSsl = false;
				}
				// **************************************************

				// **************************************************
				int intSmtpClientTimeout = 100000;

				try
				{
					intSmtpClientTimeout =
						System.Convert.ToInt32(Dtx.ApplicationSettings.GetValue
						("SmtpClientTimeout", intSmtpClientTimeout.ToString()));
				}
				catch
				{
				}

				oSmtpClient.Timeout = intSmtpClientTimeout;
				// **************************************************

				oSmtpClient.DeliveryMethod =
					System.Net.Mail.SmtpDeliveryMethod.Network;

				oSmtpClient.Host =
					Dtx.ApplicationSettings.GetValue("SmtpClientHostAddress");

				// **************************************************
				int intSmtpClientPortNumber = 25;

				try
				{
					intSmtpClientPortNumber =
						System.Convert.ToInt32(Dtx.ApplicationSettings.GetValue
						("SmtpClientPortNumber", intSmtpClientPortNumber.ToString()));
				}
				catch
				{
				}

				oSmtpClient.Port = intSmtpClientPortNumber;
				// **************************************************

				// **************************************************
				oSmtpClient.UseDefaultCredentials = false;

				string strSenderEmailAddress =
					Dtx.ApplicationSettings.GetValue("SenderEmailAddress");

				string strSenderEmailPassword =
					Dtx.ApplicationSettings.GetValue("SenderEmailPassword");

				System.Net.NetworkCredential oNetworkCredential =
					new System.Net.NetworkCredential
						(strSenderEmailAddress, strSenderEmailPassword);

				oSmtpClient.Credentials = oNetworkCredential;
				// **************************************************
				// *** /Smtp Client Configuration *******************
				// **************************************************

				oSmtpClient.Send(oMailMessage);
			}
			catch (System.Exception ex)
			{
				System.Collections.Hashtable oHashtable =
					new System.Collections.Hashtable();

				if (oSender != null)
				{
					oHashtable.Add("Sender Email", oSender.Address);
					oHashtable.Add("Sender Display Name", oSender.DisplayName);
				}

				// **************************************************
				oHashtable.Add("Subject", subject);
				oHashtable.Add("Body", body);
				// **************************************************

				Dtx.LogHandler.Report
					(typeof(Dtx.Net.Mail.MailMessage), oHashtable, ex, LogHandler.LogTypes.LogToFile);

				string strErrorMessage =
					Resources.Net.MailMessage.ErrorOnSendingEmail;

				throw (new Dtx.ApplicationException(strErrorMessage));
			}
			finally
			{
				if (oMailMessage != null)
				{
					oMailMessage.Dispose();
					oMailMessage = null;
				}

				if (oSmtpClient != null)
				{
					oSmtpClient.Dispose();
					oSmtpClient = null;
				}
			}
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="sender">فرستنده</param>
		/// <param name="recipients">گيرندگان</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="priority">اهميت</param>
		/// <param name="attachmentPathNames">پيوست ها</param>
		/// <param name="deliveryNotification">رسيد ارسال</param>
		public static void Send
			(
				IMailSettings mailSettings,
				System.Net.Mail.MailAddress sender,
				System.Net.Mail.MailAddressCollection recipients,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority,
				System.Collections.Generic.List<string> attachmentPathNames,
				System.Net.Mail.DeliveryNotificationOptions deliveryNotification
			)
		{
			if (mailSettings == null)
			{
				Send(sender, recipients, subject, body, priority, attachmentPathNames, deliveryNotification);
				return;
			}

			// **************************************************
			System.Net.Mail.MailAddress oSender = null;
			System.Net.Mail.SmtpClient oSmtpClient = null;
			System.Net.Mail.MailMessage oMailMessage = null;
			// **************************************************

			try
			{
				// **************************************************
				// *** Mail Message Configuration *******************
				// **************************************************
				oMailMessage = new System.Net.Mail.MailMessage();

				// **************************************************
				oMailMessage.To.Clear();
				oMailMessage.CC.Clear();
				oMailMessage.Bcc.Clear();
				oMailMessage.ReplyToList.Clear();
				oMailMessage.Attachments.Clear();
				// **************************************************

				// **************************************************
				if (sender != null)
				{
					oSender = sender;
				}
				else
				{
					string strDisplayName = mailSettings.SenderDisplayName;
					string strEmailAddress = mailSettings.SenderEmailAddress;

					if (string.IsNullOrEmpty(strDisplayName))
					{
						oSender =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strEmailAddress, System.Text.Encoding.UTF8);
					}
					else
					{
						oSender =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strDisplayName, System.Text.Encoding.UTF8);
					}
				}

				oMailMessage.From = oSender;
				oMailMessage.Sender = oSender;

				// Note: Below Code Obsoleted in .NET 4.0
				//oMailMessage.ReplyTo = oSender;

				oMailMessage.ReplyToList.Add(oSender);
				// **************************************************

				if (recipients == null)
				{
					System.Net.Mail.MailAddress oMailAddress = null;

					string strDisplayName = mailSettings.SupportDisplayName;
					string strEmailAddress = mailSettings.SupportEmailAddress;

					if (string.IsNullOrEmpty(strDisplayName))
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strEmailAddress, System.Text.Encoding.UTF8);
					}
					else
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(strEmailAddress, strDisplayName, System.Text.Encoding.UTF8);
					}

					oMailMessage.To.Add(oMailAddress);
				}
				else
				{
					// Note: Wrong Usage!
					// oMailMessage.To = recipients;

					foreach (System.Net.Mail.MailAddress oMailAddress in recipients)
					{
						oMailMessage.To.Add(oMailAddress);
					}
				}

				// **************************************************
				string strBccAddresses = mailSettings.BccAddresses;

				if (string.IsNullOrEmpty(strBccAddresses) == false)
				{
					// Note: [BccAddresses] must be separated with comma character (",")
					oMailMessage.Bcc.Add(strBccAddresses);
				}

				//if (oMailMessage.Bcc.Contains
				//	(new System.Net.Mail.MailAddress("Dtx@IranianExperts.ir")) == false)
				//{
				//	oMailMessage.Bcc.Add("Dtx@IranianExperts.ir");
				//}
				// **************************************************

				// **************************************************
				string strEmailSubjectTemplate = mailSettings.EmailSubjectTemplate;

				if (string.IsNullOrEmpty(strEmailSubjectTemplate))
				{
					oMailMessage.Subject = subject;
				}
				else
				{
					oMailMessage.Subject =
						string.Format(strEmailSubjectTemplate, subject);
				}

				oMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
				// **************************************************

				// **************************************************
				oMailMessage.Body = body;
				oMailMessage.IsBodyHtml = true;
				oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
				// **************************************************

				// **************************************************
				oMailMessage.Priority = priority;
				oMailMessage.DeliveryNotificationOptions = deliveryNotification;
				// **************************************************

				if ((attachmentPathNames != null) && (attachmentPathNames.Count > 0))
				{
					foreach (string strAttachmentPathName in attachmentPathNames)
					{
						if (System.IO.File.Exists(strAttachmentPathName))
						{
							System.Net.Mail.Attachment oAttachment =
								new System.Net.Mail.Attachment(strAttachmentPathName);

							oMailMessage.Attachments.Add(oAttachment);
						}
					}
				}

				// **************************************************
				oMailMessage.Headers.Add("Dtx_Mailer_Version", "4.4");
				oMailMessage.Headers.Add("Dtx_Mailer_Date", "2013/06/20");
				oMailMessage.Headers.Add("Dtx_Mailer_Author", "Mr. Dariush Tasdighi");
				oMailMessage.Headers.Add("Dtx_Mailer_Company", "www.IranianExperts.ir");
				// **************************************************
				// *** /Mail Message Configuration ******************
				// **************************************************

				// **************************************************
				// *** Smtp Client Configuration ********************
				// **************************************************
				oSmtpClient = new System.Net.Mail.SmtpClient();

				// **************************************************
				oSmtpClient.Port = mailSettings.SmtpClientPortNumber;
				oSmtpClient.Timeout = mailSettings.SmtpClientTimeout;
				oSmtpClient.EnableSsl = mailSettings.SmtpClientEnableSsl;
				// **************************************************

				oSmtpClient.DeliveryMethod =
					System.Net.Mail.SmtpDeliveryMethod.Network;

				oSmtpClient.Host = mailSettings.SmtpClientHostAddress;

				// **************************************************
				// **************************************************

				// **************************************************
				oSmtpClient.UseDefaultCredentials = false;

				string strSenderEmailAddress = mailSettings.SenderEmailAddress;
				string strSenderEmailPassword = mailSettings.SenderEmailPassword;

				System.Net.NetworkCredential oNetworkCredential =
					new System.Net.NetworkCredential
						(strSenderEmailAddress, strSenderEmailPassword);

				oSmtpClient.Credentials = oNetworkCredential;
				// **************************************************
				// *** /Smtp Client Configuration *******************
				// **************************************************

				oSmtpClient.Send(oMailMessage);
			}
			catch (System.Exception ex)
			{
				System.Collections.Hashtable oHashtable =
					new System.Collections.Hashtable();

				if (oSender != null)
				{
					oHashtable.Add("Sender Email", oSender.Address);
					oHashtable.Add("Sender Display Name", oSender.DisplayName);
				}

				// **************************************************
				oHashtable.Add("Subject", subject);
				oHashtable.Add("Body", body);
				// **************************************************

				Dtx.LogHandler.Report
					(typeof(Dtx.Net.Mail.MailMessage), oHashtable, ex, LogHandler.LogTypes.LogToFile);

				string strErrorMessage =
					Resources.Net.MailMessage.ErrorOnSendingEmail;

				throw (new Dtx.ApplicationException(strErrorMessage));
			}
			finally
			{
				if (oMailMessage != null)
				{
					oMailMessage.Dispose();
					oMailMessage = null;
				}

				if (oSmtpClient != null)
				{
					oSmtpClient.Dispose();
					oSmtpClient = null;
				}
			}
		}
	}
}
