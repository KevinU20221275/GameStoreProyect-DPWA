using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

    }
}
