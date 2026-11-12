namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.1.3
	/// Update Date: 1393/09/05
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IUnitOfWork
	{
		ITagRepository TagRepository { get; }
		IPageRepository PageRepository { get; }
		IPostRepository PostRepository { get; }
		ILayoutRepository LayoutRepository { get; }
		ISettingsRepository SettingsRepository { get; }
		IPostCategoryRepository PostCategoryRepository { get; }
	}
}
