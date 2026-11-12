namespace Models
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/06/01
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "ProjectActions", Schema = Constants.SCHEMA_GENERAL)]
	public class ProjectAction : BaseAreaControllerAction
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProjectAction>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.ProjectControllerId)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_ProjectControllerId_Name", 1) { IsUnique = true }));

				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_ProjectControllerId_Name", 2) { IsUnique = true }));
				// **************************************************
				// *** /Define Index(es) ****************************
				// **************************************************

				HasOptional(current => current.Layout)
					.WithMany(layout => layout.ProjectActions)
					.HasForeignKey(current => current.LayoutId)
					.WillCascadeOnDelete(true)
					;

				HasRequired(current => current.ProjectController)
					.WithMany(projectController => projectController.ProjectActions)
					.HasForeignKey(current => current.ProjectControllerId)
					.WillCascadeOnDelete(true)
					;

				HasMany(current => current.Groups)
					.WithMany(group => group.ProjectActions)
					.Map(current =>
					{
						current.ToTable("ProjectActionsOfGroups");
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("ProjectActionId");
						current.MapRightKey("GroupId");
					});

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
		public static string GetDisplayName(ProjectAction projectAction)
		{
			if (projectAction == null)
			{
				return (string.Empty);
			}

			string strResult = projectAction.DisplayName;

			if (strResult == null)
			{
				strResult = projectAction.Name;
			}
			else
			{
				strResult = strResult.Trim();

				if (strResult == string.Empty)
				{
					strResult = projectAction.Name;
				}
			}

			return (strResult);
		}
		#endregion /Static Method(s)

		public ProjectAction()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ProjectController),
			Name = Resources.Models.Strings.ProjectControllerKeys.EntityName)]
		public virtual ProjectController ProjectController { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ProjectController),
			Name = Resources.Models.Strings.ProjectControllerKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid ProjectControllerId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Layout),
			Name = Resources.Models.Cms.Strings.LayoutKeys.EntityName)]
		public virtual Cms.Layout Layout { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Layout),
			Name = Resources.Models.Cms.Strings.LayoutKeys.EntityName)]

		public System.Guid? LayoutId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.AccessType),
			Name = Resources.Models.Strings.AccessTypeKeys.EntityName)]
		public virtual AccessType AccessType { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.AccessType),
			Name = Resources.Models.Strings.AccessTypeKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid AccessTypeId { get; set; }
		// **********

		// **********
		public Enums.AccessTypes AccessTypeEnum
		{
			get
			{
				if (AccessType == null)
				{
					return (Enums.AccessTypes.Registered);
				}
				else
				{
					return ((Enums.AccessTypes)AccessType.Code);
				}
			}
		}
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

		public virtual System.Collections.Generic.IList<User> Users { get; set; }

		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }
	}
}
