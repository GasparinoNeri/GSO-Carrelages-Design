using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Core.UseCases.Abstractions;

namespace GsoCarrelages.Core.UseCases;

public class OrderUseCases : IOrderUseCases
{
    private readonly IOrderGateway _orderGateway;

    private static readonly string[] AllowedStatuses =
    [
        "en_attente",
        "payee",
        "expediee",
        "livree",
        "annulee"
    ];

    public OrderUseCases(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway
            ?? throw new ArgumentNullException(nameof(orderGateway));
    }

    public async Task<long> CreateAsync(Order order)
    {
        if (string.IsNullOrWhiteSpace(order.ClientEmail))
        {
            throw new ArgumentException("Le client est obligatoire.");
        }

        if (order.Lignes.Count == 0)
        {
            throw new ArgumentException("Le panier est vide.");
        }

        if (order.TotalTtc <= 0)
        {
            throw new ArgumentException(
                "Le total de la commande doit être supérieur à 0."
            );
        }

        if (string.IsNullOrWhiteSpace(order.Rue))
        {
            throw new ArgumentException("La rue est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(order.Localite))
        {
            throw new ArgumentException("La ville est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(order.CodePostal))
        {
            throw new ArgumentException(
                "Le code postal est obligatoire."
            );
        }

        order.Statut = "en_attente";
        order.Devise = "EUR";

        return await _orderGateway.CreateAsync(order);
    }

    public Task<IEnumerable<Order>> GetByClientEmailAsync(string email)
    {
        return _orderGateway.GetByClientEmailAsync(email);
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        return _orderGateway.GetAllAsync();
    }

    public Task<bool> UpdateStatusAsync(
        long idCommande,
        string statut
    )
    {
        if (!AllowedStatuses.Contains(statut))
        {
            throw new ArgumentException(
                "Le statut de la commande est invalide."
            );
        }

        return _orderGateway.UpdateStatusAsync(
            idCommande,
            statut
        );
    }
}
