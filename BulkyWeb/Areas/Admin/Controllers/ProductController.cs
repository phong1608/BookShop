using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Respository.IRespository;
using Bulky.Models;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProduct _productService;
        private readonly ICategory _categoryService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IProduct productService,ICategory categoryService,IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            List < Category > categories = await _categoryService.GetAllCategory();
            List<Product> listProducts = await _productService.GetAllProducts();
            
            IEnumerable<SelectListItem> CategoryList = categories.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.ToString()
            });

            return View(listProducts);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await _categoryService.GetAllCategory();

            ViewBag.CategoryList = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product,IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(u=>u.Errors).Select(e=>e.ErrorMessage).ToList();
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName= Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\products");
                using (var filestream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                product.ImageUrl = @"\images\products\" + fileName;

            }
            await _productService.AddProducts(product);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            else
            {
                List<Category> categories = await _categoryService.GetAllCategory();

                ViewBag.CategoryList = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
                Product? product =await _productService.GetProductById(id);
                return View(product);
            }
            

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile? file)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(u => u.Errors).Select(e => e.ErrorMessage).ToList();
                return View(product);
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\products");
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                product.ImageUrl = @"\images\products\" + fileName;

            }
                await _productService.UpdateProduct(product);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Product? product =await _productService.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            await _productService.DeleteProduct(product.Id);
            return RedirectToAction("Index");
        }



    }
}
