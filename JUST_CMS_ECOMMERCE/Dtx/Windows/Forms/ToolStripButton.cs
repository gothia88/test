namespace Dtx.Windows.Forms
{
	public class ToolStripButton : System.Windows.Forms.ToolStripButton, ICommandHolder
	{
		public ToolStripButton()
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
