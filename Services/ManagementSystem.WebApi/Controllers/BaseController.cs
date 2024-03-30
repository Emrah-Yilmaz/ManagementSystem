using ManagementSystem.Domain.Models;
using ManagementSystem.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public DomainPrincipalModel ReadJwtToken()
        {
            var claims = HttpContext.User.Claims;
            var tokenInfo = new Dictionary<string, string>();

            foreach (var claim in claims)
            {
                tokenInfo.Add(claim.Type, claim.Value);
            }

            var model = new DomainPrincipalModel
            {
                Id = Convert.ToInt32(tokenInfo[Shared.JwtClaims.UserId]),
                Name = tokenInfo[Shared.JwtClaims.FirstName],
                LastName = tokenInfo[Shared.JwtClaims.LastName],
                UserName = tokenInfo[Shared.JwtClaims.UserName],
                Email = tokenInfo[Shared.JwtClaims.Email]
            };

            return model;
        }
    }
}
