using System.Text.Json;
using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;
using InfrastructureOrder = GsoCarrelages.Infrastructure.Models.Order;

namespace GsoCarrelages.Infrastructure.Gateways;

public class OrderGateway : IOrderGateway
{
    private readonly IOrderRepository _orderRepository;

    public OrderGateway(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository
            ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public Task<long> CreateAsync(Order order)
    {
        var infrastructureOrder = ToInfrastructureOrder(order);

        return _orderRepository.CreateAsync(infrastructureOrder);
    }

    public async Task<IEnumerable<Order>> GetByClientEmailAsync(string email)
    {
        var infrastructureOrders =
            await _orderRepository.GetByClientEmailAsync(email);

        return infrastructureOrders.Select(ToCoreOrder);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var infrastructureOrders =
            await _orderRepository.GetAllAsync();

        return infrastructureOrders.Select(ToCoreOrder);
    }

    public Task<bool> UpdateStatusAsync(
        long idCommande,
        string statut
    )
    {
        return _orderRepository.UpdateStatusAsync(
            idCommande,
            statut
        );
    }

    private static InfrastructureOrder ToInfrastructureOrder(Order order)
    {
        return new InfrastructureOrder
        {
            IdCommande = order.IdCommande,
            IdClient = order.IdClient,
            ClientEmail = order.ClientEmail,
            Rue = order.Rue,
            Complement = order.Complement,
            Localite = order.Localite,
            CodePostal = order.CodePostal,
            ContactNom = order.ContactNom,
            ContactTel = order.ContactTel,
            Statut = order.Statut,
            TotalTtc = order.TotalTtc,
            Devise = order.Devise,
            LignesJson = JsonSerializer.Serialize(order.Lignes),
            CreatedAt = order.CreatedAt
        };
    }

    private static Order ToCoreOrder(InfrastructureOrder order)
    {
        var lignes = string.IsNullOrWhiteSpace(order.LignesJson)
            ? []
            : JsonSerializer.Deserialize<List<OrderLine>>(
                order.LignesJson
            ) ?? [];

        return new Order
        {
            IdCommande = order.IdCommande,
            IdClient = order.IdClient,
            ClientEmail = order.ClientEmail,
            Rue = order.Rue,
            Complement = order.Complement,
            Localite = order.Localite,
            CodePostal = order.CodePostal,
            ContactNom = order.ContactNom,
            ContactTel = order.ContactTel,
            Statut = order.Statut,
            TotalTtc = order.TotalTtc,
            Devise = order.Devise,
            Lignes = lignes,
            CreatedAt = order.CreatedAt
        };
    }
}
