namespace Models
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/05/22
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		(name: "Messages", Schema = Constants.SCHEMA_GENERAL)]
	public class Message : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Message>
		{
			public Configuration()
			{
				HasRequired(current => current.SenderUser)
					.WithMany(user => user.SentMessages)
					.HasForeignKey(current => current.SenderUserId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.RecipientUser)
					.WithMany(user => user.ReceivedMessages)
					.HasForeignKey(current => current.RecipientUserId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.RelatedMessage)
					.WithMany(message => message.RepliedMessages)
					.HasForeignKey(current => current.RelatedMessageId)
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

						Property(current => current.Subject)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		public Message()
		{
			ReadDateTime = new ComplexTypes.CustomDateTime();
			SendDateTime = new ComplexTypes.CustomRequiredDateTime();
			DeleteDateTimeBySender = new ComplexTypes.CustomDateTime();
			DeleteDateTimeByRecipient = new ComplexTypes.CustomDateTime();
		}

		// **********
		// **********
		// **********
		public virtual User SenderUser { get; set; }
		// **********

		// **********
		public System.Guid SenderUserId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		public virtual User RecipientUser { get; set; }
		// **********

		// **********
		public System.Guid RecipientUserId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		public virtual Message RelatedMessage { get; set; }
		// **********

		// **********
		public System.Guid? RelatedMessageId { get; set; }
		// **********
		// **********
		// **********

		// **********
		public string Subject { get; set; }
		// **********

		// **********
		public string Body { get; set; }
		// **********

		// **********
		public bool IsDraft { get; set; }
		// **********

		// **********
		public ComplexTypes.CustomRequiredDateTime SendDateTime { get; set; }
		// **********

		// **********
		public bool IsRead { get; set; }
		// **********

		// **********
		public ComplexTypes.CustomDateTime ReadDateTime { get; set; }
		// **********

		// **********
		public bool IsDeletedBySender { get; set; }
		// **********

		// **********
		public ComplexTypes.CustomDateTime DeleteDateTimeBySender { get; set; }
		// **********

		// **********
		public bool IsDeletedByRecipient { get; set; }
		// **********

		// **********
		public ComplexTypes.CustomDateTime DeleteDateTimeByRecipient { get; set; }
		// **********

		// **********
		public bool NotifySenderAfterReadingByRecipient { get; set; }
		// **********

		// **********
		public bool NotifySenderAfterDeletingByRecipient { get; set; }
		// **********

		// **********
		public virtual System.Collections.Generic.IList<Message> RepliedMessages { get; set; }
		// **********
	}
}
