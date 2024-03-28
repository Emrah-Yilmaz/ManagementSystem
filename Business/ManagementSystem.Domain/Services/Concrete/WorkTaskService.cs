using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.WebApi.Models.WorkTask.Response;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Domain.Services.Concrete
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IMapper _mapper;

        public WorkTaskService(IWorkTaskRepository workTaskRepository, IMapper mapper)
        {
            _workTaskRepository = workTaskRepository;
            _mapper = mapper;
        }

        public async Task<IList<WorkTasksDto>> GetTasksWithUserAsync(CancellationToken cancellationToken = default)
        {
            var result = await _workTaskRepository.GetTasksWithUserAsync();
            var mappedResult = _mapper.Map<IList<WorkTasksDto>>(result);
            return mappedResult;
        }
    }
}
