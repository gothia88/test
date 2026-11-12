namespace Dtx.Enums
{
	/// <summary>
	/// عبارات منظمی که قابل قبول می باشد
	/// </summary>
	public enum RegularExperssions : int
	{
		/// <summary>
		/// تعريف نشده
		/// </summary>
		None = 0,

		/// <summary>
		/// آدرس پست الکترونیکی
		/// </summary>
		/// <example>
		/// Dariush@IranianExperts.ir
		/// </example>
		InternetEmailAddress = 1,

		/// <summary>
		/// آدرس اینترنتی
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir
		/// </example>
		InternetUrl = 2,

		/// <summary>
		/// شماره تلفن
		/// </summary>
		/// <example>
		/// 021-12345678 Or 0912-1234567
		/// </example>
		IRNPhoneNumber = 3,

		/// <summary>
		/// کد پستی
		/// </summary>
		/// <example>
		/// 123-123456-1
		/// </example>
		IRNZIPCode = 4
	}
}
