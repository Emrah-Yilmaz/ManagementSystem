namespace ManagementSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<WorkTask>? WorkTasks { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public virtual ICollection<Address>? Addresses { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
