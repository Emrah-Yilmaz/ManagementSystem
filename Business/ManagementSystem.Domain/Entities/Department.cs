using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User>? Users { get; set; }

        public virtual ICollection<Project>? Projects { get; set; }
    }
}
