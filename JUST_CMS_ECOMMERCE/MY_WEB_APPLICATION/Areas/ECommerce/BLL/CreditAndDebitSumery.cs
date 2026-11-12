namespace MY_WEB_APPLICATION.Areas.ECommerce.BLL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1394/02/18
	/// Changed In Version: 1.9.3
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CreditAndDebitSumery : System.Object
	{
		public CreditAndDebitSumery()
		{
		}

		public decimal Debit { get; set; }

		/// <summary>
		/// Version: 1.0.1
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string DisplayDebit
		{
			get
			{
				string strResult = string.Empty;

				if (Debit >= 0)
				{
					strResult =
						string.Format("<span>{0}</span>",
						Dtx.Text.Convert.DisplayFormattedNumber(Debit));
				}
				else
				{
					strResult =
						string.Format("<span style='color: Red'>{0} (منفی)</span>",
						Dtx.Text.Convert.DisplayFormattedNumber(-Debit));
				}

				return (strResult);
			}
		}

		/// <summary>
		/// Version: 1.0.1
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string DisplayDebitInWord
		{
			get
			{
				string strResult = string.Empty;

				if (Debit >= 0)
				{
					strResult =
						string.Format("<span>{0}</span>",
						Dtx.Text.Convert.NumberToWord((long)Debit));
				}
				else
				{
					long lngDebit = -1 * (long)Debit;

					strResult =
						string.Format("<span style='color: Red'>منفی {0}</span>",
						Dtx.Text.Convert.NumberToWord(lngDebit));
				}

				return (strResult);
			}
		}

		public decimal Credit { get; set; }

		/// <summary>
		/// Version: 1.0.1
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string DisplayCredit
		{
			get
			{
				string strResult = string.Empty;

				if (Credit >= 0)
				{
					strResult =
						string.Format("<span>{0}</span>",
						Dtx.Text.Convert.DisplayFormattedNumber(Credit));
				}
				else
				{
					strResult =
						string.Format("<span style='color: Red'>{0} (منفی)</span>",
						Dtx.Text.Convert.DisplayFormattedNumber(-Credit));
				}

				return (strResult);
			}
		}

		/// <summary>
		/// Version: 1.0.1
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string DisplayCreditInWord
		{
			get
			{
				string strResult = string.Empty;

				if (Credit >= 0)
				{
					strResult =
						string.Format("<span>{0}<span>",
						Dtx.Text.Convert.NumberToWord((long)Credit));
				}
				else
				{
					long lngCredit = -1 * (long)Credit;

					strResult =
						string.Format("<span style='color: Red'>منفی {0}</span>",
						Dtx.Text.Convert.NumberToWord(lngCredit));
				}

				return (strResult);
			}
		}

		public decimal Balance
		{
			get
			{
				return (Credit - Debit);
			}
		}

		/// <summary>
		/// Version: 1.0.1
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string DisplayBalance
		{
			get
			{
				string strResult = string.Empty;

				if (Balance >= 0)
				{
					strResult =
						string.Format("<span>{0}</span>",
						Dtx.Text.Convert.DisplayFormattedNumber(Balance));
				}
				else
				{
					strResult =
						string.Format("<span style='color: Red'>{0} (منفی)</span>",
						Dtx.Text.Convert.DisplayFormattedNumber(-Balance));
				}

				return (strResult);
			}
		}

		/// <summary>
		/// Version: 1.0.1
		/// Update Date: 1394/02/18
		/// Changed In Version: 1.9.3
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public string DisplayBalanceInWord
		{
			get
			{
				string strResult = string.Empty;

				if (Balance >= 0)
				{
					strResult =
						string.Format("<span>{0}</span>",
						Dtx.Text.Convert.NumberToWord((long)Balance));
				}
				else
				{
					long lngBalance = -1 * (long)Balance;

					strResult =
						string.Format("<span style='color: Red'>منفی {0}</span>",
						Dtx.Text.Convert.NumberToWord(lngBalance));
				}

				return (strResult);
			}
		}
	}
}
