namespace Models.Cms
{
	/// <summary>
	/// Version: 1.2.0
	/// Update Date: 1394/01/26
	/// Changed In Version: 1.8.9
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("Posts", Schema = Constants.SCHEMA_CMS)]
	public class Post : BaseLocalizedPageOrPost, Common.ITag, Common.IFileContainer
	{
		#region Configuration
		public class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Post>
		{
			public Configuration()
			{
				HasOptional(current => current.Layout)
					.WithMany(layout => layout.Posts)
					.HasForeignKey(current => current.LayoutId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.CreatorUser)
					.WithMany(user => user.WrittenPosts)
					.HasForeignKey(current => current.CreatorUserId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.PostCategory)
					.WithMany(postCategory => postCategory.Posts)
					.HasForeignKey(current => current.PostCategoryId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.VerifierUser)
					.WithMany(user => user.VerifiedPosts)
					.HasForeignKey(current => current.VerifierUserId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.AccessType)
						.WithMany(accessType => accessType.Posts)
						.HasForeignKey(current => current.AccessTypeId)
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
						Property(current => current.Body)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Author)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.ImageUrl)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Keywords)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Password)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Copyright)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Introduction)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Classification)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		public Post()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.PostCategory),
			Name = Resources.Models.Cms.Strings.PostCategoryKeys.EntityName)]
		public virtual PostCategory PostCategory { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.PostCategory),
			Name = Resources.Models.Cms.Strings.PostCategoryKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid PostCategoryId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.IsFeatured)]
		public bool IsFeatured { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.HasAttachment)]
		public bool HasAttachment { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Post),
			Name = Resources.Models.Cms.Strings.PostKeys.DisplayCreatorUser)]
		public bool DisplayCreatorUser { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: Constants.FILE_EXTENSION_LENGTH,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string AttachmentExtension { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.AttachmentLength)]
		public long AttachmentLength { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Sha1OfAttachment)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			MinimumLength = 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Sha1OfAttachment { get; set; }
		// **********

		// **********
		public string FileName
		{
			get
			{
				string strResult = string.Empty;

				if (HasAttachment)
				{
					strResult =
						string.Format("{0}{1}", Id, AttachmentExtension);
				}

				return (strResult);
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.DownloadCount)]
		public int DownloadCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Post),
			Name = Resources.Models.Cms.Strings.PostKeys.IsCommentingEnabled)]
		public bool IsCommentingEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Post),
			Name = Resources.Models.Cms.Strings.PostKeys.Password)]
		public string Password { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Post),
			Name = Resources.Models.Cms.Strings.PostKeys.Introduction)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Introduction { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.ImageUrl)]

		//[System.ComponentModel.DataAnnotations.RegularExpression
		//	(Dtx.Text.RegularExpressions.Patterns.Url,
		//	ErrorMessageResourceType = typeof(Resources.Messages),
		//	ErrorMessageResourceName =
		//	Resources.Strings.MessagesKeys.RegularExpressionForUrl)]

		//[System.ComponentModel.DataAnnotations.DataType
		//	(System.ComponentModel.DataAnnotations.DataType.Url)]
		public string ImageUrl { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.Post),
			Name = Resources.Models.Cms.Strings.PostKeys.Body)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Body { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Common.Tag> TagList { get; set; }

		public virtual System.Collections.Generic.IList<PostComment> PostComments { get; set; }
	}
}
