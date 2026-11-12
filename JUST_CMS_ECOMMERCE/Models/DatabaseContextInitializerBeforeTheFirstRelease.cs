using System.Linq;
using System.Data.Entity;

namespace Models
{
	internal class DatabaseContextInitializerBeforeTheFirstRelease :
		System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
	{
		public DatabaseContextInitializerBeforeTheFirstRelease()
		{
		}

		protected override void Seed(DatabaseContext databaseContext)
		{
			base.Seed(databaseContext);

			try
			{
				DatabaseContextInitializer.Seed(databaseContext);
			}
			catch (System.Exception ex)
			{
				Dtx.LogHandler.Report(GetType(), null, ex);
			}
		}
	}
}
