using GsoCarrelages.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepository.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var newId = await _productRepository.CreateAsync(product);

        product.IdProduit = newId;

        return CreatedAtAction(nameof(GetById), new { id = newId }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Product product)
    {
        if (id != product.IdProduit)
        {
            return BadRequest("L'id du produit ne correspond pas.");
        }

        var updated = await _productRepository.UpdateAsync(product);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _productRepository.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}