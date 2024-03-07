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

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            var idConsole = _appDbContext.tbl_console.Find(id);

            if (idConsole == null)
            {
                return NotFound();
            }

            return View(idConsole);
        }


        [HttpPost]
        public IActionResult Edit(MConsole consoleModel)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Update(consoleModel);
                _appDbContext.SaveChanges();
                TempData["editConsole"] = "Se edito correctamente la Consola";
                return RedirectToAction(nameof(Index));
            }
            return View(consoleModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var idConsole = _appDbContext.tbl_console.Find(id);

            if (idConsole == null)
            {
                return NotFound();
            }

            return View(idConsole);
        }

        [HttpPost]
        public IActionResult Delete(MConsole consoleModel)
        {
            
            _appDbContext.Remove(consoleModel);
            _appDbContext.SaveChanges();
            TempData["deleteConsole"] = "Se Elimino correctamente la Consola";
            return RedirectToAction(nameof(Index));
            
        }
    }
}
