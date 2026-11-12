namespace Dtx.Rss
{
	/// <summary>
	/// Application Settings
	/// </summary>
	/// <remarks>
	/// Version: 1.0.4
	/// Update Date: 1392/08/25
	/// Developer: Mr. Dariush Tasdighi
	/// </remarks>
	public static class ApplicationSettings
	{
		/// <summary>
		/// 
		/// </summary>
		static ApplicationSettings()
		{
			LicenseKey =
				Dtx.ApplicationSettings.GetValue("LicenseKeyForCuteRss");
		}

		/// <summary>
		/// 
		/// </summary>
		public static string LicenseKey { get; set; }
	}
}
