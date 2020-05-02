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
      
        public IActionResult Details(int id)
        {
            Product ProductFound = _productRepository.GetProductById(id);
            if (ModelState.IsValid)
            {
                return View(ProductFound);
            }
            return NotFound();
        }

        public IActionResult Delete(int id)
        {
            Product ProductFound = _productRepository.GetProductById(id);
            if (ProductFound == null)
            return NotFound();
            
            return View(ProductFound);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {             
           Product product = _productRepository.GetProductById(id);
           _productRepository.DeleteProduct(product);
           return RedirectToAction(nameof(Index));       
        }

        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetProductById(id);
            if (product == null) NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.EditProduct(product);
                return RedirectToAction("Index");
            }
            else return View(product);
        }

        public IActionResult Index()
        {
            return View(_productRepository.GetAllProducts());
        }
    }
}