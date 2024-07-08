using DataBase.Commands;

namespace Assets.Scripts.DataBase.GeneralCommands
{
	public struct LimitCommand : ICommand
	{
		public int Limit;

		public string Command()
		{
			return $"LIMIT {Limit}";
		}
	}
}
