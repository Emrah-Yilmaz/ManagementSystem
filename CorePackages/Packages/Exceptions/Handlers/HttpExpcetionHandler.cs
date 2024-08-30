using Microsoft.AspNetCore.Http;
using Packages.Exceptions.HttpProblemDetails;
using Packages.Exceptions.Types;
using Packages.Extensions;

namespace Packages.Exceptions.Handlers
{
    public class HttpExpcetionHandler : ExceptionHandler
    {
        private HttpResponse? _response;
        public HttpResponse Response
        {
            get => _response ?? throw new ArgumentNullException(nameof(_response));
            set => _response = value;
        }
        protected override Task HandleException(ValidationException validationException)
        {
            throw new NotImplementedException();
        }

        protected override Task HandleException(Exception exception)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            string details = new InternalServerErrorProblemDetails(exception.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(BusinessException businessException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new BusinessProblemDetails(businessException.Message).AsJson();
            return Response.WriteAsync(details);
        }

    }
}
