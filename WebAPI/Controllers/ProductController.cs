using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        public static List<Product> productList = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = productList.SingleOrDefault(pr => pr.Id == Guid.Parse(id));
                if (product == null) { return NotFound("Product ID is not found"); }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = productVM.Name,
                Price = productVM.Price
            };
            productList.Add(product);
            return Ok(new
            {
                Success = true,
                Data = product
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, Product productEdit)
        {
            try
            {
                var product = productList.SingleOrDefault(pr => pr.Id == Guid.Parse(id));
                if (product == null) { return NotFound("Product ID is not found"); }
                //Update
                if (id != product.Id.ToString()) { return BadRequest("Id is not same!"); }
                product.Name = productEdit.Name;
                product.Price = productEdit.Price;

                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                var product = productList.SingleOrDefault(pr => pr.Id == Guid.Parse(id));
                if (product == null) { return NotFound("Product ID is not found"); }

                productList.Remove(product);
                return Ok(productList);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
