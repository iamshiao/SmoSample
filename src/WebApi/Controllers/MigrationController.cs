using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using WebApi.Business.MigratieScripts;
using WebApi.Services;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	public class MigrationController : Controller
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
			return View();
		}

		[HttpPost("RunMigrations")]
		public IActionResult RunMigrations()
		{
			_runMigratieScripts.Execute();

			return Ok();
		}
	}
}