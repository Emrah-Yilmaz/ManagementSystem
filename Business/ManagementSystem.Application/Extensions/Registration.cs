using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementSystem.Application.Features.Queries.WorkTask;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ManagementSystem.Application.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetWorkTasksQueryHandler).GetTypeInfo().Assembly));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<GetWorkTasksQueryValidator>();
            return services;
        }
    }
}
