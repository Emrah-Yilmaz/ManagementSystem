using ManagementSystem.WebApi.Models.Response.User;

namespace ManagementSystem.WebApi.Models.Response.Department
{
    public class UsesrByDepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int WorkersCount { get; set; }
        public IList<UserInfoResponse> UserInfoResposnes { get; set; }
    }
}
