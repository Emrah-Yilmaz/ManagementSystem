namespace ManagementSystem.Domain.Entities
{
    public class Address : BaseEntity
    {
        public int UserId { get; set; }
        public virtual required ICollection<User> Users { get; set; }

        public int CityId { get; set; }
        public virtual required City City { get; set; }

        public int DistrictId { get; set; }

        public int QuerterId  { get; set; }

        public int StreetId { get; set; }
    }
}
