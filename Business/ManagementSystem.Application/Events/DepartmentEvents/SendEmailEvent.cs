using CommonLibrary.Messages;
using MediatR;

namespace ManagementSystem.Application.Events.DepartmentEvents
{
    public class SendEmailEvent : CreatedDepartmentMessage, INotification
    {
    }
}
