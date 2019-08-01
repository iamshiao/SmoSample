using System.Threading.Tasks;

namespace WebApi.Services
{
	public interface IRunSqlCommand
	{
		void Execute(string script);
	}
}