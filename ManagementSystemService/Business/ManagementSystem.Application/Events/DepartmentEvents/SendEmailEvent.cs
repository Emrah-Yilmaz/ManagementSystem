using MediatR;

namespace ManagementSystem.Application.Events.DepartmentEvents
{
    public class SendEmailEvent : INotification
    {
        public int Id { get; set; }
    }
}
