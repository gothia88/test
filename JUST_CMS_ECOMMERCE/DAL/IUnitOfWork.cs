namespace DAL
{
	/// <summary>
	/// Version: 1.3.4
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IUnitOfWork : System.IDisposable
	{
		// **************************************************
		Cms.IUnitOfWork CmsUnitOfWork { get; }

		ECommerce.IUnitOfWork ECommerceUnitOfWork { get; }
		// **************************************************

		ICityRepository CityRepository { get; }

		IRoleRepository RoleRepository { get; }

		IUserRepository UserRepository { get; }

		IGroupRepository GroupRepository { get; }

		IStateRepository StateRepository { get; }

		IGenderRepository GenderRepository { get; }

		ICountryRepository CountryRepository { get; }

		ICultureRepository CultureRepository { get; }

		IAccessTypeRepository AccessTypeRepository { get; }

		IHttpRefererRepository HttpRefererRepository { get; }

		IProjectAreaRepository ProjectAreaRepository { get; }

		IUserAddressRepository UserAddressRepository { get; }

		IMailSettingsRepository MailSettingsRepository { get; }

		IUserLoginLogRepository UserLoginLogRepository { get; }

		IProjectActionRepository ProjectActionRepository { get; }

		IProjectControllerRepository ProjectControllerRepository { get; }

		IApplicationSettingsRepository ApplicationSettingsRepository { get; }

		IGlobalApplicationSettingsRepository GlobalApplicationSettingsRepository { get; }

		void Save();
	}
}
