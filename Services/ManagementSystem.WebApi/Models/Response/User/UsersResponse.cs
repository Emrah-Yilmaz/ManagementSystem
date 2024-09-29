using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.WebApi.Models.Response.User
{
    public class UsersResponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public List<ProjectResponse> Projects { get; set; }
    }
}
