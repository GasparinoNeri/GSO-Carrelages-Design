using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GsoCarrelages.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductUseCases _productUseCases;

    public ProductsController(IProductUseCases productUseCases)
    {
        _productUseCases = productUseCases;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productUseCases.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var product = await _productUseCases.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var newId = await _productUseCases.CreateAsync(product);

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

        var updated = await _productUseCases.UpdateAsync(product);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _productUseCases.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}