using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        // GET: ProductController
        public IActionResult Index()
        {
            IEnumerable<MProduct> productModel = _appDbContext.tbl_product;
            return View(productModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MProduct productModel)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Add(productModel);
                _appDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var idProduct = _appDbContext.tbl_product.Find(id);

            if (idProduct == null)
            {
                return NotFound();
            }

            return View(idProduct);
        }


        [HttpPost]
        public IActionResult Edit(MProduct productModel)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Update(productModel);
                _appDbContext.SaveChanges();
                TempData["editProduct"] = "Se edito correctamente el Producto";
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var idProduct = _appDbContext.tbl_product.Find(id);

            if (idProduct == null)
            {
                return NotFound();
            }

            return View(idProduct);
        }

        [HttpPost]
        public IActionResult Delete(MProduct productModel)
        {

            _appDbContext.Remove(productModel);
            _appDbContext.SaveChanges();
            TempData["deleteProduct"] = "Se Elimino correctamente el Producto";
            return RedirectToAction(nameof(Index));

        }

    }
}
