using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Model
{
    public class MockProductRepository
    {
        private List<Product> _productsList;
        public MockProductRepository()
        {

        }

        private void LoadProduct()
        {
            _productsList = new List<Product>
                {
                new Product { Id = 1, Name = "Ruler", Price = 1 },
                new Product { Id = 3, Name = "Pencilecase", Price = 2 },
                new Product { Id = 2, Name = "Pen", Price = 1 }
                };
        }
    }   
}
