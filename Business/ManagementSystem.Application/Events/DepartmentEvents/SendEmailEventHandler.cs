using AutoMapper;
using CommonLibrary.Messages;
using MassTransit;
using MediatR;

namespace ManagementSystem.Application.Events.DepartmentEvents
{
    public class SendEmailEventHandler : INotificationHandler<SendEmailEvent>
    {
        private readonly IBusControl _busControl;
        private readonly IMapper _mapper;

        public SendEmailEventHandler(IBusControl busControl, IMapper mapper)
        {
            _busControl = busControl;
            _mapper = mapper;
        }

        public async Task Handle(SendEmailEvent notification, CancellationToken cancellationToken)
        {
            var mappedMessage = _mapper.Map<CreatedDepartmentMessage>(notification);
            await _busControl.Publish(mappedMessage, cancellationToken);
        }
    }
}
