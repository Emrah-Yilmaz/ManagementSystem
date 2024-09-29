namespace ManagementSystem.Domain.Persistence.User
{
    public interface IUserRepository : IGenericRepository<Domain.Entities.User>
    {
        Task<List<Domain.Entities.User>> GetUserWithProjectsAsync();

    }
}
