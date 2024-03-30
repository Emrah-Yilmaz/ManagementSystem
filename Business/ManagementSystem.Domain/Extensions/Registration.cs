using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.Domain.Services.Concrete;
using ManagementSystem.Domain.TokenHandler;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementSystem.Domain.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddDomainRegistration(this IServiceCollection services)
        {
            services.AddScoped<IWorkTaskService, WorkTaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDomainPrincipal, DomainPrincipal>();
            return services;
        }
    }
}
