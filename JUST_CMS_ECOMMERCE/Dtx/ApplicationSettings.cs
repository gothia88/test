namespace Dtx
{
	/// <remarks>
	/// Version: 1.0.2
	/// Update Date: 1392/07/21
	/// Developer: Mr. Dariush Tasdighi
	/// </remarks>
	public static class ApplicationSettings
	{
		public static string GetValue(string key)
		{
			return (GetValue(key, string.Empty));
		}

		public static string GetValue(string key, string defaultValue)
		{
			if (defaultValue == null)
			{
				defaultValue = string.Empty;
			}
			else
			{
				defaultValue = defaultValue.Trim();
			}

			if (key == null)
			{
				return (defaultValue);
			}

			key = key.Trim();
			if (key == string.Empty)
			{
				return (defaultValue);
			}

			try
			{
				string strData =
					System.Configuration.ConfigurationManager.AppSettings[key];

				if (string.IsNullOrEmpty(strData))
				{
					return (defaultValue);
				}

				return (strData.Trim());
			}
			catch
			{
				return (defaultValue);
			}
		}
	}
}
