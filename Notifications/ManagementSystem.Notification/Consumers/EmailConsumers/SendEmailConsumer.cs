using CommonLibrary.Messages;
using MassTransit;

namespace ManagementSystem.Notification.Consumers.EmailConsumers
{
    public class SendEmailConsumer : IConsumer<CreatedDepartmentMessage>
    {
        public Task Consume(ConsumeContext<CreatedDepartmentMessage> context)
        {
            // ok 
            return Task.CompletedTask;
        }
    }
}
