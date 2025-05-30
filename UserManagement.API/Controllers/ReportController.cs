using System.Collections.Generic;
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
        // Chama o serviço que retorna a lista
        var Relatorio = _reportService.CreateRelatorio();
        return Ok(tarefas);
    }
}
