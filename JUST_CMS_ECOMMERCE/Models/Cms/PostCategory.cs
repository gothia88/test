namespace Models.Cms
{
	/// <summary>
	/// Version: 1.1.1
	/// Update Date: 1393/10/03
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("PostCategories", Schema = Constants.SCHEMA_CMS)]
	public class PostCategory : BaseLocalizedEntity
	{
		#region Configuration
		public class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PostCategory>
		{
			public Configuration()
			{
				HasOptional(current => current.ParentPostCategory)
					.WithMany(postCategory => postCategory.Children)
					.HasForeignKey(current => current.ParentPostCategoryId)
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
						Property(current => current.ImageUrl)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

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

		public PostCategory()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.PostCategory),
			Name = Resources.Models.Cms.Strings.PostCategoryKeys.EntityName)]
		public virtual PostCategory ParentPostCategory { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.Cms.PostCategory),
			Name = Resources.Models.Cms.Strings.PostCategoryKeys.EntityName)]
		public System.Guid? ParentPostCategoryId { get; set; }
		// **********
		// **********
		// **********
		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 200,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		// **********
		public string FullName
		{
			get
			{
				string strResult = string.Empty;

				PostCategory oCurrent = this;

				while (oCurrent != null)
				{
					if (strResult == string.Empty)
					{
						strResult = oCurrent.Name;
					}
					else
					{
						strResult =
							oCurrent.Name + " -> " + strResult;
					}

					oCurrent = oCurrent.ParentPostCategory;
				}

				return (strResult);
			}
		}
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
			Name = Resources.Models.Strings.GeneralKeys.Hits)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int Hits { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.ImageUrl)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Url,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Url)]
		public string ImageUrl { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Post> Posts { get; set; }

		public virtual System.Collections.Generic.IList<PostCategory> Children { get; set; }
	}
}
