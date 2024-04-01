namespace ManagementSystem.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<WorkTask>? WorkTasks { get; set; }
    }
}
