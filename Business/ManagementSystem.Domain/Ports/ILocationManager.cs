using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Ports
{
    public interface ILocationManager
    {
        Task<string> GetCitiesAsync(CancellationToken cancellationToken = default);
        Task<DistrictsApiResponse> GetDistrictsAsync(string cityName, CancellationToken cancellationToken = default);
        Task<QuarterApiResponse> GetQuartersAsync(CancellationToken cancellationToken = default);
    }
}
