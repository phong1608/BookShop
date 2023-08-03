using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _productService;

        public HomeController(ILogger<HomeController> logger, IProduct productServices)
        {
            _productService = productServices;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products= await _productService.GetAllProducts();
            return View(products);
        }
        public async Task<IActionResult> Details(int? productId)
        {
            if(productId==null || productId==0)
            {
                return NotFound();
            }

            Product? product= await _productService.GetProductById(productId);
            
            return View(product);
        }
        
            

        public IActionResult Privacy()
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