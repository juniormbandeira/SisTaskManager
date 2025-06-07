using Microsoft.AspNetCore.Mvc;
using UserManagement.API.DTOs;
using UserManagement.API.Services;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto.CreateRequest dto)
        {
            try
            {
                var task = await _taskService.CreateTask(dto);
                return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return task != null ? Ok(task) : NotFound();
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] TaskDto.UpdateStatusRequest dto)
        {
            var success = await _taskService.UpdateTaskStatus(dto);
            return success ? Ok() : BadRequest("Tarefa não encontrada ou status inválido.");
        }
    }
}
