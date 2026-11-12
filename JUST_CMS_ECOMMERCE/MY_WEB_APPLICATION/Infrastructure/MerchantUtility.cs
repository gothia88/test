using System.Linq;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/01/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class MerchantUtility
	{
		static MerchantUtility()
		{
		}

		public static string SendRequestForPayment(Models.ECommerce.Merchant merchant, double amount)
		{
			System.Net.WebRequest oWebRequest =
				System.Net.WebRequest.Create(merchant.FirstSendUrl);

			oWebRequest.Method = merchant.FirstSendMethod;
			oWebRequest.ContentType = "application/x-www-form-urlencoded";

			string strParameters =
				string.Format("{0}={1}&{2}={3}&{4}={5}",
				merchant.ApiName, merchant.ApiValue,
				merchant.AmountName, amount,
				merchant.RedirectName, merchant.RedirectValue);

			byte[] bytBuffer =
				System.Text.Encoding.UTF8.GetBytes(strParameters);

			oWebRequest.ContentLength = bytBuffer.Length;

			System.IO.Stream oDataStream =
				oWebRequest.GetRequestStream();

			oDataStream.Write
				(buffer: bytBuffer, offset: 0, count: bytBuffer.Length);

			oDataStream.Close();

			System.Net.WebResponse oWebResponse =
				oWebRequest.GetResponse();

			oDataStream =
				oWebResponse.GetResponseStream();

			System.IO.StreamReader oStreamReader =
				new System.IO.StreamReader(oDataStream);

			string oResponseFields =
				oStreamReader.ReadToEnd();

			oStreamReader.Close();

			string strResult = "-1";

			try
			{
				strResult =
					oResponseFields.ToString();
			}
			catch
			{
			}

			return (strResult);
		}

		public static string SendRequestForCheckingPayment
			(Models.ECommerce.Merchant merchant, string transactionIdentity, string identityGet)
		{
			System.Net.WebRequest oWebRequest =
				System.Net.WebRequest.Create(merchant.GetUrl);

			oWebRequest.Method = merchant.GetMethod;
			oWebRequest.ContentType = "application/x-www-form-urlencoded";

			string strParameters =
				string.Format("{0}={1}&{2}={3}&{4}={5}",
				merchant.ApiName, merchant.ApiValue,
				merchant.TransactionIdentityName, transactionIdentity,
				merchant.IdentityGetName, identityGet);

			byte[] bytBuffer =
				System.Text.Encoding.UTF8.GetBytes(strParameters);

			oWebRequest.ContentLength = bytBuffer.Length;

			System.IO.Stream oDataStream =
				oWebRequest.GetRequestStream();

			oDataStream.Write
				(buffer: bytBuffer, offset: 0, count: bytBuffer.Length);

			oDataStream.Close();

			System.Net.WebResponse oWebResponse =
				oWebRequest.GetResponse();

			oDataStream =
				oWebResponse.GetResponseStream();

			System.IO.StreamReader oStreamReader =
				new System.IO.StreamReader(oDataStream);

			string strResponseFields =
				oStreamReader.ReadToEnd();

			oStreamReader.Close();

			string strResult = "-1";

			try
			{
				strResult =
					strResponseFields.ToString();
			}
			catch
			{
			}

			return (strResult);
		}
	}
}
