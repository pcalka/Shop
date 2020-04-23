using System.Collections.Generic;

namespace Shop.API.Models
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

        public IEnumerable<Product> GetAllProducts()
        {
            return _productsList;
        }

        public Product GetProductById(int Id)
        {
            return _productsList.Find(p => p.Id == Id);
        }
    }   
}
