using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.Domain.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementSystem.Domain.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddDomainRegistration(this IServiceCollection services)
        {
            services.AddScoped<IWorkTaskService, WorkTaskService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
