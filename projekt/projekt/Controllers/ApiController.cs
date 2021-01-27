using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;


namespace projekt.Controllers
{
   [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly IProductRepository repository;
        public ApiController(IProductRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Gets all products by category
        /// </summary>
        /// <param name="category">Product category</param>
        /// <returns>All products List by category </returns>
       [HttpGet("GetAll")]
       public ActionResult<IEnumerable<Product>> List(string category)
        {
            return Ok(repository.Products.Where(p => p.Category == category));
        }
        /// <summary>
        /// Get one product by  id 
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>One product</returns>
        [HttpGet("GetById")]

        public ActionResult<Product> GetById(int id)
        {
            var product = repository.Products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();
            return Ok (product);
        }
        /// <summary>
        /// Adds product to database
        /// </summary>
        /// <param name="product">Product to add </param>
        /// <returns>Insertion Product</returns>

        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            repository.SaveProduct(product);
            return Ok(product);
        }
        /// <summary>
        /// Delete Product by Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<Product> Delete(int productId)
        {
            repository.DeleteProduct(productId);
            return NoContent();
        }
        /// <summary>
        /// Updates Product
        /// </summary>
        /// <param name="product">Update Product </param>
        /// <returns>Update Product</returns>
        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!repository.Products.Any(p => p.ProductId == product.ProductId))
                return NotFound();

            repository.SaveProduct(product);
            return NoContent();
        }
    }
}
