using ManagementSystem.Domain.Services.Abstract.City;
using ManagementSystem.Domain.Services.Abstract.Comment;
using ManagementSystem.Domain.Services.Abstract.Department;
using ManagementSystem.Domain.Services.Abstract.User;
using ManagementSystem.Domain.Services.Abstract.WorkTask;
using ManagementSystem.Domain.Services.Concrete.City;
using ManagementSystem.Domain.Services.Concrete.Comment;
using ManagementSystem.Domain.Services.Concrete.Department;
using ManagementSystem.Domain.Services.Concrete.User;
using ManagementSystem.Domain.Services.Concrete.WorkTask;
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
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILocationService, LocationService>();
            return services;
        }
    }
}
