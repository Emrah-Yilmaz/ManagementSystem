namespace ManagementSystem.Domain.Entities
{
    public class Address : BaseEntity
    {
        public int UserId { get; set; }

        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public int QuerterId  { get; set; }

        public string Description { get; set; }
    }
}
