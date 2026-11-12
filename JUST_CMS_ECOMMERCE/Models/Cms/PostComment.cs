namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/10/16
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("PostComments", Schema = Constants.SCHEMA_CMS)]
	public class PostComment : Common.BaseComment
	{
		#region Configuration
		public class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PostComment>
		{
			public Configuration()
			{
				HasRequired(current => current.Post)
					.WithMany(post => post.PostComments)
					.HasForeignKey(current => current.PostId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.User)
					.WithMany(user => user.PostComments)
					.HasForeignKey(current => current.UserId)
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
						Property(current => current.UserIP)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Subject)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.UserUrl)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Description)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.UserFullName)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.UserEmailAddress)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		public PostComment()
		{
		}

		// **********
		// **********
		// **********
		public Post Post { get; set; }

		public System.Guid PostId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		public User User { get; set; }

		public System.Guid? UserId { get; set; }
		// **********
		// **********
		// **********
	}
}
