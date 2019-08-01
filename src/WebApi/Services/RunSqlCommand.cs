using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Configuration;

namespace WebApi.Services
{
	public class RunSqlCommand : IRunSqlCommand
	{
		private readonly ApplicationSettings _applicationSettings;
		private readonly ILogger _logger;

		public RunSqlCommand(
			ApplicationSettings applicationSettings,
			ILogger logger)
		{
			_applicationSettings = applicationSettings;
			_logger = logger;
		}
		public void Execute(string script)
		{
			ExecuteWithSmo(script);
			//Task.Run(async () => await ExecuteWithAdo(script));
		}

		private async Task ExecuteWithAdo(string script)
		{
			using (var connection = new SqlConnection(_applicationSettings.DatabaseConnectionString))
			{
				connection.Open();
				using (SqlTransaction outertransaction = connection.BeginTransaction(IsolationLevel.Serializable))
				{
					try
					{
						foreach (var innerScript in script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
						{
							var scriptToRun = innerScript.Replace("GO", string.Empty);
							using (var sqlCommand = new SqlCommand(scriptToRun, connection, outertransaction))
							{
								sqlCommand.CommandTimeout = (int)TimeSpan.FromMinutes(3).TotalSeconds;
								await sqlCommand.ExecuteNonQueryAsync();
							}
						}
						outertransaction.Commit();
					}
					catch (Exception ex)
					{
						outertransaction.Rollback();
						_logger.LogInformation("Script not executed {0}", script);
						throw;
					}
				}
			}

		}
		private void ExecuteWithSmo(string script)
		{
			ServerConnection serverConnection = null;
			try
			{
				serverConnection = new ServerConnection(
											new SqlConnection(_applicationSettings.DatabaseConnectionString));

				var server = new Server(serverConnection);
				serverConnection.BeginTransaction();
				var database = server.Databases[_applicationSettings.SqlServerDatabase];
				database.ExecuteNonQuery(script);
				serverConnection.CommitTransaction();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "SMO failure, rolling back");
				serverConnection?.RollBackTransaction();
				throw;
			}
			finally
			{
				serverConnection?.Disconnect();
			}
		}
	}
}
