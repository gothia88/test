//using System.Linq;
//using System.Data.Entity;

//namespace Models.Migrations
//{
//	internal sealed class Configuration :
//		System.Data.Entity.Migrations.DbMigrationsConfiguration<DatabaseContext>
//	{
//		public Configuration()
//		{
//			ContextKey = "Models.DatabaseContext";

//			AutomaticMigrationsEnabled = false;
//			AutomaticMigrationDataLossAllowed = false;
//		}

//		protected override void Seed(DatabaseContext databaseContext)
//		{
//			base.Seed(databaseContext);

//			if (databaseContext.Users.Count() != 0)
//			{
//				return;
//			}

//			try
//			{
//				DatabaseContextInitializer.Seed(databaseContext);
//			}
//			catch (System.Exception ex)
//			{
//				Dtx.LogHandler.Report(GetType(), null, ex);
//			}
//		}
//	}
//}
