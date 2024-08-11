using AutoMapper;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Department.Delete
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, int>
    {
        private readonly IDepartmentService _service;
        private readonly IMapper _mapper;
        public DeleteDepartmentCommandHandler(IDepartmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var mappedArgs = _mapper.Map<GetDepartmentArgs>(request);
            var result = await _service.Deletesync(mappedArgs, cancellationToken);
            return result;
        }

    }
}
