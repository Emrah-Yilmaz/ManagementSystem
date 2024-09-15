namespace ManagementSystem.Domain.Services.Abstract.City
{
    public interface ILocationService : IDomainService
    {
        Task<bool> CreateCityAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateDistrictsAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateQuartersAsync(CancellationToken cancellationToken = default);
    }
}
