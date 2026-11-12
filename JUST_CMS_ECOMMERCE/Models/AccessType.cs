using System.Linq;
using System.Data.Entity;

namespace Models
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/06/01
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "AccessTypes", Schema = Constants.SCHEMA_GENERAL)]
	public class AccessType : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccessType>
		{
			public Configuration()
			{
				// **************************************************
				// *** Define Index(es) *****************************
				// **************************************************
				Property(current => current.CultureLcid)
					.HasColumnAnnotation("Index",
						new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation(new[]
						{
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Code", 1) { IsUnique = true },
							new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
								("IX_CultureLcid_Name", 1) { IsUnique = true },
						}));
				// **************************************************

				// **************************************************
				Property(current => current.Code)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Code", 2) { IsUnique = true }));

				Property(current => current.Name)
					.HasColumnAnnotation("Index",
					new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
						(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute
							("IX_CultureLcid_Name", 2) { IsUnique = true }));
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

		public static AccessType GetByCultureAndCode(int cultureLcid, Enums.AccessTypes code)
		{
			AccessType oResult = null;
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext =
					new DatabaseContext();

				oResult =
					oDatabaseContext.AccessTypes
					.Where(current => current.Code == (int)code)
					.Where(current => current.CultureLcid == cultureLcid)
					.FirstOrDefault();
			}
			catch
			{
			}
			finally
			{
				if (oDatabaseContext != null)
				{
					oDatabaseContext.Dispose();
					oDatabaseContext = null;
				}
			}

			return (oResult);
		}

		public AccessType()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Code)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.ZeroOrUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForZeroOrUnsignedInteger)]
		public int Code { get; set; }
		// **********

		// **********
		public Enums.AccessTypes CodeEnum
		{
			get
			{
				return ((Enums.AccessTypes)Code);
			}
		}
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
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
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

		public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Post> Posts { get; set; }

		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }
	}
}
