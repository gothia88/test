namespace Dtx.Windows.Forms
{
	public class MenuStrip : System.Windows.Forms.MenuStrip
	{
		public MenuStrip()
		{
			BackColor = System.Drawing.Color.Azure;
		}

		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Azure")]
		public new System.Drawing.Color BackColor
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
	}
}
