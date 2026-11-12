namespace DAL
{
	/// <summary>
	/// Version: 1.1.8
	/// Update Date: 1393/10/24
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IUserRepository : IRepository<Models.User>
	{
		System.Linq.IQueryable<Models.User>
			GetActiveByCultureLcid(int cultureLcid);

		System.Linq.IQueryable<ViewModels.Areas.Common.Rss.IndexViewModel>
			GetActiveForRss(int take);

		Models.User GetByEmailAddress(string emailAddress);

		Models.User GetByNationalCode(string nationalCode);

		Models.User GetByCellPhoneNumber(string cellPhoneNumber);

		Models.User GetByEmailAddressVerificationKey(string verificationKey);

		Models.User GetByCellPhoneNumberVerificationKey(string verificationKey);

		bool CheckDirectAccessForAction(System.Guid userId, System.Guid actionId);

		Models.User GetByEmailAddressExceptId(string emailAddress, System.Guid id);

		Models.User GetByNationalCodeExceptId(string nationalCode, System.Guid id);

		Models.User GetByCellPhoneNumberExceptId(string cellPhoneNumber, System.Guid id);

		//System.Linq.IOrderedQueryable<Models.User> GetActiveByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.User> GetActiveRecipientsByCultureLcid(int cultureLcid);

		//System.Linq.IOrderedQueryable<Models.User> GetActiveForTheOthersByCultureLcid(int cultureLcid);
	}
}
