using UserManagement.API.Data;
using UserManagement.API.DTOs;
using Microsoft.EntityFrameworkCore;
using UserManagement.API.Models;

namespace UserManagement.API.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Método para criar usuário
    public async Task<UserDto.Response> CreateUser(UserDto.CreateRequest dto)
    {
        // Validações adicionais (ex: email único)
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("Email já cadastrado!");

        // Mapear DTO para a entidade User
        var user = new User
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha), // Hash da senha!
            DataCriacao = DateTime.UtcNow
        };

        // Salvar no banco
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Retornar DTO de resposta
        return new UserDto.Response
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            DataCriacao = user.DataCriacao
        };
    }

    public async Task<UserDto.Response> UpdateUser(int id, UserDto.UpdateRequest dto)
{
    var user = await _context.Users.FindAsync(id);

    if (user == null)
        throw new Exception("Usuário não encontrado.");

    // Verifica se o email está sendo alterado e se já existe no banco
    if (user.Email != dto.Email && await _context.Users.AnyAsync(u => u.Email == dto.Email))
        throw new Exception("Email já está em uso por outro usuário.");

    // Atualiza os campos
    user.Nome = dto.Nome;
    user.Email = dto.Email;

    // Atualiza a senha apenas se foi enviada
    if (!string.IsNullOrWhiteSpace(dto.Senha))
        user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

    _context.Users.Update(user);
    await _context.SaveChangesAsync();

    return new UserDto.Response
    {
        Id = user.Id,
        Nome = user.Nome,
        Email = user.Email,
        DataCriacao = user.DataCriacao,
        PerfilId = user.PerfilId
    };
}


    // Dentro da classe UserService, adicione:
    public async Task<UserDto.Response?> GetUserById(int id)
    {
        // Busca o usuário no banco de dados pelo Id (incluindo o Perfil se existir)
        var user = await _context.Users
            .Include(u => u.Perfil) // Carrega o relacionamento com Perfil (se necessário)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null; // Retorna null se o usuário não existir

        // Mapeia a entidade User para UserDto.Response
        return new UserDto.Response
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            DataCriacao = user.DataCriacao,
            PerfilId = user.PerfilId // Opcional: inclua o ID do perfil se necessário
        };
    }

    // Outros métodos (Update, Delete...)
}