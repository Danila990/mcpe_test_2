using DataBase.Commands;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.DataBase.GeneralCommands
{
	public struct UniteCommand : ICommand
	{
		public IEnumerable<ICommand> Commands;

		public string Command()
		{
			StringBuilder result = new StringBuilder();
			foreach(var command in Commands)
			{
				result.Append(command.Command());
				result.Append(' ');
			}

			return result.ToString();
		}
	}
}
