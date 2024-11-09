using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.User
{
    public interface IUserService : IDomainService
    {
        public Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default);
        public Task<int> CreateAsync(CreateUserArgs args, CancellationToken cancellationToken = default);
        public Task<bool> CreateUserAddressAsync(CreateAddressArgs args, CancellationToken cancellationToken = default);
        Task<List<UserDto>> GetUsers(GetUserArgs args, CancellationToken cancellationToken = default);
        Task<UserDto> GetUser(int userId, CancellationToken cancellationToken = default);
        Task<bool> AddUserToDepartment(AddUserToDepartmentArgs args, CancellationToken cancellationToken = default);
        Task<bool> AssignUserToProjectAsync(AssignUserToProjectArgs args, CancellationToken cancellationToken = default);
        Task<bool> CreateUsersWithBogus(CancellationToken cancellationToken = default);
        Task<int> UpdateUserAddressAsync(UpdateAddressArgs args, CancellationToken cancellationToken = default);
        Task<bool> ChangeStatusAsync(ChangeStatusArgs args, CancellationToken cancellationToken = default);
    }
}