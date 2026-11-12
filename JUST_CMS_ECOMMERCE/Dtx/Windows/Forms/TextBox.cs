namespace Dtx.Windows.Forms
{
	public class TextBox : System.Windows.Forms.TextBox
	{
		public TextBox()
		{
			Required = false;

			Caption = "";
			RelativeLabel = null;

			InputType = Enums.InputTypes.Text;
			InputLanguage = Enums.InputLanguages.Farsi;
		}

		private System.Windows.Forms.ErrorProvider _myErrorProvider = null;
		protected virtual System.Windows.Forms.ErrorProvider MyErrorProvider
		{
			get
			{
				if (_myErrorProvider == null)
				{
					_myErrorProvider = new System.Windows.Forms.ErrorProvider();
				}
				return (_myErrorProvider);
			}
		}

		private string _caption;
		[System.ComponentModel.DefaultValue("")]
		public string Caption
		{
			get
			{
				if (DesignMode)
				{
					return (_caption);
				}
				else
				{
					if (string.IsNullOrEmpty(_caption))
					{
						if (RelativeLabel == null)
						{
							return (string.Empty);
						}
						else
						{
							if (RelativeLabel.Text == null)
							{
								return (string.Empty);
							}
							else
							{
								return (RelativeLabel.Text);
							}
						}
					}
					else
					{
						return (_caption);
					}
				}
			}
			set
			{
				_caption = value;
			}
		}

		[System.ComponentModel.DefaultValue(null)]
		public System.Windows.Forms.Label RelativeLabel { get; set; }

		System.Globalization.CultureInfo _faCultureInfo = new System.Globalization.CultureInfo("fa-IR", false);
		System.Globalization.CultureInfo _enCultureInfo = new System.Globalization.CultureInfo("en-US", false);

		private bool _requird;
		[System.ComponentModel.DefaultValue(false)]
		public bool Required
		{
			get
			{
				return (_requird);
			}
			set
			{
				_requird = value;
			}
		}

		private Enums.InputTypes _inputType;
		[System.ComponentModel.DefaultValue(Enums.InputTypes.Text)]
		public Enums.InputTypes InputType
		{
			get
			{
				return (_inputType);
			}
			set
			{
				_inputType = value;
			}
		}

		private Enums.RegularExperssions _rgularExperssion;
		[System.ComponentModel.DefaultValue(Enums.RegularExperssions.None)]
		public Enums.RegularExperssions RegularExperssion
		{
			get
			{
				return (_rgularExperssion);
			}
			set
			{
				_rgularExperssion = value;
			}
		}

		private Enums.InputLanguages _inputLanguage;
		[System.ComponentModel.DefaultValue(Enums.InputLanguages.Farsi)]
		public Enums.InputLanguages InputLanguage
		{
			get
			{
				return (_inputLanguage);
			}
			set
			{
				_inputLanguage = value;

				switch (_inputLanguage)
				{
					case Enums.InputLanguages.Farsi:
					{
						MyErrorProvider.RightToLeft = true;
						RightToLeft = System.Windows.Forms.RightToLeft.Yes;
						break;
					}

					case Enums.InputLanguages.English:
					{
						MyErrorProvider.RightToLeft = false;
						RightToLeft = System.Windows.Forms.RightToLeft.No;
						break;
					}
				}
			}
		}

		[System.ComponentModel.ReadOnly(true)]
		public override System.Windows.Forms.RightToLeft RightToLeft
		{
			get
			{
				return (base.RightToLeft);
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
		{
			base.OnValidating(e);

			if (Required)
			{
				if (Text == "")
				{
					if (Caption == "")
					{
						string strMessage = string.Format("فيلد مربوطه را وارد كنيد!");
						MyErrorProvider.SetError(this, strMessage);
					}
					else
					{
						string strMessage = string.Format("فيلد {0} را وارد كنيد!", Caption);
						MyErrorProvider.SetError(this, strMessage);
					}
				}
				else
				{
					MyErrorProvider.SetError(this, "");
				}
			}

			if ((InputType != Enums.InputTypes.Numeric) && (InputType != Enums.InputTypes.Currency))
			{
				switch (RegularExperssion)
				{
					case Enums.RegularExperssions.None:
					{
						break;
					}

					case Enums.RegularExperssions.InternetEmailAddress:
					{
						Text = Text.Trim();

						if (!System.Text.RegularExpressions.Regex.IsMatch(Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
						{
							ForeColor = System.Drawing.Color.Red;
							string strMessage = "Example :Dariush@IranianExperts.ir";

							MyErrorProvider.SetError(this, strMessage);
						}
						else
						{
							ForeColor = System.Drawing.Color.Black;
						}
						break;
					}

					case Enums.RegularExperssions.InternetUrl:
					{
						Text = Text.Trim();

						if (!System.Text.RegularExpressions.Regex.IsMatch(Text, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
						{
							ForeColor = System.Drawing.Color.Red;
							string strMessage = "Example :http://www.IranianExperts.ir";

							MyErrorProvider.SetError(this, strMessage);
						}
						else
						{
							ForeColor = System.Drawing.Color.Black;
						}
						break;
					}

					case Enums.RegularExperssions.IRNPhoneNumber:
					{
						Text = Text.Trim();

						if (!System.Text.RegularExpressions.Regex.IsMatch(Text, @"((\(\d{3}\) ?)|(\d{4}-))?\d{7}|((\(\d{3}\) ?)|(\d{3}-))?\d{8}"))
						{
							ForeColor = System.Drawing.Color.Red;
							string strMessage = "Example :021-12345678 Or 0912-1234567";

							MyErrorProvider.SetError(this, strMessage);
						}
						else
						{
							ForeColor = System.Drawing.Color.Black;
						}
						break;
					}

					case Enums.RegularExperssions.IRNZIPCode:
					{
						Text = Text.Trim();

						if (System.Text.RegularExpressions.Regex.IsMatch(Text, @"\d{3}(-\d{6})(-\d{1})?") == false)
						{
							ForeColor = System.Drawing.Color.Red;
							string strMessage = "Example :123-123456-1";

							MyErrorProvider.SetError(this, strMessage);
						}
						else
						{
							ForeColor = System.Drawing.Color.Black;
						}
						break;
					}
				}
			}
		}

		protected override void OnEnter(System.EventArgs e)
		{
			base.OnEnter(e);

			switch (InputType)
			{
				case Enums.InputTypes.Numeric:
				{
					Text = Text.Replace(",", "").Replace(" ", "");
					break;
				}

				case Enums.InputTypes.Currency:
				{
					Text = Text.Replace(",", "").Replace(" ", "").Replace("ريال", "").Replace("$", "");
					break;
				}
			}

			switch (InputLanguage)
			{
				case Enums.InputLanguages.Farsi:
				{
					System.Windows.Forms.InputLanguage.CurrentInputLanguage =
					System.Windows.Forms.InputLanguage.FromCulture(_faCultureInfo);
					break;
				}

				case Enums.InputLanguages.English:
				{
					System.Windows.Forms.InputLanguage.CurrentInputLanguage =
					System.Windows.Forms.InputLanguage.FromCulture(_enCultureInfo);
					break;
				}
			}

			if (Multiline)
			{
				Select(0, 0);
			}
			else
			{
				SelectAll();
			}
		}

		protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			if ((e.KeyChar == 13) && (Multiline == false))
			{
				System.Windows.Forms.SendKeys.SendWait("{Tab}");
			}

			switch (InputType)
			{
				case Enums.InputTypes.Numeric:
				case Enums.InputTypes.Currency:
				{
					if ((((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == 8)) == false)
					{
						e.Handled = true;
					}
					else
					{
						if ((Text.Length == 0) && (e.KeyChar == '0'))
						{
							e.Handled = true;
						}
					}
					break;
				}
			}
		}

		protected override void OnValidated(System.EventArgs e)
		{
			base.OnValidated(e);

			switch (InputType)
			{
				case Enums.InputTypes.Currency:
				{
					if (Text != "")
					{
						long lngValue = System.Convert.ToInt64(Text);

						switch (InputLanguage)
						{
							case Enums.InputLanguages.Farsi:
							{
								Text = lngValue.ToString("#,##0 ريال");
								break;
							}

							case Enums.InputLanguages.English:
							{
								Text = lngValue.ToString("$#,##0");
								break;
							}
						}
					}
					break;
				}

				case Enums.InputTypes.Numeric:
				{
					if (Text != string.Empty)
					{
						long lngValue = System.Convert.ToInt64(Text);
						Text = lngValue.ToString("#,##0");
					}
					break;
				}
			}
		}
	}
}
