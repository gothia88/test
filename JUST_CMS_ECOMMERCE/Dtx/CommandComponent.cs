namespace Dtx
{
	public abstract class CommandComponent : System.ComponentModel.Component, ICommand
	{
		public virtual void Execute()
		{
			if (CheckCommandValidation())
			{
				ExecuteIfCommandIsValid();
			}
		}

		protected abstract bool CheckCommandValidation();
		protected abstract void ExecuteIfCommandIsValid();
	}
}
