using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task DeleteProduct(Product product);
        Task EditProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetAllFavoritesProducts();
        Task<Product> GetProductById(int Id);
        Task MarkingFavourite(int id);
    }
}
