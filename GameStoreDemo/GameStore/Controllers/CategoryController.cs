using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public IActionResult Index()
        {
            return View();
        }
    }
}
