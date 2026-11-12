namespace Dtx.Windows.Forms
{
	public class Button : System.Windows.Forms.Button, ICommandHolder
	{
		public Button()
		{
			Command = null;

			BackColor = System.Drawing.Color.Khaki;
			FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		}

		private ICommand _command;
		[System.ComponentModel.DefaultValue(null)]
		public ICommand Command
		{
			get
			{
				return (_command);
			}
			set
			{
				_command = value;
			}
		}

		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Khaki")]
		public override System.Drawing.Color BackColor
		{
			get
			{
				return (base.BackColor);
			}
			set
			{
				base.BackColor = value;
			}
		}

		[System.ComponentModel.DefaultValue(System.Windows.Forms.FlatStyle.Flat)]
		public new System.Windows.Forms.FlatStyle FlatStyle
		{
			get
			{
				return (base.FlatStyle);
			}
			set
			{
				base.FlatStyle = value;
			}
		}
	}
}
