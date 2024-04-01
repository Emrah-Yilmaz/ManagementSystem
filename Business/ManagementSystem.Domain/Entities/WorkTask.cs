using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Domain.Entities
{
    public class WorkTask : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int AssignedUserId { get; set; }
        public virtual User? AssignedUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
