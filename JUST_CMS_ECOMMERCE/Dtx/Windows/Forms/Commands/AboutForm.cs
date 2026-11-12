namespace Dtx.Windows.Forms.Commands
{
	internal partial class AboutForm : BaseForm
	{
		public AboutForm()
		{
			InitializeComponent();
		}

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			myBaseStatusStrip.Visible = false;
		}
	}
}
