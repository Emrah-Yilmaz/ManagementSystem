using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.User
{
    public interface IUserService : IDomainService
    {
        public Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default);
        public Task<int> CreateAsync(CreateUserArgs args, CancellationToken cancellationToken = default);
        public Task<bool> CreateUserAddressAsync(CreateAddressArgs args, CancellationToken cancellationToken = default);
        Task<List<UserDto>> GetUsers(CancellationToken cancellationToken = default);
        Task<bool> AddUserToDepartment(AddUserToDepartmentArgs args, CancellationToken cancellationToken = default);
        Task<bool> CreateUsersWithBogus(CancellationToken cancellationToken = default);
    }
}