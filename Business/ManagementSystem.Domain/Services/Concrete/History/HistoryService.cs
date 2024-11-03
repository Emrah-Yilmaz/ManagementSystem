using AutoMapper;
using ManagementSystem.Domain.Models.Args.History;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.History;
using ManagementSystem.Domain.Services.Abstract.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Domain.Services.Concrete.History
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _repository;
        private readonly IMapper _mapper;

        public HistoryService(IHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<HistoryDto>> GetHistoriesAsync(HistoryArgs args, CancellationToken cancellationToken = default)
        {
            var histories = await _repository.GetListAsync(
                predicate: p => p.TableName == args.Modules.ToString() && p.RecordId == args.Id,
                noTracking: true,
                cancellationToken: default);

            if (histories is null || histories.Count == 0)
                return null;

            var mappedResult = _mapper.Map<List<HistoryDto>>(histories);
            return mappedResult;
            
        }
    }
}
