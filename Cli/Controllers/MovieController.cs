using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cli.Controllers
{
    [Route("/")]
    public class MovieController : ControllerBase
    {
        [HttpGet]   
        public async Task<string> Index()
        {
            return "hello";
        }
    }
}