namespace Models.ECommerce
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1394/02/09
	/// Changed In Version: 1.9.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table
		("Transactions", Schema = Constants.SCHEMA_ECOMMERCE)]
	public class Transaction : BaseEntity
	{
		#region Configuration
		/// <summary>
		/// Version: 1.0.4
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Transaction>
		{
			public Configuration()
			{
				Property(current => current.Debit)
					.HasPrecision(precision: 18, scale: 2);

				Property(current => current.Credit)
					.HasPrecision(precision: 18, scale: 2);

				Property(current => current.SiteDebit)
					.HasPrecision(precision: 18, scale: 2);

				Property(current => current.SiteCredit)
					.HasPrecision(precision: 18, scale: 2);

				HasOptional(current => current.User)
					.WithMany(user => user.Transactions)
					.HasForeignKey(current => current.UserId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.Plan)
					.WithMany(plan => plan.Transactions)
					.HasForeignKey(current => current.PlanId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.ParentTransaction)
					.WithMany(transaction => transaction.Children)
					.HasForeignKey(current => current.ParentTransactionId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.CreatorOrModifierUser)
					.WithMany(user => user.CreatorOrModifierTransactions)
					.HasForeignKey(current => current.CreatorOrModifierUserId)
					.WillCascadeOnDelete(false)
					;

				switch (Models.Utility.DatabaseProvider)
				{
					case Models.Enums.DatabaseProviders.Oracle:
					{
						break;
					}

					case Models.Enums.DatabaseProviders.SqlServer:
					{
						break;
					}

					case Models.Enums.DatabaseProviders.SqlServerCompactEdition:
					{
						Property(current => current.Description)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						Property(current => current.AdministratorDescription)
							.HasColumnType("ntext")
							.IsMaxLength()
							;

						break;
					}
				}
			}
		}
		#endregion /Configuration

		public Transaction()
		{
			InsertDateTime =
				new ComplexTypes.CustomRequiredDateTime();

			UpdateDateTime =
				new ComplexTypes.CustomRequiredDateTime();
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.UserId)]
		public virtual User User { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.UserId)]
		public System.Guid? UserId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.PlanId)]
		public virtual Plan Plan { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.PlanId)]
		public System.Guid? PlanId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		/// <summary>
		/// Version: 1.0.4
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.ParentTransactionId)]
		public virtual Transaction ParentTransaction { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.0.4
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.ParentTransactionId)]
		public System.Guid? ParentTransactionId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.CreatorOrModifierUserId)]
		public virtual User CreatorOrModifierUser { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.CreatorOrModifierUserId)]
		public System.Guid? CreatorOrModifierUserId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.Debit)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Money,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForMoney)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public decimal Debit { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.Credit)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Money,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForMoney)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public decimal Credit { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.SiteDebit)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Money,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForMoney)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public decimal SiteDebit { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.SiteCredit)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Dtx.Text.RegularExpressions.Patterns.Money,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForMoney)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public decimal SiteCredit { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.IsPending)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public bool IsPending { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.IsPaidOnline)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public bool IsPaidOnline { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.MerchantIdentityGet)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public string MerchantIdentityGet { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.ECommerce.Transaction),
			Name = Resources.Models.ECommerce.Strings.TransactionKeys.MerchantTransactionIdentity)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public string MerchantTransactionIdentity { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.User),
			Name = Resources.Models.Strings.UserKeys.AdministratorDescription)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string AdministratorDescription { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.InsertDateTime)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public ComplexTypes.CustomRequiredDateTime InsertDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Models.General),
			Name = Resources.Models.Strings.GeneralKeys.UpdateDateTime)]

		[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
		public ComplexTypes.CustomRequiredDateTime UpdateDateTime { get; set; }
		// **********

		// **********
		/// <summary>
		/// Version: 1.0.4
		/// Update Date: 1394/02/09
		/// Changed In Version: 1.9.1
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public virtual System.Collections.Generic.IList<ECommerce.Transaction> Children { get; set; }
		// **********
	}
}
