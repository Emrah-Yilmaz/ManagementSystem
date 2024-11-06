using Core.Application.Pipelines.Caching;
using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementSystem.Application.Features.Queries.WorkTask;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Packages.Loggings.SeriLog;
using Packages.Loggings.SeriLog.Loggers;
using Packages.Pipelines.Authorization;
using Packages.Pipelines.Caching;
using Packages.Pipelines.Logging;
using Packages.Pipelines.Validation;
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
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            });

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<GetWorkTasksQueryValidator>();
            services.AddSingleton<LoggerServiceBase, FileLogger>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
