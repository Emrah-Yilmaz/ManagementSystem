using ManagementSystem.Domain.Persistence;
using ManagementSystem.Infrastructure.Context;
using ManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
            services.AddScoped<DbContext, AppDbContext>();

            return services;
        }
    }
}
