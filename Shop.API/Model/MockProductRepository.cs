using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Model
{
    public class MockProductRepository : IProductRepository
    {
        private List<Product> _productsList;
        public MockProductRepository()
        {
            if (_productsList == null)
            {
                LoadProduct();
            }
        }

        private void LoadProduct()
        {
            _productsList = new List<Product>
                {
                new Product { Id = 1, Name = "Ruler", Price = 1 },
                new Product { Id = 3, Name = "Pencilecase", Price = 2 },
                new Product { Id = 2, Name = "Pen", Price = 1 },
                new Product { Id = 4, Name = "Book", Price = 10 },
                new Product { Id = 2, Name = "Box", Price = 5 }
                };
        }

        public List<Product> GetAllProducts()
        {
            return _productsList;
        }

        public Product GetById(int Id)
        {
            return _productsList.Find(p => p.Id == Id);
        }
    }   
}
