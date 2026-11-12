namespace Dtx.Rss
{
	/// <summary>
	/// License
	/// </summary>
	/// <remarks>
	/// Version: 1.0.5
	/// Update Date: 1393/04/24
	/// Developer: Mr. Dariush Tasdighi
	/// </remarks>
	internal static class License
	{
		/// <summary>
		/// 
		/// </summary>
		static License()
		{
		}

		/// <summary>
		/// Check License Key
		/// </summary>
		internal static void CheckKey()
		{
		}

		/// <summary>
		/// Check License Key
		/// </summary>
		//internal static void CheckKey()
		//{
		//	// نباشد، کاربر می‌تواند بدون هيچ محدوديتی از اين ابزار استفاده نمايد Web Based در صورتی که پروژه به صورت
		//	if ((System.Web.HttpContext.Current == null) ||
		//		(System.Web.HttpContext.Current.Request == null) ||
		//		(System.Web.HttpContext.Current.Request.Url == null) ||
		//		(System.Web.HttpContext.Current.Request.Url.Host == null))
		//	{
		//		return;
		//	}

		//	string strDomain =
		//		System.Web.HttpContext.Current.Request.Url.Host;

		//	// نباشد، کاربر می‌تواند بدون هيچ محدوديتی از اين ابزار استفاده نمايد Web Based در صورتی که پروژه به صورت
		//	if (String.IsNullOrEmptyOrWhiteSpace(strDomain))
		//	{
		//		return;
		//	}

		//	strDomain = strDomain.Replace(" ", string.Empty);

		//	// اجرا شود نيز، کاربر می‌تواند بدون هيچ محدوديتی از اين ابزار استفاده نمايد LocalHost بوده و در Web Based در صورتی که پروژه به صورت
		//	if (string.Compare(strDomain, "localhost",
		//		System.StringComparison.InvariantCultureIgnoreCase) == 0)
		//	{
		//		return;
		//	}

		//	string strLicenseKey = GetKey(strDomain);

		//	if (string.Compare(strLicenseKey, ApplicationSettings.LicenseKey,
		//		System.StringComparison.InvariantCultureIgnoreCase) == 0)
		//	{
		//		return;
		//	}

		//	throw (new System.Exception("You do not have a valid [License Key] for [Cute RSS]!"));
		//}

		/// <summary>
		/// Get License Key
		/// </summary>
		/// <param name="domain"></param>
		/// <returns></returns>
		internal static string GetKey(string domain)
		{
			string strVersion = "CUTE_1_RSS";

			if (String.IsNullOrEmptyOrWhiteSpace(domain))
			{
				return (string.Empty);
			}

			domain = domain.Replace(" ", string.Empty);

			if (domain == string.Empty)
			{
				return (string.Empty);
			}

			domain = domain.ToLower();

			if (domain.StartsWith("www.",
				comparisonType: System.StringComparison.InvariantCultureIgnoreCase))
			{
				domain = domain.Substring(4);
			}

			int intIndex =
				domain.IndexOf("/",
				comparisonType: System.StringComparison.InvariantCultureIgnoreCase);

			if (intIndex >= 0)
			{
				domain = domain.Substring(0, intIndex);
			}

			string strDomain = string.Empty;

			if (domain.Length == 1)
			{
				strDomain = domain;
			}
			else
			{
				strDomain =
					domain.Substring(1) + domain.Substring(0, 1);
			}

			string strResult = string.Empty;

			strResult =
				Dtx.Security.Hashing.GetMD5(strDomain);

			strResult =
				strResult.Substring(strVersion.Length);

			strResult =
				string.Format("{0}_{1}", strVersion, strResult);

			strResult =
				Dtx.Security.Hashing.GetSha1(strResult);

			return (strResult);
		}
	}
}
