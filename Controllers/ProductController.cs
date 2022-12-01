using AspNetApiApplication.Models;
using AspNetApiApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetApiApplication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		public ProductController()
		{
		}

		[HttpGet]
		public ActionResult<List<Product>> GetAll() => ProductService.GetAll();

		[HttpGet("{id}")]
		public ActionResult<Product> Get(int id)
		{
			var product = ProductService.Get(id);

			if (product == null)
				return NotFound();

			return product;
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			ProductService.Add(product);
			return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Product product)
		{
			if (id != product.Id)
				return BadRequest();

			var existing = ProductService.Get(id);
			if (existing == null)
				return NotFound();

			ProductService.Update(product);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var existing = ProductService.Get(id);

			if (existing == null)
				return NotFound();

			ProductService.Delete(id);

			return NoContent();
		}
	}
}
