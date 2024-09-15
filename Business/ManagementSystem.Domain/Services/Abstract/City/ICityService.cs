namespace ManagementSystem.Domain.Services.Abstract.City
{
    public interface ICityService : IDomainService
    {
        Task<bool> CreateAsync(CancellationToken cancellationToken = default);
    }
}
