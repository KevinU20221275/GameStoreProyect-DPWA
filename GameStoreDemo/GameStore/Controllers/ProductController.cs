using GameStore.Data;
using GameStore.Models;
using GameStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: ProductController
        public IActionResult Index()
        {
            IEnumerable<MProduct> productList = _appDbContext.tbl_product;

            foreach (var obj in productList)
            {
                obj.Category = _appDbContext.tbl_category.FirstOrDefault(c => c.idCategory == obj.idCategory);
                obj.Console = _appDbContext.tbl_console.FirstOrDefault(c => c.idConsole == obj.idConsole);

            }
            return View(productList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewModelProduct vmproduct = new ViewModelProduct()
            {
                MProduct = new MProduct(),
                categorySelectList = _appDbContext.tbl_category.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.idCategory.ToString()
                }),

                consoleSelectList = _appDbContext.tbl_console.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.ConsoleName,
                    Value = c.idConsole.ToString()
                })
            };

            return View(vmproduct);
            
        }

        [HttpPost]
        public IActionResult Create(ViewModelProduct vmProduct)
        {
            var files = HttpContext.Request.Form.Files;
            string webPath = _webHostEnvironment.WebRootPath;
            string upload = webPath + SSP.ProductPath;
            string fileName= Guid.NewGuid().ToString();
            string extencion = Path.GetExtension(files[0].FileName);
            using (var fileStream = new FileStream
                (Path.Combine(upload, fileName + extencion), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            vmProduct.MProduct.Image = fileName + extencion;
            _appDbContext.Add(vmProduct.MProduct);
            _appDbContext.SaveChanges();

            
            return RedirectToAction("Index");
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
