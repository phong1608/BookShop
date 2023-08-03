using Bulky.DataAccess.Respository.IRespository;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryService;
        
        public CategoryController( ICategory categoryService)
        {
            
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> listCategories = await _categoryService.GetAllCategory();
            return View(listCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(u => u.Errors).Select(e => e.ErrorMessage).ToList();
                return View(category);
            }
            await _categoryService.AddCategory(category);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(u => u.Errors).Select(e => e.ErrorMessage).ToList();
                return View(category);
            }

            await _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {

            await _categoryService.DeleteCategoryById(category.Id);
            return RedirectToAction("Index");
        }
    }
}
