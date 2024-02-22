using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ConsoleController : Controller
    {
        // GET: ConsoleController
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
