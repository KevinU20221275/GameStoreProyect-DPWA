using GameStore.Data;
using GameStore.Models;
using GameStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            vmproduct.MProduct = _appDbContext.tbl_product.Find(id);

            return View(vmproduct);
        }


        [HttpPost]
        public IActionResult Edit(ViewModelProduct vmProduct)
        {
            var obj = _appDbContext.tbl_product.AsNoTracking()
                .FirstOrDefault(u => u.idProduct==vmProduct.MProduct.idProduct);

            var files = HttpContext.Request.Form.Files; // Para ver la imagen
            string webPath = _webHostEnvironment.WebRootPath;
            if (files.Count > 0)
            {
                string upload = webPath + SSP.ProductPath;
                var oldfile = Path.Combine(upload, obj.Image);
                if (System.IO.File.Exists(oldfile))
                {
                    System.IO.File.Delete(oldfile);
                }
  
                string fileName = Guid.NewGuid().ToString();
                string extencion = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream
                    (Path.Combine(upload, fileName + extencion), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                vmProduct.MProduct.Image = fileName + extencion;
            } else
            {
                vmProduct.MProduct.Image = obj.Image;
            }
            

            _appDbContext.Update(vmProduct.MProduct);
            _appDbContext.SaveChanges();
            TempData["editProduct"] = "Se edito correctamente el Producto";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            MProduct objProduct = _appDbContext.tbl_product.Include(c => c.Category)
                .Include(c => c.Console).FirstOrDefault(p => p.idProduct == id);

            if (objProduct == null)
            {
                return NotFound();
            }

            return View(objProduct);
        }


        [HttpPost]
        public IActionResult Delete(int idProduct)
        {

            MProduct product = _appDbContext.tbl_product.Find(idProduct);

            string webPath = _webHostEnvironment.WebRootPath;
            string upload = webPath + SSP.ProductPath;
            var oldfile = Path.Combine(upload, product.Image);
            if (System.IO.File.Exists(oldfile))
            {
                System.IO.File.Delete(oldfile);
            }

            if (product == null)
            {
                return NotFound();
            }
            _appDbContext.Remove(product);
            _appDbContext.SaveChanges();
            TempData["deleteProduct"] = "Se Elimino correctamente el Producto";
            return RedirectToAction(nameof(Index));

        }

    }
}
