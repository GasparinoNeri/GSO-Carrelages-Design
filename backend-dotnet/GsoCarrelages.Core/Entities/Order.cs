namespace GsoCarrelages.Core.Entities;

public class Order
{
    public long IdCommande { get; set; }

    public long IdClient { get; set; }

    public string ClientEmail { get; set; } = string.Empty;

    public string Rue { get; set; } = string.Empty;

    public string? Complement { get; set; }

    public string Localite { get; set; } = string.Empty;

    public string CodePostal { get; set; } = string.Empty;

    public string? ContactNom { get; set; }

    public string? ContactTel { get; set; }

    public string Statut { get; set; } = "en_attente";

    public decimal TotalTtc { get; set; }

    public string Devise { get; set; } = "EUR";

    public List<OrderLine> Lignes { get; set; } = [];

    public DateTime CreatedAt { get; set; }
}
