using CommonLibrary.Messages;
using MassTransit;

namespace ManagementSystem.Notification.Consumers.EmailConsumers
{
    public class SendEmailConsumer : IConsumer<CreatedDepartmentMessage>
    {
        public Task Consume(ConsumeContext<CreatedDepartmentMessage> context)
        {
            // TODO: Email sending feature will be added 
            return Task.CompletedTask;
        }
    }
}
