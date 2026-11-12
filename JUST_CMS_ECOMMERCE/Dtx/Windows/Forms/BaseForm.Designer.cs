namespace Dtx.Windows.Forms
{
	partial class BaseForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.myBaseStatusStrip = new Dtx.Windows.Forms.StatusStrip();
			this.SuspendLayout();
			// 
			// myBaseStatusStrip
			// 
			this.myBaseStatusStrip.Location = new System.Drawing.Point(0, 251);
			this.myBaseStatusStrip.Name = "myBaseStatusStrip";
			this.myBaseStatusStrip.Size = new System.Drawing.Size(292, 22);
			this.myBaseStatusStrip.TabIndex = 3;
			// 
			// BaseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.SteelBlue;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.myBaseStatusStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.Black;
			this.Name = "BaseForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected StatusStrip myBaseStatusStrip;
	}
}