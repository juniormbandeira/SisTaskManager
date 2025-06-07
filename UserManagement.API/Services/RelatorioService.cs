using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserManagement.API.Services
{
    public class RelatorioService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RelatorioService> _logger;

        public RelatorioService(AppDbContext context, ILogger<RelatorioService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gera um relatório de tarefas em ordem cronológica,
        /// com opção de filtro por período (data inicial e final)
        /// </summary>
        /// <param name="dataInicial">Filtrar tarefas a partir desta data (opcional)</param>
        /// <param name="dataFinal">Filtrar tarefas até esta data (opcional)</param>
        /// <returns>Lista de RelatorioDto.Response</returns>
        public async Task<List<RelatorioDto.Response>> CreateRelatorio(
            DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            try
            {
                // Busca as tarefas do banco
                var query = _context.Tarefas.AsQueryable();

                // Aplica filtro pela data inicial, se fornecido
                if (dataInicial.HasValue)
                    query = query.Where(t => t.DataCriacao >= dataInicial.Value);

                // Aplica filtro pela data final, se fornecido
                if (dataFinal.HasValue)
                    query = query.Where(t => t.DataCriacao <= dataFinal.Value);

                // Ordena tarefas por data de criação (crescente)
                query = query.OrderBy(t => t.DataCriacao);

                var tarefas = await query.ToListAsync();

                // Mapeamento para DTO do relatório
                var relatorio = tarefas.Select(t => new RelatorioDto.Response
                {
                    // Exemplo de mapeamento
                    Nome = t.Nome,
                    Setor = t.Setor,
                    DataExecucao = t.DataCriacao,
                    Status = t.Status
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
}