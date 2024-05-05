using System.Diagnostics;
using GameStore.Data;
using GameStore.Models;
using GameStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger,  AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            ViewModelHome VMHome = new ViewModelHome()
            {
                Products = _appDbContext.tbl_product.Include(u => u.Category)
                .Include(u => u.Console),
                Consoles = _appDbContext.tbl_console,
                Categories = _appDbContext.tbl_category
            };

            return View(VMHome);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateAcount()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
