using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ManagementSystem.WebApi.Configurations.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum = context.Type
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(f => new OpenApiString(f.Name))
                    .Cast<IOpenApiAny>()
                    .ToList();
            }
        }
    }
}
