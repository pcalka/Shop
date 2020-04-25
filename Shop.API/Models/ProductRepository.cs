using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product GetProductById(int id)
        {
            var ProductFound = _context.Products.Find(id);
            return ProductFound;
        }
    }
}
