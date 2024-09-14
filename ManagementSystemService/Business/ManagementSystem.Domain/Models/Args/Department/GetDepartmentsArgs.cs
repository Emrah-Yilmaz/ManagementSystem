using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Models.Args.Department
{
    public class GetDepartmentsArgs : BasePagedQuery
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

