namespace Dtx.Windows.Forms
{
	public class ToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem, ICommandHolder
	{
		public ToolStripMenuItem()
		{
			Command = null;
		}

		private ICommand _command;
		[System.ComponentModel.DefaultValue(null)]
		public ICommand Command
		{
			get
			{
				return (_command);
			}
			set
			{
				_command = value;
			}
		}
	}
}
