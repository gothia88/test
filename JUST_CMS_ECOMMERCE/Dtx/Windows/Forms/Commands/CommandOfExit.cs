namespace Dtx.Windows.Forms.Commands
{
	public class CommandOfExit : System.ComponentModel.Component, ICommand
	{
		public void Execute()
		{
			System.Windows.Forms.DialogResult enmResult;

			enmResult =
				Dtx.Windows.Forms.MessageBox.Show
					(
						"آيا به خروج از برنامه اطمينان داريد؟",
						"سوال",
						System.Windows.Forms.MessageBoxButtons.YesNo,
						System.Windows.Forms.MessageBoxIcon.Question,
						System.Windows.Forms.MessageBoxDefaultButton.Button2
					);

			if (enmResult == System.Windows.Forms.DialogResult.Yes)
			{
				System.Windows.Forms.Application.Exit();
			}
		}
	}
}
