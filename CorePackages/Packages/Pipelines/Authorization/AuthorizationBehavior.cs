using MediatR;
using Microsoft.AspNetCore.Http;

namespace Packages.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IRequireAuthorization authorizationRequest)
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null || !user.IsInRole(authorizationRequest.RequiredRole))
                {
                    throw new UnauthorizedAccessException($"User does not have the required role: {authorizationRequest.RequiredRole}");
                }
            }

            return await next();
        }
    }

}
