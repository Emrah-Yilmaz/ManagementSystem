using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.Location
{
    public interface ILocationService : IDomainService
    {
        Task<bool> CreateCityAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateDistrictsAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateQuartersAsync(CancellationToken cancellationToken = default);
        Task<List<CityDto>> GetCitiesAsync(CancellationToken cancellationToken = default);
        Task<List<DistrictDto>> GetDistrictsByCityIdAsync(int cityId, CancellationToken cancellationToken = default);
        Task<List<QuarterDto>> GetQuartersByDistrictIdAsync(int districtId, CancellationToken cancellationToken = default);
    }
}
