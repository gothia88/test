using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	public static class SmsUtility
	{
		static SmsUtility()
		{
		}

		public static int GetCredit(Models.GlobalApplicationSettings globalApplicationSettings = null)
		{
			int intResult = 0;
			MY_WEB_APPLICATION.ir.dorsanet.sms.Send oSend = null;

			try
			{
				if (globalApplicationSettings == null)
				{
					globalApplicationSettings =
						Infrastructure.GlobalApplicationSettings.Instance;
				}

				oSend =
					new MY_WEB_APPLICATION.ir.dorsanet.sms.Send();

				string strUsername =
					globalApplicationSettings.SmsCenterUsername;

				string strPassword =
					globalApplicationSettings.SmsCenterPassword;

				// اگر خطای ذيل را دريافت کرديم
				// The request failed with HTTP status 417: Expectation failed.
				// اين بدان معنی است که
				// Proxy
				// اجرا شده و بايد آنرا قطع نماييم
				double dblResult =
					oSend.Credit(strUsername, strPassword);

				intResult = System.Convert.ToInt32(dblResult);
			}
			catch //(System.Exception ex)
			{
				intResult = -1;
			}
			finally
			{
				if (oSend != null)
				{
					oSend.Dispose();
					oSend = null;
				}
			}

			return (intResult);
		}

		public static bool Send
			(string recipients, string text, Models.GlobalApplicationSettings globalApplicationSettings = null)
		{
			if (recipients == null)
			{
				return (false);
			}

			recipients = recipients.Replace(" ", string.Empty);
			if (recipients == string.Empty)
			{
				return (false);
			}

			MY_WEB_APPLICATION.ir.dorsanet.sms.Send oSend = null;

			try
			{
				if (globalApplicationSettings == null)
				{
					globalApplicationSettings =
						Infrastructure.GlobalApplicationSettings.Instance;
				}

				//if (globalApplicationSettings.IsSmsServiceEnabled == false)
				//{
				//	return (false);
				//}

				oSend =
					new MY_WEB_APPLICATION.ir.dorsanet.sms.Send();

				int intCredit =
					GetCredit(globalApplicationSettings);

				if (intCredit <= 0)
				{
					return (false);
				}
				else
				{
					string strUsername =
						globalApplicationSettings.SmsCenterUsername;

					string strPassword =
						globalApplicationSettings.SmsCenterPassword;

					// شماره فرستنده
					string strFrom =
						globalApplicationSettings.SmsCenterNumber;

					// شماره های گيرنده يا گيرندگان
					string[] strRecipients = recipients.Split(',', ':', ';', '|');

					System.Collections.Generic.List<string> oTo =
						new System.Collections.Generic.List<string>();

					for (int intIndex = 0; intIndex <= strRecipients.Length - 1; intIndex++)
					{
						string strRecipient = strRecipients[intIndex];

						if (strRecipient.StartsWith("0098"))
						{
							strRecipient = strRecipient.Substring(4);

							if (strRecipient.Length == 10)
							{
								oTo.Add(strRecipient);
							}
						}
						else
						{
							if (strRecipient.StartsWith("0"))
							{
								strRecipient = strRecipient.Substring(1);

								if (strRecipient.Length == 10)
								{
									oTo.Add(strRecipient);
								}
							}
							else
							{
								if (strRecipient.Length == 10)
								{
									oTo.Add(strRecipient);
								}
							}
						}
					}

					string[] strTo = new string[oTo.Count];
					for (int intIndex = 0; intIndex <= oTo.Count - 1; intIndex++)
					{
						string strRecipient = oTo[intIndex];

						strTo[intIndex] = strRecipient;
					}

					//string strText = "اين اس ام اس برای تست ارسال شده است";

					string strText = text;

					// پيام به صورت خودکار روی گوشی گيرنده (گيرندگان) باز شود؟
					bool blnFlash = false;

					long[] lngRecIds = null;
					byte[] bytStatuses = null;

					int intSendingSmsResult = oSend.SendSms
						(username: strUsername,
						password: strPassword,
						to: strTo,
						from: strFrom,
						text: strText,
						flash: blnFlash,
						status: ref bytStatuses,
						recId: ref lngRecIds);

					if ((intSendingSmsResult == 0) && (bytStatuses.Length > 0) && (bytStatuses[0] == 0))
					{
						return (true);
					}
					else
					{
						return (false);
					}
				}
			}
			catch //(System.Exception ex)
			{
				return (false);
			}
			finally
			{
				if (oSend != null)
				{
					oSend.Dispose();
					oSend = null;
				}
			}
		}
	}
}
