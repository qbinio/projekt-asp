using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projekt.Models;

namespace projekt.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult ListAll() => View(productRepository.Products);

        public ViewResult List(string category) => View(productRepository.Products.Where( p => p.Category == category));
        
    }
}

