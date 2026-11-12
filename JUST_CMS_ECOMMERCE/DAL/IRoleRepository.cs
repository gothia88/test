namespace DAL
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IRoleRepository : IRepository<Models.Role>
	{
		Models.Role GetByCultureLcidAndCode(int cultureLcid, int code);

		System.Linq.IOrderedQueryable<Models.Role>
			GetByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code);

		System.Linq.IOrderedQueryable<Models.Role>
			GetActiveByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code);



		/// <summary>
		/// Version: 1.0.7
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		System.Collections.Generic.List<Models.Role>
			GetForDropDownList(int cultureLcid, bool forSiteResponsibles, Models.Enums.Roles code);
	}
}
