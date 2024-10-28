using CommonLibrary.Templates.Mail;
using ManagementSystem.Application.Events.DepartmentEvents;
using ManagementSystem.Domain.Services.Abstract.Department;
using ManagementSystem.Domain.TokenHandler;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Department.Create
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartmentService _service;
        private readonly IMediator _mediator;
        private readonly IDomainPrincipal _domainPrincipal;

        public CreateDepartmentCommandHandler(IDepartmentService service, IMediator mediator, IDomainPrincipal domainPrincipal)
        {
            _service = service;
            _mediator = mediator;
            _domainPrincipal = domainPrincipal;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request, cancellationToken);
            var @eventCreateDepartment = new SendEmailEvent()
            {
                Id = result,
                DepartmentName = request.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = $"{_domainPrincipal.GetClaims().Name} {_domainPrincipal.GetClaims().LastName}",
                Subject = "Departman Oluşturuldu",
                To = MailTemplate.Admin
            };

            await _mediator.Publish(eventCreateDepartment, cancellationToken);
            return result;
        }
    }
}