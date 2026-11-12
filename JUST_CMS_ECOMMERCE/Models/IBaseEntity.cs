namespace Models
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/24
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IBaseExtendedEntity
	{
		bool IsActive { get; }
		bool IsSystem { get; }
		bool IsDeleted { get; }
		bool IsVerified { get; }
	}
}
