using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.API.Models;

namespace Shop.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {            
            return View(_productRepository.GetAllProducts());
        }

        public IActionResult Details(int Id)
        {
            return View(_productRepository.GetProductById(Id));
        }
    }
}