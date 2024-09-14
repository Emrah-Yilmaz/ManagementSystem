using CommonLibrary.Messages;
using MassTransit;
using MediatR;

namespace ManagementSystem.Application.Events.DepartmentEvents
{
    public class SendEmailEventHandler : INotificationHandler<SendEmailEvent>
    {
        private readonly IBusControl _busControl;

        public SendEmailEventHandler(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task Handle(SendEmailEvent notification, CancellationToken cancellationToken)
        {
            await _busControl.Publish(new CreatedDepartmentMessage
            {
                Id = 1
            }, cancellationToken);
        }
    }
}
