using ManagementSystem.Domain.Models;
using ManagementSystem.Domain.Utilities;
using Microsoft.AspNetCore.Http;

namespace ManagementSystem.Domain.TokenHandler
{
    public class DomainPrincipal : IDomainPrincipal
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DomainPrincipal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public  DomainPrincipalModel GetClaims()
        {
            var model = new DomainPrincipalModel
            {
                Id = Convert.ToInt32(_httpContextAccessor?.HttpContext.User?.Claims?.FirstOrDefault(p => p.Type == Shared.JwtClaims.UserId)?.Value),
                Name = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(p => p.Type == Shared.JwtClaims.FirstName)?.Value ?? Shared.JwtClaims.Unknown,
                LastName = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(p => p.Type == Shared.JwtClaims.LastName)?.Value ?? Shared.JwtClaims.Unknown,
                UserName = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(p => p.Type == Shared.JwtClaims.UserName)?.Value ?? Shared.JwtClaims.Unknown,
                Email = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(p => p.Type == Shared.JwtClaims.Email)?.Value ?? Shared.JwtClaims.Unknown
            };

            return model;
        }

    }
}

