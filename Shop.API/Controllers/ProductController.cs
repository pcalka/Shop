using Microsoft.AspNetCore.Mvc;
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
            Product ProductFound = _productRepository.GetProductById(Id);
            if (ModelState.IsValid)
            {
                return View(ProductFound);
            }
            return NotFound();
        }

        public IActionResult Delete(int Id)
        {
            Product ProductFound = _productRepository.GetProductById(Id);
            if (ModelState.IsValid)
            {
                _productRepository.DeleteProduct(ProductFound);
                return View("Index");
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(product);                     
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}