using System.Linq;

namespace MY_WEB_APPLICATION.Areas.ECommerce.BLL
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1394/02/18
	/// Changed In Version: 1.9.3
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class User
	{
		static User()
		{
		}

		/// <summary>
		/// Version: 1.0.5
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public static CreditAndDebitSumery GetUserCreditAndDebitSumery(System.Guid userId)
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork =
					new DAL.UnitOfWork();

				decimal? decCredit =
					oUnitOfWork.ECommerceUnitOfWork.TransactionRepository
					.Get()
					.Where(current => current.UserId == userId)
					.Where(current => current.IsPending == false)
					.Sum(current => current.Credit);

				decimal? decDebit =
					oUnitOfWork.ECommerceUnitOfWork.TransactionRepository
					.Get()
					.Where(current => current.UserId == userId)
					.Where(current => current.IsPending == false)
					.Sum(current => current.Debit);

				CreditAndDebitSumery oCreditAndDebitSumery =
					new CreditAndDebitSumery();

				if (decDebit.HasValue)
				{
					oCreditAndDebitSumery.Debit = decDebit.Value;
				}

				if (decCredit.HasValue)
				{
					oCreditAndDebitSumery.Credit = decCredit.Value;
				}

				return (oCreditAndDebitSumery);
			}
			catch //(System.Exception ex)
			{
				//Dtx.LogHandler.Report(typeof(Transaction), null, ex);

				return (new CreditAndDebitSumery());
			}
			finally
			{
				if (oUnitOfWork != null)
				{
					oUnitOfWork.Dispose();
					oUnitOfWork = null;
				}
			}
		}
	}
}
