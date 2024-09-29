namespace ManagementSystem.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Department>? Departments { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<WorkTask>? WorkTasks { get; set; }
    }
}
