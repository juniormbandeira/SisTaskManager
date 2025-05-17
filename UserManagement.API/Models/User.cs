namespace UserManagement.API.Models;

public class User
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string SenhaHash { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Chave estrangeira para Perfil
    public int PerfilId { get; set; }

    // Relacionamento: Um usuário pertence a um perfil
    public Perfil? Perfil { get; set; }
}