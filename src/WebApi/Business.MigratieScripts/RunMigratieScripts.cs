using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApi.Services;

namespace WebApi.Business.MigratieScripts
{

	public class RunMigratieScripts : IRunMigratieScripts
	{
		private const string migrationScriptDirectory = "Migrations";
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IRunSqlCommand _runSqlCommand;

		public RunMigratieScripts(
			IHostingEnvironment hostingEnvironment,
			IRunSqlCommand runSqlCommand)
		{
			_hostingEnvironment = hostingEnvironment;
			_runSqlCommand = runSqlCommand;
		}
		public void Execute()
		{
			var allMigrations = GetAllMigrations();
			if (allMigrations != null)
			{
				foreach (var migration in allMigrations.OrderBy(m => m.Filename))
				{
					RunMigration(migration);
				}
			}
		}

		private void RunMigration(MigrationFile migration)
		{
			_runSqlCommand.Execute(migration.Contents);
		}

		private IEnumerable<MigrationFile> GetAllMigrations()
		{
			var baseDirectory = _hostingEnvironment.ContentRootPath;
			var migrationFilePath = Path.Combine(baseDirectory, migrationScriptDirectory);

			var migrationFiles = Directory.GetFiles(migrationFilePath);

			if (migrationFiles != null)
			{
				foreach (var migrationFile in migrationFiles)
				{
					var contents = File.ReadAllText(migrationFile);

					yield return new MigrationFile
					{
						Filename = migrationFile,
						Contents = contents
					};
				}
			}
		}
	}
}