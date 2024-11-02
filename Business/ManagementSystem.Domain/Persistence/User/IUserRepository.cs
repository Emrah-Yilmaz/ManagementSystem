namespace ManagementSystem.Domain.Persistence.User
{
    public interface IUserRepository : IRepository<Domain.Entities.User>
    {
        Task<List<Domain.Entities.User>> GetUserWithProjectsAsync();

    }
}
