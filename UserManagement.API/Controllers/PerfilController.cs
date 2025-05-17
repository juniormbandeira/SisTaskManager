using Microsoft.AspNetCore.Mvc;
using UserManagement.API.DTOs;
using UserManagement.API.Services;

[ApiController]
[Route("api/[controller]")]
public class PerfisController : ControllerBase
{
    private readonly PerfilService _perfilService;

    public PerfisController(PerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PerfilDto.CreateRequest dto)
    {
        try
        {
            var perfil = await _perfilService.CreatePerfil(dto);
            return CreatedAtAction(nameof(GetById), new { id = perfil.Id }, perfil);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var perfil = await _perfilService.GetPerfilById(id);
        return perfil != null ? Ok(perfil) : NotFound();
    }
}