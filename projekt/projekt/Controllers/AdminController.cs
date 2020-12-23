using System.Linq;
using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using Microsoft.AspNetCore.Authorization;


namespace projekt.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        [Authorize]
        public ViewResult Index() => View(repository.Products);
        [HttpGet]
        public ViewResult Edit(int productId) =>
            View(repository.Products
                .FirstOrDefault(p => p.ProductId == productId));
        [HttpPost]
        public IActionResult Save(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Zapisano{product.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }
        public ViewResult Create() => View("Edit", new Product());
        [HttpPost]
        public IActionResult Delete( int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if(deleteProduct != null)
            {
                TempData["message"] = $"Usunieto{deleteProduct.Name}";
            }
            return RedirectToAction("Index");
        }
    }
}
