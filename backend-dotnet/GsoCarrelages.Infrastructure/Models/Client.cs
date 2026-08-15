namespace GsoCarrelages.Infrastructure.Models;

public class Client
{
    public long IdClient { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? Tel { get; set; }

    public DateTime DateInscription { get; set; }

    public string Statut { get; set; } = "actif";
}
