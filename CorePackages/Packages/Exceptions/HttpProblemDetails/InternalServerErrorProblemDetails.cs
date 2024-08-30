using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Packages.Exceptions.HttpProblemDetails
{
    public class InternalServerErrorProblemDetails : ProblemDetails
    {
        public InternalServerErrorProblemDetails(string detail)
        {
            Title = "Internal Server Error Violation";
            Detail = detail;
            Status = StatusCodes.Status500InternalServerError;
            Type = "https://example/props/internal";
        }
    }
}
