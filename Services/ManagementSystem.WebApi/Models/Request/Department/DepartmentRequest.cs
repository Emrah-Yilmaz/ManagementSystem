namespace ManagementSystem.WebApi.Models.Request.Department
{
    public class DepartmentRequest
    {
        public string? Name { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
