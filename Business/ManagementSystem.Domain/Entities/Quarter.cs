namespace ManagementSystem.Domain.Entities
{
    public class Quarter : BaseEntity
    {
        public string Name { get; set; }
        public required int DistrictId { get; set; }
        public  virtual District District { get; set; }
    }
}
