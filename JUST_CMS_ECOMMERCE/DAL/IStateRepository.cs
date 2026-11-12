namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IStateRepository : IRepository<Models.State>
	{
		System.Linq.IOrderedQueryable<Models.State> GetByCountryId(System.Guid countryId);

		System.Linq.IOrderedQueryable<Models.State> GetActiveByCountryId(System.Guid countryId);

		Models.State GetByCountryIdAndName(System.Guid countryId, string name);

		Models.State
			GetByCountryIdAndNameExceptId(System.Guid countryId, string name, System.Guid id);

		Models.State
			GetByCountryIdAndMasterDataCode(System.Guid countryId, long masterDataCode);

		Models.State
			GetByCountryIdAndMasterDataCodeExceptId(System.Guid countryId, long masterDataCode, System.Guid id);
	}
}
