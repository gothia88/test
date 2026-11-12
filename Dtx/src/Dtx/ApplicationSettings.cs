namespace Dtx
{
	/// <remarks>
	/// Version: 1.0.2
	/// Update Date: 1392/07/21
	/// Developer: Mr. Dariush Tasdighi
	/// </remarks>
	public static class ApplicationSettings
	{
		// The .NET Framework original read values from
		// System.Configuration.ConfigurationManager.AppSettings.
		//
		// The .NET 10 port reads from a pluggable provider so callers can wire
		// up Microsoft.Extensions.Configuration (or anything else), falling back
		// to environment variables. For example:
		//
		//     Dtx.ApplicationSettings.ValueProvider =
		//         key => configuration.GetValue<string>(key);
		public static System.Func<string, string> ValueProvider { get; set; }

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
				string strData = null;

				if (ValueProvider != null)
				{
					strData = ValueProvider(key);
				}

				if (strData == null)
				{
					strData =
						System.Environment.GetEnvironmentVariable(key);
				}

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
