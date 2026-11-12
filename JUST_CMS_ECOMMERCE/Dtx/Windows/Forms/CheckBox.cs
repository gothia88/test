namespace Dtx.Windows.Forms
{
	public class CheckBox : System.Windows.Forms.CheckBox
	{
		public CheckBox()
		{
			ForeColor = System.Drawing.Color.Khaki;
		}

		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Khaki")]
		public override System.Drawing.Color ForeColor
		{
			get
			{
				return (base.ForeColor);
			}
			set
			{
				base.ForeColor = value;
			}
		}
	}
}
