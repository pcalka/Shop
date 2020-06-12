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

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {    
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return _context.Products;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product ProductFound = await _context.Products.FindAsync(id);
            return ProductFound;
        }

        public async Task<IEnumerable<Product>> GetAllFavoritesProducts()
        {
            List<Product> FavoritesProductsList = _context.Products.Where(x => x.IsFavourite == true).ToList();
            return FavoritesProductsList;
        } 

        public async Task MarkingFavourite(int id)
        {
            var ProductFound = await _context.Products.FindAsync(id);
            if (ProductFound.IsFavourite == false)
            {
                ProductFound.IsFavourite = true;
            }
            else ProductFound.IsFavourite = false;
        }
    }
}
