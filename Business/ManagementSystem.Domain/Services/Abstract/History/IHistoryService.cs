using ManagementSystem.Domain.Models.Args.History;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.History
{
    public interface IHistoryService : IDomainService
    {
        Task<List<HistoryDto>> GetHistoriesAsync(HistoryArgs args, CancellationToken cancellationToken = default);
    }
}
