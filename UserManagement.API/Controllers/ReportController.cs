using System.Collections.Generic;
using System.Linq; // Necessário para usar .OrderBy()
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("tarefas")]
    public ActionResult<List<TarefaDTO>> CreateRelatorio()
    {
        // Chama o serviço que retorna a lista de tarefas
        var tarefas = _reportService.CreateRelatorio();

        // Organiza a lista em ordem cronológica (da mais antiga para a mais nova)
        // Substitua "DataCriacao" pelo nome da propriedade de data das suas tarefas, se for diferente.
        var tarefasOrdenadas = tarefas.OrderBy(t => t.DataCriacao).ToList();

        // Retorna a lista organizada
        return Ok(tarefasOrdenadas);
    }
}