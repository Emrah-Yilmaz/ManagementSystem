using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.WorkTask;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.WorkTask;
using ManagementSystem.Domain.Services.Abstract.WorkTask;
using ManagementSystem.Domain.TokenHandler;

namespace ManagementSystem.Domain.Services.Concrete.WorkTask
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IMapper _mapper;
        private readonly IDomainPrincipal _domainPrincipal;
        public WorkTaskService(IWorkTaskRepository workTaskRepository, IMapper mapper, IDomainPrincipal domainPrincipal)
        {
            _workTaskRepository = workTaskRepository;
            _mapper = mapper;
            _domainPrincipal = domainPrincipal;
        }

        public async Task<int> CreateAsync(CreateWorkTaskArgs args, CancellationToken cancellationToken = default)
        {
            var mappedEntity = _mapper.Map<Domain.Entities.WorkTask>(args);
            mappedEntity.CreatedById = _domainPrincipal.GetClaims().Id;
            mappedEntity.CreatedBy = string.Concat(_domainPrincipal.GetClaims().Name != null + " " + _domainPrincipal.GetClaims().LastName);

            var result = await _workTaskRepository.AddAsync(mappedEntity);

            return result;
        }

        public async Task<int> UpdateAsync(UpdateWorkTaskArgs args, CancellationToken cancellationToken = default)
        {
            var entity = await _workTaskRepository.FindAsync(args.Id);

            if (entity is null)
            {
                return 0;
            }

            _mapper.Map(args, entity);
            entity.ModifiedById = _domainPrincipal.GetClaims().Id;
            entity.ModifiedBy = string.Concat(_domainPrincipal.GetClaims().Name + " " + _domainPrincipal.GetClaims().LastName);

            var result = await _workTaskRepository.UpdateAsync(entity);

            return result;
        }

        public async Task<IList<WorkTasksDto>> GetWorkTasksAsync(GetWorkTasksArgs args, CancellationToken cancellationToken = default)
        {
            var result = await _workTaskRepository.GetList(p => p.Id > 0, false, null, x => x.Department, y => y.Status, z => z.AssignedUser, w => w.Comments);

            if (!string.IsNullOrEmpty(args.Title))
            {
                result = result.Where(t => t.Title.Contains(args.Title, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(args.AssignedUser))
            {
                result = result.Where(t => t.AssignedUser.Name.Contains(args.AssignedUser, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(args.Department))
            {
                result = result.Where(t => t.Department.Name.Contains(args.Department, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(args.Status))
            {
                result = result.Where(t => t.Status.Name.Contains(args.Status, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (args.Deadline.HasValue)
            {
                result = result.Where(t => t.Deadline.Date == args.Deadline.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(args.CreatedBy))
            {
                result = result.Where(t => t.CreatedBy.Contains(args.CreatedBy, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(args.ModifiedBy))
            {
                result = result.Where(t => t.ModifiedBy.Contains(args.ModifiedBy, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            var mappedResult = _mapper.Map<IList<WorkTasksDto>>(result);
            return mappedResult;
        }

        public async Task<WorkTasksDto> GetWorkTaskAsync(GetWorkTaskArgs args, CancellationToken cancellationToken = default)
        {
            var entity =  _workTaskRepository.Get(p => p.Id == args.Id, true, x => x.Department, y => y.Status, z => z.AssignedUser, w => w.Comments);
            if (entity is null)
            {
                return null;
            }

            var mappedResult = _mapper.Map<WorkTasksDto>(entity.FirstOrDefault());

            return mappedResult;
        }
    }
}
