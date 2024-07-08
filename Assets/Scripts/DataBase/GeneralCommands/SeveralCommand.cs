using DataBase.Commands;
using System.Collections.Generic;
using System.Text;

namespace DataBase.DataBase.Commands
{
	public struct SeveralCommand : ICommand
	{
		public IEnumerable<ICommand> Commands;

		public SeveralCommand(params ICommand[] commands)
		{
			Commands = commands;
		}

		public string Command()
		{
			StringBuilder result = new StringBuilder();
			foreach(var command in Commands)
			{
				result.Append(command.Command());
				result.Append(';');
			}

			return result.ToString();
		}
	}
}
