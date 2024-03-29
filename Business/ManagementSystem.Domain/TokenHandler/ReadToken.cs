using System.IdentityModel.Tokens.Jwt;

namespace ManagementSystem.Domain.TokenHandler
{
    public class  ReadToken
    {
        public static JwtSecurityToken ReadJwtToken(string token)
        {
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            return tokenS;
        }
    }
}
