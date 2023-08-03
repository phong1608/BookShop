using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IProduct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return all products from the data store</returns>
        Task<List<Product>> GetAllProducts();
        /// <summary>
        /// Add a new product object to the data store
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Return a new product object after adding it to the data store</returns>
        Task<Product> AddProducts(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int? id);
        Task<Product?> GetProductById(int? id);
    }
}
