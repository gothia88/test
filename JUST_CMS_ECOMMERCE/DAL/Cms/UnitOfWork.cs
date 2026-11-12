namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.1.3
	/// Update Date: 1393/09/05
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UnitOfWork : System.Object, IUnitOfWork
	{
		public UnitOfWork(Models.DatabaseContext databaseContext)
		{
			if (databaseContext == null)
			{
				throw (new System.ArgumentNullException("databaseContext"));
			}

			DatabaseContext = databaseContext;
		}

		protected Models.DatabaseContext DatabaseContext { get; set; }

		//private IRepository _Repository;
		//public IRepository Repository
		//{
		//	get
		//	{
		//		if (_Repository == null)
		//		{
		//			_Repository =
		//				new Repository(DatabaseContext);
		//		}
		//		return (_Repository);
		//	}
		//}

		private ITagRepository _tagRepository;
		public ITagRepository TagRepository
		{
			get
			{
				if (_tagRepository == null)
				{
					_tagRepository =
						new TagRepository(DatabaseContext);
				}
				return (_tagRepository);
			}
		}

		private IPageRepository _pageRepository;
		public IPageRepository PageRepository
		{
			get
			{
				if (_pageRepository == null)
				{
					_pageRepository =
						new PageRepository(DatabaseContext);
				}
				return (_pageRepository);
			}
		}

		private IPostRepository _postRepository;
		public IPostRepository PostRepository
		{
			get
			{
				if (_postRepository == null)
				{
					_postRepository =
						new PostRepository(DatabaseContext);
				}
				return (_postRepository);
			}
		}

		private ILayoutRepository _layoutRepository;
		public ILayoutRepository LayoutRepository
		{
			get
			{
				if (_layoutRepository == null)
				{
					_layoutRepository =
						new LayoutRepository(DatabaseContext);
				}
				return (_layoutRepository);
			}
		}

		private ISettingsRepository _settingsRepository;
		public ISettingsRepository SettingsRepository
		{
			get
			{
				if (_settingsRepository == null)
				{
					_settingsRepository =
						new SettingsRepository(DatabaseContext);
				}
				return (_settingsRepository);
			}
		}

		private IPostCategoryRepository _postCategoryRepository;
		public IPostCategoryRepository PostCategoryRepository
		{
			get
			{
				if (_postCategoryRepository == null)
				{
					_postCategoryRepository =
						new PostCategoryRepository(DatabaseContext);
				}
				return (_postCategoryRepository);
			}
		}
	}
}
