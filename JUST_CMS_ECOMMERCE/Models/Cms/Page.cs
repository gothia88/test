namespace Models.Cms
{
	/// <summary>
	/// Version: 1.1.9
	/// Update Date: 1393/06/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("Pages", Schema = Constants.SCHEMA_CMS)]
	public class Page : BaseLocalizedPageOrPost, Common.ITag
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Page>
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

				HasOptional(current => current.Layout)
					.WithMany(layout => layout.Pages)
					.HasForeignKey(current => current.LayoutId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.AccessType)
						.WithMany(accessType => accessType.Pages)
						.HasForeignKey(current => current.AccessTypeId)
						.WillCascadeOnDelete(false)
						;

				HasRequired(current => current.CreatorUser)
					.WithMany(user => user.WrittenPages)
					.HasForeignKey(current => current.CreatorUserId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.VerifierUser)
					.WithMany(user => user.VerifiedPages)
					.HasForeignKey(current => current.VerifierUserId)
					.WillCascadeOnDelete(false)
					;

				HasMany(current => current.Groups)
					.WithMany(group => group.Pages)
					.Map(current =>
					{
						current.ToTable("PagesOfGroups", Constants.SCHEMA_CMS);
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("PageId");
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
						Property(current => current.Author)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Keywords)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.Copyright)
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

		public Page()
		{
		}

		// **********
		private string _name;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.FileName,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForFileName)]
		public string Name
		{
			get
			{
				return (_name);
			}
			set
			{
				if (value != null)
				{
					value =
						value.Replace(" ", string.Empty).ToLower();
				}
				_name = value;
			}
		}
		// **********

		public virtual PageContent PageContent { get; set; }

		// دو دستور ذيل مربوط به سطح دسترسی می‌شود
		public virtual System.Collections.Generic.IList<User> Users { get; set; }

		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }

		public virtual System.Collections.Generic.IList<Common.Tag> TagList { get; set; }
	}
}
