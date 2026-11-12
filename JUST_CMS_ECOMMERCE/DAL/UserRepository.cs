using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.1.8
	/// Update Date: 1393/10/24
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UserRepository : Repository<Models.User>, IUserRepository
	{
		public UserRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IQueryable<Models.User>
			GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsProfilePublic)
				.Where(current => current.IsEmailAddressVerified)

				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsAccountTerminated == false)

				.Where(current => current.CultureLcid == cultureLcid)
				;

			return (varEntities);
		}

		public System.Linq.IQueryable<ViewModels.Areas.Common.Rss.IndexViewModel>
			GetActiveForRss(int take)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)

				.Where(current => current.IsProfilePublic)
				.Where(current => current.IsEmailAddressVerified)
				.Where(current => current.IsAccountTerminated == false)

				.OrderByDescending(current => current.LastLoginDateTime.Value)

				.Skip(0)
				.Take(take)

				.Select(current => new ViewModels.Areas.Common.Rss.IndexViewModel
				{
					Id = current.Id,

					Description = current.JobTitle,
					UpdateDateTime = current.UpdateDateTime,

					Title =
						current.Gender.Name2 + " " + current.FirstName + " " + current.LastName,

					CreatorUserLastName = current.CreatorUser.LastName,
					CreatorUserFirstName = current.CreatorUser.FirstName,
					CreatorUserGender = current.CreatorUser.Gender.Name2,
					CreatorUserEmailAddress = current.CreatorUser.EmailAddress,
				})
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.User> GetActiveRecipientsByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsDirectContactable)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.GenderId)
				.ThenBy(current => current.FirstName)
				.ThenBy(current => current.LastName)
				;

			return (varEntities);
		}

		public Models.User GetByEmailAddress(string emailAddress)
		{
			Models.User oUser =
				Get()
				.Where(current => string.Compare(current.EmailAddress, emailAddress,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByCellPhoneNumber(string cellPhoneNumber)
		{
			Models.User oUser =
				Get()
				.Where(current =>
					string.Compare(current.CellPhoneNumber, cellPhoneNumber,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByNationalCode(string nationalCode)
		{
			Models.User oUser =
				Get()
				.Where(current =>
					string.Compare(current.NationalCode, nationalCode,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByEmailAddressVerificationKey(string verificationKey)
		{
			System.Guid sVerificationKey =
				new System.Guid(verificationKey);

			Models.User oUser =
				Get()
				.Where(current => current.EmailAddressVerificationKey == sVerificationKey)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByCellPhoneNumberVerificationKey(string verificationKey)
		{
			verificationKey =
				verificationKey.Replace("o", "0").Replace("O", "0");

			Models.User oUser =
				Get()
				.Where(current =>
					string.Compare(current.CellPhoneNumberVerificationKey, verificationKey,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public bool CheckDirectAccessForAction(System.Guid userId, System.Guid actionId)
		{
			int intCount =
				Get()
				.Where(current => current.Id == userId)
				.Where(current => current.ProjectActions.Any(action => action.Id == actionId))
				.Count();

			if (intCount == 0)
			{
				return (false);
			}
			else
			{
				return (true);
			}
		}

		public Models.User GetByEmailAddressExceptId(string emailAddress, System.Guid id)
		{
			Models.User oUser =
				Get()
				.Where(current => current.Id != id)
				.Where(current =>
					string.Compare(current.EmailAddress, emailAddress,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByNationalCodeExceptId(string nationalCode, System.Guid id)
		{
			Models.User oUser =
				Get()

				.Where(current => current.Id != id)
				.Where(current =>
					string.Compare(current.NationalCode, nationalCode,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)

				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByCellPhoneNumberExceptId(string cellPhoneNumber, System.Guid id)
		{
			Models.User oUser =
				Get()

				.Where(current => current.Id != id)
				.Where(current =>
					string.Compare(current.CellPhoneNumber, cellPhoneNumber, true) == 0)

				.FirstOrDefault();

			return (oUser);
		}
	}
}
