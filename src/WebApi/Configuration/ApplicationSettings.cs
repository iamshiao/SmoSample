using Microsoft.Extensions.Configuration;

namespace WebApi.Configuration
{
	public class ApplicationSettings
	{
		public ApplicationSettings(IConfiguration configuration)
		{
			configuration.GetSection(nameof(ApplicationSettings)).Bind(this);
		}

		public string MigrationsFolder { get; } = "Migrations";
		public string DatabaseConnectionString { get; set; }
		public string SqlServerLogin { get; set; }
		public string SqlServerPassword { get; set; }
		public string SqlServerDatabase { get; set; }
	}
}
