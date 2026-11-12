namespace Dtx.Windows.Forms
{
	public class ToolStrip : System.Windows.Forms.ToolStrip
	{
		public ToolStrip()
		{
			BackColor = System.Drawing.Color.Lavender;
		}

		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Lavender")]
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
