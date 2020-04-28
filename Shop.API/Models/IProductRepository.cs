using System.Collections.Generic;

namespace Shop.API.Models
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        void EditProduct(int id, Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int Id);       
    }
}
