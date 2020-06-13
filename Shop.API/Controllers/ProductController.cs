using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
      
        public async Task<IActionResult> Details(int id)
        {
            Product ProductFound = await _productRepository.GetProductById(id);
            if (ModelState.IsValid)
            {
                return View(ProductFound);
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product ProductFound = await _productRepository.GetProductById(id);
            if (ProductFound == null)
            return NotFound();
            
            return View(ProductFound);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {             
           Product product = await _productRepository.GetProductById(id);
           await _productRepository.DeleteProduct(product);
           return RedirectToAction(nameof(Index));       
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productRepository.GetProductById(id);
            if (product == null) NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.EditProduct(product);
                return RedirectToAction("Index");
            }
            else return View(product);
        }

        public async Task<IActionResult> Favourites()
        {
            return View(await _productRepository.GetAllFavoritesProducts());
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetAllProducts());
        }     
    }
}