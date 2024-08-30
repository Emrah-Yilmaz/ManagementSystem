using Packages.Exceptions.Types;

namespace Packages.Exceptions.Handlers
{
    public abstract class ExceptionHandler
    {
        public Task HandleExceptionAsync(Exception exception) =>
            exception switch
            {
                BusinessException businessException => HandleException(businessException),
                ValidationException validationException => HandleException(validationException),
                _ => HandleException(exception)
            };
        protected abstract Task HandleException(BusinessException validationException);
        protected abstract Task HandleException(ValidationException validationException);
        protected abstract Task HandleException(Exception exception);
    }
}
