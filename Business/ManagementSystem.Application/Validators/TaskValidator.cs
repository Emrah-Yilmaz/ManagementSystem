using FluentValidation;
using ManagementSystem.WebApi.Models.WorkTask.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Application.Validators
{
    public class TaskValidator : AbstractValidator<WorkTasksDto>
    {
        public TaskValidator() { }
    }
}
