namespace ManagementSystem.Domain.Entities
{
    public class Address : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual City City { get; set; }
        public int CityId { get; set; }

        public virtual District District { get; set; }
        public int DistrictId { get; set; }

        public virtual Quarter Quarter { get; set; }
        public int QuerterId  { get; set; }

        public string Description { get; set; }
    }
}
