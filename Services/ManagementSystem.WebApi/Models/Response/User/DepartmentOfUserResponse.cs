using ManagementSystem.WebApi.Models.Response.Department;

namespace ManagementSystem.WebApi.Models.Response.User
{
    public class DepartmentOfUserResponse 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ProjectResponse>? Projects { get; set; }
    }
}
