using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Models;

namespace SimpleWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
        new Product { Id = 2, Name = "Smartphone", Price = 699.99m, Category = "Electronics" },
        new Product { Id = 3, Name = "Desk Chair", Price = 89.99m, Category = "Furniture" }
    };

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(_products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
}