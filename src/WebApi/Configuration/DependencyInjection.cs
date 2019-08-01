using Microsoft.Extensions.DependencyInjection;
using WebApi.Business.MigratieScripts;
using WebApi.Services;

namespace WebApi.Configuration
{
	public static class DependencyInjection
	{
		public static void AddDependencyInjection(this IServiceCollection services, ApplicationSettings applicationSettings)
		{
			services.AddSingleton<ApplicationSettings>(applicationSettings);
			services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(new Microsoft.Extensions.Logging.Logger<Startup>(new Microsoft.Extensions.Logging.LoggerFactory()));

			services.AddTransient<IRunMigratieScripts, RunMigratieScripts>();
			services.AddTransient<IRunSqlCommand, RunSqlCommand>();
		}
	}
}
