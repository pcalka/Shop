using System.Collections.Generic;
using System.Linq;

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
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetAllFavoritesProducts()
        {
            var FavoritesProductsList = _context.Products.Where(x => x.IsFavourite == true).ToList();
            return FavoritesProductsList;
        }

        public Product GetProductById(int id)
        {
            var ProductFound = _context.Products.Find(id);
            return ProductFound;
        }

        public void EditProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
