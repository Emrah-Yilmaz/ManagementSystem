namespace ManagementSystem.Domain.Entities
{
    public class WorkTask : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
