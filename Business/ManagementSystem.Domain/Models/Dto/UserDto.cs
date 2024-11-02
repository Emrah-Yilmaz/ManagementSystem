namespace ManagementSystem.Domain.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
        public List<AddressDto>? Addresses { get; set; }
    }
}
