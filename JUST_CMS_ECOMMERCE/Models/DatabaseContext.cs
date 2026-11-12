namespace Models
{
	/// <summary>
	/// Version: 1.4.3
	/// Update Date: 1394/01/19
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DatabaseContext : System.Data.Entity.DbContext
	{
		static DatabaseContext()
		{
			System.Data.Entity.Database.SetInitializer
				(new DatabaseContextInitializerBeforeTheFirstRelease());

			//System.Data.Entity.Database.SetInitializer
			//	(new System.Data.Entity.MigrateDatabaseToLatestVersion
			//		<DatabaseContext, Migrations.Configuration>());
		}

		public DatabaseContext()
		{
		}

		public System.Data.Entity.DbSet<Role> Roles { get; set; }

		public System.Data.Entity.DbSet<User> Users { get; set; }

		public System.Data.Entity.DbSet<City> Cities { get; set; }

		public System.Data.Entity.DbSet<Group> Groups { get; set; }

		public System.Data.Entity.DbSet<State> States { get; set; }

		public System.Data.Entity.DbSet<Gender> Genders { get; set; }

		public System.Data.Entity.DbSet<Culture> Cultures { get; set; }

		public System.Data.Entity.DbSet<Message> Messages { get; set; }

		public System.Data.Entity.DbSet<Country> Countries { get; set; }

		public System.Data.Entity.DbSet<AccessType> AccessTypes { get; set; }

		public System.Data.Entity.DbSet<HttpReferer> HttpReferers { get; set; }

		public System.Data.Entity.DbSet<ProjectArea> ProjectAreas { get; set; }

		public System.Data.Entity.DbSet<UserAddress> UserAddresses { get; set; }

		public System.Data.Entity.DbSet<UserLoginLog> UserLoginLogs { get; set; }

		public System.Data.Entity.DbSet<MailSettings> MyMailSettings { get; set; }

		public System.Data.Entity.DbSet<ProjectAction> ProjectActions { get; set; }

		public System.Data.Entity.DbSet<ProjectController> ProjectControllers { get; set; }

		public System.Data.Entity.DbSet<ApplicationSettings> MyApplicationSettings { get; set; }

		public System.Data.Entity.DbSet<GlobalApplicationSettings> MyGlobalApplicationSettings { get; set; }



		public System.Data.Entity.DbSet<Common.Tag> Tags { get; set; }




		public System.Data.Entity.DbSet<Cms.Page> Pages { get; set; }

		public System.Data.Entity.DbSet<Cms.Post> Posts { get; set; }

		public System.Data.Entity.DbSet<Cms.Layout> Layouts { get; set; }

		public System.Data.Entity.DbSet<Cms.CmsSettings> CmsSettings { get; set; }

		public System.Data.Entity.DbSet<Cms.PageContent> PageContents { get; set; }

		public System.Data.Entity.DbSet<Cms.PostComment> PostComments { get; set; }

		public System.Data.Entity.DbSet<Cms.PostCategory> PostCategories { get; set; }



		public System.Data.Entity.DbSet<ECommerce.Plan> Plans { get; set; }

		public System.Data.Entity.DbSet<ECommerce.Transaction> Transactions { get; set; }

		public System.Data.Entity.DbSet<ECommerce.ECommerceSettings> ECommerceSettings { get; set; }

		protected override void OnModelCreating
			(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new City.Configuration());
			modelBuilder.Configurations.Add(new Role.Configuration());
			modelBuilder.Configurations.Add(new User.Configuration());
			modelBuilder.Configurations.Add(new Group.Configuration());
			modelBuilder.Configurations.Add(new State.Configuration());
			modelBuilder.Configurations.Add(new Gender.Configuration());
			modelBuilder.Configurations.Add(new Country.Configuration());
			modelBuilder.Configurations.Add(new Culture.Configuration());
			modelBuilder.Configurations.Add(new Message.Configuration());
			modelBuilder.Configurations.Add(new AccessType.Configuration());
			modelBuilder.Configurations.Add(new HttpReferer.Configuration());
			modelBuilder.Configurations.Add(new ProjectArea.Configuration());
			modelBuilder.Configurations.Add(new UserAddress.Configuration());
			modelBuilder.Configurations.Add(new MailSettings.Configuration());
			modelBuilder.Configurations.Add(new UserLoginLog.Configuration());
			modelBuilder.Configurations.Add(new ProjectAction.Configuration());
			modelBuilder.Configurations.Add(new ProjectController.Configuration());
			modelBuilder.Configurations.Add(new ApplicationSettings.Configuration());
			modelBuilder.Configurations.Add(new GlobalApplicationSettings.Configuration());

			modelBuilder.Configurations.Add(new Common.Tag.Configuration());

			modelBuilder.Configurations.Add(new Cms.Page.Configuration());
			modelBuilder.Configurations.Add(new Cms.Post.Configuration());
			modelBuilder.Configurations.Add(new Cms.Layout.Configuration());
			modelBuilder.Configurations.Add(new Cms.CmsSettings.Configuration());
			modelBuilder.Configurations.Add(new Cms.PageContent.Configuration());
			modelBuilder.Configurations.Add(new Cms.PostComment.Configuration());
			modelBuilder.Configurations.Add(new Cms.PostCategory.Configuration());

			modelBuilder.Configurations.Add(new ECommerce.Plan.Configuration());
			modelBuilder.Configurations.Add(new ECommerce.Transaction.Configuration());
			modelBuilder.Configurations.Add(new ECommerce.ECommerceSettings.Configuration());
		}
	}
}
