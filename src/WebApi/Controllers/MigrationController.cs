using Microsoft.AspNetCore.Mvc;
using WebApi.Business.MigratieScripts;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]	
	public class MigrationController : ControllerBase
	{
		private readonly IRunMigratieScripts _runMigratieScripts;

		public MigrationController(
			IRunMigratieScripts runMigratieScripts
			)
		{
			_runMigratieScripts = runMigratieScripts;
		}

		public IActionResult Index()
		{
			return Ok();
		}

		[HttpPost("RunMigrations")]
		public IActionResult RunMigrations()
		{
			_runMigratieScripts.Execute();

			return Ok();
		}
	}
}