using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/10/09
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class NotificationService
	{
		/// <summary>
		/// Version: 1.0.6
		/// Update Date: 1393/10/09
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		private static class Email
		{
			static Email()
			{
			}

			private static Models.MailSettings GetMailSettings()
			{
				DAL.UnitOfWork oUnitOfWork = null;

				try
				{
					oUnitOfWork = new DAL.UnitOfWork();

					Models.ApplicationSettings oApplicationSettings =
						oUnitOfWork.ApplicationSettingsRepository.Get()
						.Where(current => current.CultureLcid == System.Threading.Thread.CurrentThread.CurrentCulture.LCID)
						.FirstOrDefault();

					if (oApplicationSettings == null)
					{
						return (null);
					}
					else
					{
						return (oApplicationSettings.MailSettings);
					}
				}
				catch
				{
					return (null);
				}
				finally
				{
					if (oUnitOfWork != null)
					{
						oUnitOfWork.Dispose();
						oUnitOfWork = null;
					}
				}
			}

			private static string EmailsBodyPath
			{
				get
				{
					return ("~/App_Data/LocalizedTemplates/Email");
				}
			}

			public static void SendAgainEmailAddressVerificationKey(Models.User user)
			{
				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				Dtx.Calendar.PersianDate oPersianDate =
					Dtx.Calendar.Convert.CivilToPersion(user.InsertDateTime.Value);

				string strRootRelativePathName =
					string.Format("{0}/SendAgainEmailAddressVerificationKey.{1}.htm",
					EmailsBodyPath,
					oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strEmailBody =
					Dtx.IO.File.Read(strPathName);

				strEmailBody =
					strEmailBody
					.Replace("[DATE]", oPersianDate.Value4)
					.Replace("[EMAIL_ADDRESS]", user.EmailAddress)
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					.Replace("[VERIFICATION_KEY]", user.EmailAddressVerificationKey.ToString())
					;

				System.Net.Mail.MailAddress oMailAddress =
					new System.Net.Mail.MailAddress
						(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

				Models.MailSettings oMailSettings = GetMailSettings();
				if (oMailSettings == null)
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailAddress, "Email Address Verification!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
				else
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailSettings, oMailAddress, "Email Address Verification!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
			}

			public static void SendEmailAddressVerificationKey(Models.User user, string password)
			{
				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				Dtx.Calendar.PersianDate oPersianDate =
					Dtx.Calendar.Convert.CivilToPersion(user.InsertDateTime.Value);

				string strRootRelativePathName =
					string.Format("{0}/SendEmailAddressVerificationKey.{1}.htm",
					EmailsBodyPath,
					oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strEmailBody =
					Dtx.IO.File.Read(strPathName);

				strEmailBody =
					strEmailBody
					.Replace("[PASSWORD]", password)
					.Replace("[DATE]", oPersianDate.Value4)
					.Replace("[EMAIL_ADDRESS]", user.EmailAddress)
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					.Replace("[VERIFICATION_KEY]", user.EmailAddressVerificationKey.ToString())
					;

				System.Net.Mail.MailAddress oMailAddress =
					new System.Net.Mail.MailAddress
						(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

				Models.MailSettings oMailSettings = GetMailSettings();
				if (oMailSettings == null)
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailAddress, "Email Address Verification!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
				else
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailSettings, oMailAddress, "Email Address Verification!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
			}

			public static void SendNewPassword(Models.User user, string newPassword)
			{
				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				string strRootRelativePathName =
					string.Format("{0}/SendNewPassword.{1}.htm",
					EmailsBodyPath,
					oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strEmailBody =
					Dtx.IO.File.Read(strPathName);

				strEmailBody =
					strEmailBody
					.Replace("[PASSWORD]", newPassword)
					.Replace("[EMAIL_ADDRESS]", user.EmailAddress)
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					;

				System.Net.Mail.MailAddress oMailAddress =
					new System.Net.Mail.MailAddress
						(user.EmailAddress, user.EmailAddress, System.Text.Encoding.UTF8);

				Models.MailSettings oMailSettings = GetMailSettings();
				if (oMailSettings == null)
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailAddress, "New Password!", strEmailBody,
						System.Net.Mail.MailPriority.High);
				}
				else
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailSettings, oMailAddress, "New Password!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
			}

			public static void NotifyUserAfterUpdatingProfile(Models.User user)
			{
				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				string strRootRelativePathName =
					string.Format("{0}/NotifyUserAfterUpdatingProfile.{1}.htm",
					EmailsBodyPath, oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strEmailBody =
					Dtx.IO.File.Read(strPathName);

				strEmailBody =
					strEmailBody
					.Replace("[FULL_NAME]", user.FullName)
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					;

				System.Net.Mail.MailAddress oMailAddress =
					new System.Net.Mail.MailAddress
						(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

				Models.MailSettings oMailSettings = GetMailSettings();

				if (oMailSettings == null)
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailAddress, "Notify User After Updating Profile!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
				else
				{
					Dtx.Net.Mail.MailMessage.Send
						(oMailSettings, oMailAddress, "Notify User After Updating Profile!",
						strEmailBody, System.Net.Mail.MailPriority.High);
				}
			}
		}

		/// <summary>
		/// Version: 1.0.6
		/// Update Date: 1393/10/09
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		private static class Sms
		{
			static Sms()
			{
			}

			public static bool SendNewPassword(Models.User recipientUser, string newPassword)
			{
				// **************************************************
				if (recipientUser == null)
				{
					return (false);
				}

				if (string.IsNullOrEmpty(recipientUser.CellPhoneNumber))
				{
					return (false);
				}

				if (recipientUser.IsCellPhoneNumberVerified == false)
				{
					return (false);
				}
				// **************************************************

				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				string strRootRelativePathName =
					string.Format("~/App_Data/LocalizedTemplates/Sms/SendNewPassword.{0}.htm",
					oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strSmsBody =
					Dtx.IO.File.Read(strPathName);

				strSmsBody = strSmsBody
					.Replace("[PASSWORD]", newPassword)
					.Replace("[EMAIL_ADDRESS]", recipientUser.EmailAddress)
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					;

				return (SmsUtility.Send(recipientUser.CellPhoneNumber, strSmsBody));
			}

			public static bool SendCellPhoneNumberVerificationKey(Models.User user)
			{
				// **************************************************
				if (user == null)
				{
					return (false);
				}

				if (string.IsNullOrEmpty(user.CellPhoneNumber))
				{
					return (false);
				}

				// دستور ذيل نبايد نوشته شود
				//if (user.IsCellPhoneNumberVerified == false)
				//{
				//	return (false);
				//}
				// **************************************************

				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				string strRootRelativePathName =
					string.Format("~/App_Data/LocalizedTemplates/Sms/SendCellPhoneNumberVerificationKey.{0}.htm",
					oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strSmsBody =
					Dtx.IO.File.Read(strPathName);

				strSmsBody = strSmsBody
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					.Replace("[VERIFICATION_KEY]", user.CellPhoneNumberVerificationKey)
					;

				return (SmsUtility.Send(user.CellPhoneNumber, strSmsBody));
			}

			public static bool NotifyRecipientAfterContacting(Models.User user, string senderFullName)
			{
				// **************************************************
				if (user == null)
				{
					return (false);
				}

				if (string.IsNullOrEmpty(user.CellPhoneNumber))
				{
					return (false);
				}

				if (user.IsCellPhoneNumberVerified == false)
				{
					return (false);
				}
				// **************************************************

				System.Globalization.CultureInfo oCultureInfo =
					System.Threading.Thread.CurrentThread.CurrentCulture;

				string strRootRelativePathName =
					string.Format("~/App_Data/LocalizedTemplates/Sms/NotifyRecipientAfterContacting.{0}.htm",
					oCultureInfo.Name);

				string strPathName =
					System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

				string strSmsBody =
					Dtx.IO.File.Read(strPathName);

				strSmsBody = strSmsBody
					.Replace("[SENDER_FULL_NAME]", senderFullName)
					.Replace("[RECIPIENT_FULL_NAME]", user.FullName)
					.Replace("[SITE_URL]", GlobalApplicationSettings.Instance.SiteUrl)
					;

				return (SmsUtility.Send(user.CellPhoneNumber, strSmsBody));
			}
		}

		static NotificationService()
		{
		}

		public static void SendCellPhoneNumberVerificationKey(Models.User user)
		{
			try
			{
				if (Infrastructure.ApplicationSettings.Instance.IsSmsServiceEnabled)
				{
					Sms.SendCellPhoneNumberVerificationKey(user);
				}
			}
			catch
			{
			}
		}

		public static void SendAgainEmailAddressVerificationKey(Models.User user)
		{
			try
			{
				Email.SendAgainEmailAddressVerificationKey(user);
			}
			catch
			{
			}
		}

		public static void SendEmailAddressVerificationKey(Models.User user, string password)
		{
			try
			{
				Email.SendEmailAddressVerificationKey(user, password);
			}
			catch
			{
			}
		}

		public static void SendNewPassword(Models.User user, string newPassword)
		{
			try
			{
				if (Infrastructure.ApplicationSettings.Instance.IsSmsServiceEnabled)
				{
					if (user.IsCellPhoneNumberVerified)
					{
						Sms.SendNewPassword(user, newPassword);
					}
				}

				Email.SendNewPassword(user, newPassword);
			}
			catch
			{
			}
		}

		public static void NotifyRecipientAfterContacting(Models.User user, string senderFullName)
		{
			try
			{
				if (Infrastructure.ApplicationSettings.Instance.IsSmsServiceEnabled)
				{
					if (user.IsCellPhoneNumberVerified)
					{
						Sms.NotifyRecipientAfterContacting(user, senderFullName);
					}
				}

				//Email.NotifyRecipientAfterContacting(user, senderFullName);
			}
			catch
			{
			}
		}

		public static void NotifyUserAfterUpdatingProfile(Models.User user)
		{
			try
			{
				Email.NotifyUserAfterUpdatingProfile(user);
			}
			catch
			{
			}
		}
	}
}
