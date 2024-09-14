using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDeparmentsQueryHandler : IRequestHandler<GetDeparmentsQuery, PagedViewModel<DepartmentDto>>
    {
        private readonly IDepartmentService _service;

        public GetDeparmentsQueryHandler(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<PagedViewModel<DepartmentDto>> Handle(GetDeparmentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetDepartments(request, cancellationToken);
            return result;
        }
    }
}
