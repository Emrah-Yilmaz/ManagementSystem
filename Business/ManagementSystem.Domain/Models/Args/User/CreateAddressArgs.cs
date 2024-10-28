namespace ManagementSystem.Domain.Models.Args.User
{
    public partial class CreateAddressArgs
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int QuarterId { get; set; }
        public int StreetId { get; set; }
        public string? Description { get; set; }
    }
}
