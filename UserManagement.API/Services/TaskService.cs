using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;
using UserManagement.API.Exceptions;

namespace UserManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(AppDbContext context, ILogger<TaskService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TaskItem> CreateTaskAsync(TaskDto.CreateRequest dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            _logger.LogInformation("Creating new task for user {UserId}", dto.UserId);

            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
            {
                throw new InvalidOperationException($"User with ID {dto.UserId} not found");
            }

            var task = new TaskItem
            {
                Nome = dto.Nome?.Trim(),
                Setor = dto.Setor?.Trim(),
                Data = dto.Data,
                HoraFinalizacao = dto.HoraFinalizacao,
                UserId = dto.UserId,
                Status = TaskStatus.Agendada,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task created successfully with ID {TaskId}", task.Id);
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task for user {UserId}", dto.UserId);
                throw new TaskOperationException("Failed to create task", ex);
            }
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving task with ID {TaskId}", id);
            
            try
            {
                return await _context.Tasks
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task with ID {TaskId}", id);
                throw new TaskOperationException($"Failed to retrieve task with ID {id}", ex);
            }
        }

        public async Task<bool> UpdateTaskStatusAsync(TaskDto.UpdateStatusRequest dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            _logger.LogInformation("Updating status for task {TaskId} to {Status}", dto.TaskId, dto.Status);

            var task = await _context.Tasks.FindAsync(dto.TaskId);
            if (task == null)
            {
                _logger.LogWarning("Task with ID {TaskId} not found", dto.TaskId);
                return false;
            }

            if (!Enum.TryParse<TaskStatus>(dto.Status, true, out var newStatus))
            {
                _logger.LogWarning("Invalid status value: {Status}", dto.Status);
                return false;
            }

            try
            {
                task.Status = newStatus;
                task.EvidenciaUrl = dto.EvidenciaUrl;
                task.UpdatedAt = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task status updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task status for task {TaskId}", dto.TaskId);
                throw new TaskOperationException($"Failed to update task status for task {dto.TaskId}", ex);
            }
        }
    }
}
