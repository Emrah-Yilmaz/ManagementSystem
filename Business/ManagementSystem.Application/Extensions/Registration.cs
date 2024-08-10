using Core.Application.Pipelines.Caching;
using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementSystem.Application.Features.Queries.WorkTask;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Packages.Pipelines.Caching;
using System.Reflection;

namespace ManagementSystem.Application.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
                configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            });

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<GetWorkTasksQueryValidator>();
            return services;
        }
    }
}
