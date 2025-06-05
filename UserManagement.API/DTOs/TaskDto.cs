namespace UserManagement.API.DTOs
{
    public class TaskDto
    {
        public class CreateRequest
        {
            public string Nome { get; set; } = null!;
            public string Setor { get; set; } = null!;
            public DateTime Data { get; set; }
            public DateTime HoraFinalizacao { get; set; }
            public int UserId { get; set; }
        }

        public class UpdateStatusRequest
        {
            public int TaskId { get; set; }
            public string Status { get; set; } = null!;
            public string? EvidenciaUrl { get; set; }
        }
    }
}
