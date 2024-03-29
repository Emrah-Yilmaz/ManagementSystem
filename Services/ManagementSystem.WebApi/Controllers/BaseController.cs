using ManagementSystem.Domain.Models;
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
                Id = Convert.ToInt32(tokenInfo["userId"]),
                Name = tokenInfo["firstName"],
                LastName = tokenInfo["lastName"],
                UserName = tokenInfo["username"],
                Email = tokenInfo["mailAddress"]
            };

            return model;
        }
    }
}
