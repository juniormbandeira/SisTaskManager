using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.DTOs;

public static class PerfilDto
{
    // --- Para POST (Criação) ---
    public class CreateRequest
    {

        [Required(ErrorMessage = "Nome do perfil é obrigatório!")]
        [StringLength(50)]
        public required string Nome { get; set; }

        [StringLength(200)]
        public string? Descricao { get; set; }
    }

    // --- Para PUT (Atualização) ---
    public class UpdateRequest
    {
        [Required(ErrorMessage = "ID do perfil é obrigatório!")]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public required string Nome { get; set; }

        [StringLength(200)]
        public string? Descricao { get; set; }
    }

    // --- Para GET (Resposta) ---
    public class Response
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public List<UserShortResponse> Usuarios { get; set; } = new(); // Lista simplificada
    }

    // --- DTO auxiliar para evitar referência circular ---
    public class UserShortResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}