namespace ManagementSystem.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public virtual User? User { get; set; }
        public int UserId { get; set; }

        public virtual WorkTask? Task { get; set; }
        public int TaskId { get; set; }
    }
}
