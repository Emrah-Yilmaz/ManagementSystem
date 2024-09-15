namespace ManagementSystem.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int CityCode { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
