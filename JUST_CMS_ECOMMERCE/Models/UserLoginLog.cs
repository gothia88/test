namespace Models
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/10/16
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "UserLoginLogs", Schema = Constants.SCHEMA_GENERAL)]
	public class UserLoginLog : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserLoginLog>
		{
			public Configuration()
			{
				HasRequired(current => current.User)
					.WithMany(user => user.UserLoginLogs)
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
						break;
					}
				}
			}
		}
		#endregion /Configuration

		public UserLoginLog()
		{
			LogoutDateTime = new ComplexTypes.CustomDateTime();
			LoginDateTime = new ComplexTypes.CustomRequiredDateTime();
		}

		// **********
		// **********
		// **********
		public System.Guid UserId { get; protected internal set; }
		// **********

		// **********
		public virtual User User { get; protected internal set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.IsHidden)]
		public bool IsHidden { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.SessionId)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string SessionId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.IP)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string IP { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.LoginDateTime)]
		public ComplexTypes.CustomRequiredDateTime LoginDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.UserLoginLog),
			Name = Resources.Models.Strings.UserLoginLogKeys.LogoutDateTime)]
		public ComplexTypes.CustomDateTime LogoutDateTime { get; set; }
		// **********
	}
}
