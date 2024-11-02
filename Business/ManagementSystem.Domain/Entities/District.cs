using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Domain.Entities
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public required int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Quarter> Quarters { get; set; }
    }
}
