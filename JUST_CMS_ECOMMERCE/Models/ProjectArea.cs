namespace Models
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/05/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "ProjectAreas", Schema = Constants.SCHEMA_GENERAL)]
	public class ProjectArea : BaseAreaControllerAction
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProjectArea>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));
				// **************************************************
				// *** /Define Index(es) ****************************
				// **************************************************

				switch (Models.Utility.DatabaseProvider)
				{
					case Enums.DatabaseProviders.Oracle:
					{
						break;
					}

					case Enums.DatabaseProviders.SqlServer:
					{
						break;
					}

					case Enums.DatabaseProviders.SqlServerCompactEdition:
					{
						Property(current => current.Description)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		#region Static Method(s)
		public static string GetDisplayName(ProjectArea projectArea)
		{
			if (projectArea == null)
			{
				return (string.Empty);
			}

			string strResult = projectArea.DisplayName;

			if (strResult == null)
			{
				strResult = projectArea.Name;
			}
			else
			{
				strResult = strResult.Trim();

				if (strResult == string.Empty)
				{
					strResult = projectArea.Name;
				}
			}

			return (strResult);
		}
		#endregion /Static Method(s)

		public ProjectArea()
		{
		}

		public virtual System.Collections.Generic.IList<ProjectController> ProjectControllers { get; set; }
	}
}
