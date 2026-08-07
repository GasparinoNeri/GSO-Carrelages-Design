namespace GsoCarrelages.Core.Entities;

public class User
{
    public long IdUtilisateur { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Prenom { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? Telephone { get; set; }

    public string? Adresse { get; set; }

    public DateTime? DateNaissance { get; set; }

    public string? PhotoProfil { get; set; }

    public string MotDePasse { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool Actif { get; set; }

    public DateTime CreatedAt { get; set; }
}
