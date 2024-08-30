using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Packages.Exceptions.HttpProblemDetails
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public BusinessProblemDetails(string detail)
        {
            Title = "Rule Violation";
            Detail = detail;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example/props/business";
        }
    }
}
