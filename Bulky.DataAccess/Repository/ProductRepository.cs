using Azure.Core;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _db;
        
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProducts(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int? id)
        {
            _db.Products.RemoveRange(_db.Products.Where(p => p.Id == id));
            int removedProduct = await _db.SaveChangesAsync();
            return removedProduct > 0;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            List<Product>? products = await _db.Products.Include("Category").ToListAsync();
            return products;
        }

        public async Task<Product?> GetProductById(int? id)
        {
           Product? product = await _db.Products.Include("Category").FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            Product? matchingProduct = await _db.Products.FirstOrDefaultAsync(temp => temp.Id == product.Id);
            if (matchingProduct == null)
            {
                return product;
            }
            matchingProduct.ISBN = product.ISBN;
            matchingProduct.Author = product.Author;
            matchingProduct.Description = product.Description;
            
            matchingProduct.Price = product.Price;
            matchingProduct.Price50 = product.Price50;
            matchingProduct.Price100 = product.Price100;
            matchingProduct.CategoryId = product.CategoryId;
            matchingProduct.ImageUrl = product.ImageUrl;
            int updatedCategory = await _db.SaveChangesAsync();
            return matchingProduct;

        }
    }
}
