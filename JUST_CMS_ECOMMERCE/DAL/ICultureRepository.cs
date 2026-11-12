namespace DAL
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/01
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ICultureRepository : IRepository<Models.Culture>
	{
		Models.Culture GetFirst();

		Models.Culture GetByLcid(int lcid);

		System.Linq.IOrderedQueryable<Models.Culture> GetActive();



		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/01
		/// Changed In Version: 1.8.9
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		System.Collections.Generic.List<Models.Culture>
			GetForDropDownList(bool forSiteResponsibles);
	}
}
