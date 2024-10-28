using CommonLibrary.Messages;
using CommonLibrary.Templates.Mail;
using ManagementSystem.Notification.Services;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ManagementSystem.Notification.Consumers.EmailConsumers
{
    public class SendEmailConsumer : IConsumer<CreatedDepartmentMessage>
    {
        public IEmailService _emailService;

        public SendEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Consume(ConsumeContext<CreatedDepartmentMessage> context)
        {
            var template = string.Format(MailTemplate.CreatedDepartment, context.Message.CreatedOn, context.Message.CreatedBy, context.Message.Id, context.Message.DepartmentName);
            _emailService.SendEmailAsync(context.Message.To, context.Message.Subject, template);
            return Task.CompletedTask;
        }
    }
}
