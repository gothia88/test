namespace Models.Common
{
	/// <summary>
	/// Version: 1.0.8
	/// Update Date: 1393/08/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("Tags", Schema = Constants.SCHEMA_COMMON)]
	public class Tag : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Tag>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.CultureLcid)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name", 1) { IsUnique = true }));

				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name", 2) { IsUnique = true }));
				// **************************************************
				// *** /Define Index(es) ****************************
				// **************************************************

				HasOptional(current => current.VerifierUser)
					.WithMany(user => user.VerifiedTags)
					.HasForeignKey(current => current.VerifierUserId)
					.WillCascadeOnDelete(false)
					;

				HasMany(current => current.Pages)
				.WithMany(page => page.TagList)
				.Map(current =>
				{
					current.ToTable("TagsOfPages", Constants.SCHEMA_CMS);
					// MapRightKey را می نويسيم و بعد MapLeftKey اول
					// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
					current.MapLeftKey("TagId");
					current.MapRightKey("PageId");
				});

				HasMany(current => current.Posts)
				.WithMany(post => post.TagList)
				.Map(current =>
				{
					current.ToTable("TagsOfPosts", Constants.SCHEMA_CMS);
					// MapRightKey را می نويسيم و بعد MapLeftKey اول
					// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
					current.MapLeftKey("TagId");
					current.MapRightKey("PostId");
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
						break;
					}
				}
			}
		}
		#endregion /Configuration

		public Tag()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Post> Posts { get; set; }
	}
}
