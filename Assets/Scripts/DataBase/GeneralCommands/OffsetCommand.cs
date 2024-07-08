using DataBase.Commands;

namespace Assets.Scripts.DataBase.GeneralCommands
{
	public struct OffsetCommand : ICommand
	{
		public int Offset;

		public string Command()
		{
			return $"OFFSET {Offset}";
		}
	}
}
