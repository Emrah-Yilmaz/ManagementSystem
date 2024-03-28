using ManagementSystem.Application.Features.Queries.WorkTask;
using System.Reflection;

namespace ManagementSystem.WebApi.Extensions
{
    public static class Registration
    {
        public static void AddWebApiRegistration(this IServiceCollection services)
        {
            var assemblies = new Assembly[]
            {
                typeof(Program).Assembly,
                typeof(Domain.Initializer).Assembly,
            };
            services.AddAutoMapper(config =>
            {
                config.AllowNullCollections = true;
            },assemblies);
        }
    }
}
