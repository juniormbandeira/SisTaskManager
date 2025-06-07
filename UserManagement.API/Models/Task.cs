namespace UserManagement.API.Models
{
    public enum TaskStatus
    {
        Agendada,
        EmAndamento,
        Realizada,
        Cancelada
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Setor { get; set; } = null!;
        public DateTime Data { get; set; }
        public DateTime HoraFinalizacao { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Agendada;
        public string? EvidenciaUrl { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
