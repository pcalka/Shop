using System.Collections.Generic;

namespace Shop.API.Models
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        void EditProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllFavoritesProducts();
        Product GetProductById(int Id);       
    }
}
