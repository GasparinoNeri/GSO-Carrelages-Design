using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GsoCarrelages.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderUseCases _orderUseCases;

    public OrdersController(IOrderUseCases orderUseCases)
    {
        _orderUseCases = orderUseCases;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        try
        {
            var order = new Order
            {
                ClientEmail = request.ClientEmail,
                Rue = request.Rue,
                Complement = request.Complement,
                Localite = request.Localite,
                CodePostal = request.CodePostal,
                ContactNom = request.ContactNom,
                ContactTel = request.ContactTel,
                TotalTtc = request.TotalTtc,
                Lignes = request.Lignes
            };

            var newId = await _orderUseCases.CreateAsync(order);

            return Ok(new
            {
                idCommande = newId
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("client/{email}")]
    public async Task<IActionResult> GetByClientEmail(string email)
    {
        var orders =
            await _orderUseCases.GetByClientEmailAsync(email);

        return Ok(orders);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderUseCases.GetAllAsync();

        return Ok(orders);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        long id,
        UpdateOrderStatusRequest request
    )
    {
        try
        {
            var updated =
                await _orderUseCases.UpdateStatusAsync(
                    id,
                    request.Statut
                );

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public record CreateOrderRequest(
    string ClientEmail,
    string Rue,
    string? Complement,
    string Localite,
    string CodePostal,
    string? ContactNom,
    string? ContactTel,
    decimal TotalTtc,
    List<OrderLine> Lignes
);

public record UpdateOrderStatusRequest(
    string Statut
);
