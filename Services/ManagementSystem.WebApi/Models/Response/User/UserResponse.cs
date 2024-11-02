using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.Department;
using ManagementSystem.WebApi.Models.Response.Location;

namespace ManagementSystem.WebApi.Models.Response.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<AddressResponse>? Addresses { get; set; }
        public DepartmentOfUserResponse? Department { get; set; }
    }
}
