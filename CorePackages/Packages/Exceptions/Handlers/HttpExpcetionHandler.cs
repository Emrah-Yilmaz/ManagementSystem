using Microsoft.AspNetCore.Http;
using Packages.Exceptions.Converters;
using Packages.Exceptions.HttpProblemDetails;
using Packages.Exceptions.Types;
using Packages.Extensions;
using System.Net;
using System.Text.Json;

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
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            Converters = { new SpecialCharacterRemovingConverter() },
            WriteIndented = true
        };

        protected override Task HandleException(ValidationException validationException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            var errorResponse = new
            {
                Message = validationException.Message,
                Errors = validationException.Errors
            };
            return Response.WriteAsync(JsonSerializer.Serialize(errorResponse, _jsonOptions));
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
