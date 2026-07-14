namespace GsoCarrelages.Infrastructure.Models;

public class Product
{
    public long IdProduit { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal PrixUnitaire { get; set; }

    public int StockOnHand { get; set; }

    public bool Actif { get; set; }
}
