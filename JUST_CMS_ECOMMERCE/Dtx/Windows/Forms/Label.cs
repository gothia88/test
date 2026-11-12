namespace Dtx.Windows.Forms
{
	public class Label : System.Windows.Forms.Label
	{
		public Label()
		{
			ForeColor = System.Drawing.Color.White;
			TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		}

		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "White")]
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

		[System.ComponentModel.DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public override System.Drawing.ContentAlignment TextAlign
		{
			get
			{
				return (base.TextAlign);
			}
			set
			{
				base.TextAlign = value;
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			if (DesignMode)
			{
				System.Drawing.Pen oPen =
					new System.Drawing.Pen(System.Drawing.Color.DarkGray, 1.0f);

				oPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

				System.Drawing.Rectangle oRectangle =
					new System.Drawing.Rectangle(0, 0, Width - 1, Height - 1);

				e.Graphics.DrawRectangle(oPen, oRectangle);
			}
		}
	}
}
