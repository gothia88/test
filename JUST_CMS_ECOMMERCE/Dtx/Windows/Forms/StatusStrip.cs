namespace Dtx.Windows.Forms
{
	public class StatusStrip : System.Windows.Forms.StatusStrip
	{
		public StatusStrip()
		{
			BackColor = System.Drawing.Color.Linen;
		}

		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Linen")]
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
