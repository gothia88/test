namespace DAL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ICityRepository : IRepository<Models.City>
	{
		System.Linq.IOrderedQueryable<Models.City> GetByStateId(System.Guid stateId);

		System.Linq.IOrderedQueryable<Models.City> GetActiveByStateId(System.Guid stateId);

		Models.City GetByStateIdAndName(System.Guid stateId, string name);

		Models.City
			GetByStateIdAndNameExceptId(System.Guid stateId, string name, System.Guid id);

		Models.City
			GetByStateIdAndMasterDataCode(System.Guid stateId, long masterDataCode);

		Models.City
			GetByStateIdAndMasterDataCodeExceptId(System.Guid stateId, long masterDataCode, System.Guid id);
	}
}
