using ManagementSystem.Domain.Persistence.City;
using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Persistence.History;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Domain.Persistence.WorkTask;
using ManagementSystem.Domain.Ports;
using ManagementSystem.Infrastructure.Adapters;
using ManagementSystem.Infrastructure.Context;
using ManagementSystem.Infrastructure.Persistence;
using ManagementSystem.Infrastructure.Persistence.City;
using ManagementSystem.Infrastructure.Persistence.Comment;
using ManagementSystem.Infrastructure.Persistence.Department;
using ManagementSystem.Infrastructure.Persistence.History;
using ManagementSystem.Infrastructure.Persistence.Location;
using ManagementSystem.Infrastructure.Persistence.Project;
using ManagementSystem.Infrastructure.Persistence.User;
using ManagementSystem.Infrastructure.Persistence.WorkTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ManagementSystem.Infrastructure.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services,
                                                                        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(conf =>
            {
                var connStr = configuration["SqlConnection"].ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDepartmentRepository, DeparmentRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ILocationManager, LocationAdapter>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IQuarterRepository, QuarterRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // TContext burada AppDbContext olacak

            return services;
        }
    }
}
