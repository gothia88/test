namespace DAL
{
	/// <summary>
	/// Version: 1.0.7
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IGenderRepository : IRepository<Models.Gender>
	{
		System.Linq.IOrderedQueryable<Models.Gender> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Gender> GetActiveByCultureLcid(int cultureLcid);

		Models.Gender
			GetByCultureLcidAndMasterDataCodeExceptId(int cultureLcid, long masterDataCode, System.Guid id);



		/// <summary>
		/// Version: 1.0.7
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		System.Collections.Generic.List<Models.Gender>
			GetForDropDownList(int cultureLcid, bool forSiteResponsibles);
	}
}
