namespace ManagementSystem.Domain.Models.Args.WorkTask
{
    public class GetWorkTasksArgs
    {
        public string? Title { get; set; }
        public string? AssignedUser { get; set; }
        public string? Department { get; set; }
        public string? Status { get; set; }
        public DateTime? Deadline { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
