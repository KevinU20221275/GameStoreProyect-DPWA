using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ConsoleController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ConsoleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        // GET: ConsoleController
        public IActionResult Index()
        {
            IEnumerable<MConsole> consoleModel=_appDbContext.tbl_console;
            return View(consoleModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MConsole consoleModel)
        {
            if (ModelState.IsValid) 
            { 
                _appDbContext.Add(consoleModel);
                _appDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(consoleModel);
        }
        
    }
}
