using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Application.Events.DepartmentEvents
{
    public class SendEmailEvent : INotification
    {
        public int Id { get; set; }
    }
}
