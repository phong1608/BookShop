using Bulky.DataAccess.Respository.IRespository;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Respository
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;

        }

        public async Task<bool> DeleteCategoryById(int? id)
        {
            
            _dbContext.Categories.RemoveRange(_dbContext.Categories.Where(t=>t.Id==id));
            int deletedRow = await _dbContext.SaveChangesAsync();
            return deletedRow > 0;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            List<Category> categories = await _dbContext.Categories.ToListAsync();
            
            return categories;
        }
        public async Task<Category?> GetCategoryById(int? id)
        {
            Category? category=await _dbContext.Categories.FirstOrDefaultAsync(p=>p.Id==id);
            return category;
        }
        public async Task<Category> UpdateCategory(Category category)
        {
            Category? matchingCatgory= await _dbContext.Categories.FirstOrDefaultAsync(temp=>temp.Id==category.Id);
            if(matchingCatgory==null)
            {
                return category;
            }
            matchingCatgory.Name = category.Name;
            matchingCatgory.DisplayOrder=category.DisplayOrder;
            int updatedCategory = await _dbContext.SaveChangesAsync();
            return matchingCatgory;

        }
    }
}
