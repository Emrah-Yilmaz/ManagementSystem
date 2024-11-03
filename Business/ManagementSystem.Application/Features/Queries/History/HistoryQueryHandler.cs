using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.History;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.History
{
    public class HistoryQueryHandler : IRequestHandler<HistoryQuery, List<HistoryDto>>
    {
        private readonly IHistoryService _historyService;

        public HistoryQueryHandler(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<List<HistoryDto>> Handle(HistoryQuery request, CancellationToken cancellationToken)
        {
            return await _historyService.GetHistoriesAsync(request, cancellationToken);
        }
    }
}
