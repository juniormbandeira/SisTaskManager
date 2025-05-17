using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.DTOs;

public static class UserDto
{
    // --- Para POST (Criação) ---
    public class CreateRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(100, MinimumLength = 3)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Formato de email inválido!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória!")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres!")]
        public required string Senha { get; set; } // Será hasheada no serviço
    }

    // --- Para PUT (Atualização) ---
    public class UpdateRequest
    {
        [Required(ErrorMessage = "ID do usuário é obrigatório!")]
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        public required string Nome { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }
    }

    // --- Para GET (Resposta) ---
    public class Response
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public int PerfilId { get; set; } // APENAS o ID (evita referência circular)
    }
}