namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ICountryRepository : IRepository<Models.Country>
	{
		System.Linq.IOrderedQueryable<Models.Country> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Country> GetActiveByCultureLcid(int cultureLcid);

		Models.Country GetByCultureLcidAndName(int cultureLcid, string name);

		Models.Country
			GetByCultureLcidAndNameExceptId(int cultureLcid, string name, System.Guid id);

		Models.Country
			GetByCultureLcidAndMasterDataCode(int cultureLcid, long masterDataCode);

		Models.Country
			GetByCultureLcidAndMasterDataCodeExceptId(int cultureLcid, long masterDataCode, System.Guid id);
	}
}
