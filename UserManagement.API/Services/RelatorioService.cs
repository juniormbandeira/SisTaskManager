using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.API.Services;

public class RelatorioService
{
    private readonly AppDbContext _context;
    private readonly ILogger<PerfilService> _logger;

    public RelatorioService(AppDbContext context, ILogger<PerfilService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // --- Cria um novo perfil (vinculado a um User) ---
    public async Task<PerfilDto.Response> CreateRelatorio()
    {
        try
        {
            var tarefas = await _context.Tarefas.ToListAsync();

            var relatorio = tarefas.Select(t => new RelatorioDto.Response
            {
                // Mapeia os campos aqui, ex:
                // Nome = t.Nome,
                // Setor = t.Setor,
                // DataExecucao = t.Data,
                // Status = t.Status
            }).ToList();

            return relatorio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar Relatorio");
            throw;
        }
    }
}