using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Model;
using ProductsAPI.Data;
using ProductsAPI.Services;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAPI.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct productRepository;

        /*Below code does dependency injection via constructor
          It is not responsibility of Product Controller Class to create Object of Product Repository
        It have to be added in Startup Class configure service method to create object of Product Repository */
        public ProductsController (IProduct _productRepository)
        {
            productRepository = _productRepository ;
        }

        // GET: api/Products
        //1. Get List of all products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productRepository.GetProducts();
        }

        // GET api/Products/1
        //2. Get product details for specific Id 
        [HttpGet("{id}" ,Name ="Get")]
        public Product Get(int id)
        {
            var product = productRepository.GetProduct(id);
            return product;
        }

        // POST api/Products
        // 3. Add new product to the database
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productRepository.AddProduct(product);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/Products/5
        //4. Update existing Product in the database with respective id 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != product.Id)
            {
                return BadRequest();
            }
            try 
            {
                productRepository.UpdateProduct(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound("Invalid product ID...");

            }
            return Ok("Product Updated...");
        }

        // DELETE api/Product/5
        //5. Delete the existing product with respective Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productRepository.DeleteProduct(id);
            return Ok("Record Deleted...");
        }
    }
}
