using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Models.Args.Department
{
    public class GetDepartmentsArgs
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

