using Microsoft.AspNetCore.Mvc;
using raycommerceproject.Data;
using raycommerceproject.Models;
using raycommerceproject.Services;

namespace raycommerceproject.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();

            if (products.Count == 0)
            {
                return NotFound("No products found");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound(new { Message = "Product not found" });
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            var addedProduct = _productService.AddProduct(product);

            return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, addedProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var result = _productService.UpdateProduct(id, updatedProduct);

            if (result == null)
            {
                return NotFound(new { Message = "Product not found" });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var success = _productService.DeleteProduct(id);

            if (!success)
            {
                return NotFound(new { Message = "Product not found" });
            }

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
    }
