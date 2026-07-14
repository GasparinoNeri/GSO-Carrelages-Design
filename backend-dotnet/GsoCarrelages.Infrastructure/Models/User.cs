namespace GsoCarrelages.Infrastructure.Models;

public class User
{
    public long IdUtilisateur { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Prenom { get; set; }

    public string Email { get; set; } = string.Empty;

    public string MotDePasse { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool Actif { get; set; }
}
