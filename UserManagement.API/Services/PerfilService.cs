'using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.API.Services;

public class PerfilService
{
    private readonly AppDbContext _context;
    private readonly ILogger<PerfilService> _logger;

    public PerfilService(AppDbContext context, ILogger<PerfilService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // --- Cria um novo perfil (vinculado a um User) ---
    public async Task<PerfilDto.Response> CreatePerfil(PerfilDto.CreateRequest dto)
    {
        try
        {
            // Cria o perfil
            var perfil = new Perfil
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            _context.Perfis.Add(perfil);
            await _context.SaveChangesAsync();

            return new PerfilDto.Response
            {
                Id = perfil.Id,
                Nome = perfil.Nome,
                Descricao = perfil.Descricao,
                Usuarios = perfil.Usuarios.Select(u => new PerfilDto.UserShortResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar perfil");
            throw;
        }
    }

    // --- Atualiza um perfil existente ---
    public async Task<PerfilDto.Response> UpdatePerfil(int id, PerfilDto.UpdateRequest dto)
    {
        var perfil = await _context.Perfis
            .Include(p => p.Usuarios)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (perfil == null)
            throw new KeyNotFoundException("Perfil não encontrado!");

        perfil.Nome = dto.Nome;
        perfil.Descricao = dto.Descricao;

        await _context.SaveChangesAsync();

        return new PerfilDto.Response
        {
            Id = perfil.Id,
            Nome = perfil.Nome,
            Descricao = perfil.Descricao,
            Usuarios = perfil.Usuarios.Select(u => new PerfilDto.UserShortResponse
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList()
        };
    }

    // --- Obtém perfil por ID com usuários vinculados ---
    public async Task<PerfilDto.Response?> GetPerfilById(int id)
    {
        var perfil = await _context.Perfis
            .Include(p => p.Usuarios)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (perfil == null)
            return null;

        return new PerfilDto.Response
        {
            Id = perfil.Id,
            Nome = perfil.Nome,
            Descricao = perfil.Descricao,
            Usuarios = perfil.Usuarios.Select(u => new PerfilDto.UserShortResponse
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList()
        };
    }
    //desabilita um usuario
    public async Task DesabilitarUsuario(int usuarioId)
{
    var usuario = await _context.Usuarios.FindAsync(usuarioId);

    if (usuario == null)
        throw new KeyNotFoundException("Usuário não encontrado!");

    if (!usuario.Ativo)
        throw new InvalidOperationException("Usuário já está desabilitado.");

    usuario.Ativo = false;
    await _context.SaveChangesAsync();
}



    // --- Deleta um perfil (soft delete ou hard delete) ---
    public async Task DeletePerfil(int id)
    {
        var perfil = await _context.Perfis.FindAsync(id);
        if (perfil == null)
            throw new KeyNotFoundException("Perfil não encontrado!");

        _context.Perfis.Remove(perfil); // Hard delete
        await _context.SaveChangesAsync();
    }
}'