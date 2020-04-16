using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Model
{
    interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetById(int Id);
    }
}
