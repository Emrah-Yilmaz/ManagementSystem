using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Packages.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static string AsJson<TProblemDetails>(this TProblemDetails details)
            where TProblemDetails : ProblemDetails => JsonSerializer.Serialize(details);
    }
}
