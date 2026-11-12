namespace Dtx.Windows.Forms.Commands
{
	public class CommandOfAbout : System.ComponentModel.Component, ICommand
	{
		public void Execute()
		{
			AboutForm frmAbout =
				new AboutForm();

			frmAbout.ShowDialog();

			if (frmAbout.IsDisposed == false)
			{
				frmAbout.Dispose();
			}
			frmAbout = null;
		}
	}
}
