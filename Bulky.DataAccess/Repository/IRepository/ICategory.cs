using BulkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Respository.IRespository
{
    public interface ICategory
    {
        /// <summary>
        /// Add a new category to the data store
        /// </summary>
        /// <param name="category">Return a new category object after add it to the table</param>
        /// <returns></returns>
        Task<Category> AddCategory(Category category);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category">Return all Category from the data store</param>
        /// <returns></returns>
        Task<List<Category>> GetAllCategory();
        /// <summary>
        /// Delete a 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteCategoryById(int? id);
        Task<Category?> GetCategoryById(int? id);
        Task<Category> UpdateCategory(Category category);

    }
}
