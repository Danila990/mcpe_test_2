using DataBase.Commands;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BucketObjectsListUpdater.DB.DBExecuters
{
	public class DataBaseCommandExecuter : IDisposable
	{
		private readonly SqliteConnection _connection;
		private readonly string _pathToDataBase;

		private Stream _dataBaseStream;

		public DataBaseCommandExecuter(string pathToDataBase)
		{
			_pathToDataBase = pathToDataBase;
			var connectionString = $"URI=file:{pathToDataBase};";
			_connection = new SqliteConnection(connectionString);
		}

		public bool IsOpen
		{
			get;
			private set;
		}

		public void Dispose()
		{
			_connection.Close();
			_connection.Dispose();
			_dataBaseStream.Close();
			IsOpen = false;
		}

		public async Task OpenConnection(CancellationToken token = default)
		{
			if(!File.Exists(_pathToDataBase))
			{
				File.Create(_pathToDataBase).Dispose();
			}

			_dataBaseStream = File.Open(_pathToDataBase, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			await Task.Run(_connection.Open, token);
			IsOpen = true;
		}

		public async Task ExecuteNonQuery(ICommand commandText, CancellationToken token = default)
		{
			using(var command = _connection.CreateCommand())
			{
				command.CommandText = commandText.Command();
				await command.ExecuteNonQueryAsync(token);
			}
		}

		public async Task<object> ExecuteScalar(ICommand commandText, CancellationToken token = default)
		{
			using(var command = _connection.CreateCommand())
			{
				command.CommandText = commandText.Command();
				return await command.ExecuteScalarAsync(token);
			}
		}

		public async Task<DbDataReader> ExecuteReader(ICommand commandText, 
			CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default)
		{
			using(var command = _connection.CreateCommand())
			{
				command.CommandText = commandText.Command();
				return await command.ExecuteReaderAsync(behavior, token);
			}
		}
	}
}
