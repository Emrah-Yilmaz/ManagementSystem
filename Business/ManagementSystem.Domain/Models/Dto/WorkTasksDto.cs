namespace ManagementSystem.Domain.Models.Dto
{
    public class WorkTasksDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime? Deadline { get; set; }
        public string Status { get; set; }
        public string NameSurname { get; set; }
        public string Department { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedBy { get; set; }
        public int? ModifiedById { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
