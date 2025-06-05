using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;

namespace UserManagement.API.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateTask(TaskDto.CreateRequest dto)
        {
            var task = new TaskItem
            {
                Nome = dto.Nome,
                Setor = dto.Setor,
                Data = dto.Data,
                HoraFinalizacao = dto.HoraFinalizacao,
                UserId = dto.UserId,
                Status = TaskStatus.Agendada
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<bool> UpdateTaskStatus(TaskDto.UpdateStatusRequest dto)
        {
            var task = await _context.Tasks.FindAsync(dto.TaskId);
            if (task == null) return false;

            if (Enum.TryParse<TaskStatus>(dto.Status, true, out var newStatus))
            {
                task.Status = newStatus;
                task.EvidenciaUrl = dto.EvidenciaUrl;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
