using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        // GET: CategoryController
        public IActionResult Index()
        {
            IEnumerable<MCategory> categoryModel = _appDbContext.tbl_category;
            var objCategory = categoryModel.OrderBy(categoryModel=> categoryModel.Order).ToList();
            return View(objCategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MCategory categoryModel)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Add(categoryModel);
                _appDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var idCategory = _appDbContext.tbl_category.Find(id);

            if (idCategory == null)
            {
                return NotFound();
            }

            return View(idCategory);
        }


        [HttpPost]
        public IActionResult Edit(MCategory categoryModel)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Update(categoryModel);
                _appDbContext.SaveChanges();
                TempData["editCategory"] = "Se edito correctamente la Categoria";
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var idCategory = _appDbContext.tbl_category.Find(id);

            if (idCategory == null)
            {
                return NotFound();
            }

            return View(idCategory);
        }

        [HttpPost]
        public IActionResult Delete(MCategory categoryModel)
        {

            _appDbContext.Remove(categoryModel);
            _appDbContext.SaveChanges();
            TempData["deleteCategory"] = "Se Elimino correctamente la Categoria";
            return RedirectToAction(nameof(Index));

        }
    }
}
