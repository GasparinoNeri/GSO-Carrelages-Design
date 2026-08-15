namespace GsoCarrelages.Core.Entities;

public class OrderLine
{
    public long IdProduit { get; set; }

    public string Nom { get; set; } = string.Empty;

    public decimal PrixUnitaire { get; set; }

    public int Quantite { get; set; }
}
