using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementSystem.Application.Features.Queries.WorkTask;
using ManagementSystem.Application.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ManagementSystem.Application.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetWorkTasksQueryHandler).GetTypeInfo().Assembly));
            services.AddValidatorsFromAssemblyContaining<TaskValidator>();
            return services;
        }
    }
}
