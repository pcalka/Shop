using System.Collections.Generic;

namespace Shop.API.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int Id);
    }
}
