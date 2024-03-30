using ManagementSystem.Domain.Models;

namespace ManagementSystem.Domain.TokenHandler
{
    public interface IDomainPrincipal
    {
        public DomainPrincipalModel GetClaims();
    }
}
