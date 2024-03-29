using ManagementSystem.Domain.Models.Args;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract
{
    public interface IUserService : IDomainService
    {
        public Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default);
        public Task<int> CreateAsync(CreateUserArgs args, CancellationToken cancellationToken = default);
    }
}
