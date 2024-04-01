namespace ManagementSystem.Domain.Models.Args.WorkTask
{
    public class UpdateWorkTaskArgs
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public int? AssignedUserId { get; set; }
        public int? DepartmentId { get; set; }
    }
}