namespace Models
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/05/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "ProjectControllers", Schema = Constants.SCHEMA_GENERAL)]
	public class ProjectController : BaseAreaControllerAction
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProjectController>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.ProjectAreaId)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_ProjectAreaId_Name", 1) { IsUnique = true }));

				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_ProjectAreaId_Name", 2) { IsUnique = true }));
				// **************************************************
				// *** /Define Index(es) ****************************
				// **************************************************

				HasRequired(current => current.ProjectArea)
					.WithMany(projectArea => projectArea.ProjectControllers)
					.HasForeignKey(current => current.ProjectAreaId)
					.WillCascadeOnDelete(false)
					;

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
		public static string GetDisplayName(ProjectController projectController)
		{
			if (projectController == null)
			{
				return (string.Empty);
			}

			string strResult = projectController.DisplayName;

			if (strResult == null)
			{
				strResult = projectController.Name;
			}
			else
			{
				strResult = strResult.Trim();

				if (strResult == string.Empty)
				{
					strResult = projectController.Name;
				}
			}

			return (strResult);
		}
		#endregion /Static Method(s)

		public ProjectController()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ProjectArea),
			Name = Resources.Models.Strings.ProjectAreaKeys.EntityName)]
		public virtual ProjectArea ProjectArea { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ProjectArea),
			Name = Resources.Models.Strings.ProjectAreaKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid ProjectAreaId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public new string Name { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }
	}
}
