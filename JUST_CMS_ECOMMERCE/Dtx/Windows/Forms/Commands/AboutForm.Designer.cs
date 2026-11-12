namespace Dtx.Windows.Forms.Commands
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.lblAbout = new Dtx.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblAbout
			// 
			this.lblAbout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblAbout.Location = new System.Drawing.Point(5, 5);
			this.lblAbout.Name = "lblAbout";
			this.lblAbout.Size = new System.Drawing.Size(353, 170);
			this.lblAbout.TabIndex = 4;
			this.lblAbout.Text = resources.GetString("lblAbout.Text");
			this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(363, 202);
			this.Controls.Add(this.lblAbout);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Text = "درباره";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.Controls.SetChildIndex(this.lblAbout, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblAbout;
	}
}