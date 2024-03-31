using AutoMapper;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Services.Abstract.Department;
using ManagementSystem.Domain.TokenHandler;
using System.Linq.Expressions;

namespace ManagementSystem.Domain.Services.Concrete.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;


        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateDepartmentArgs args, CancellationToken cancellationToken = default)
        {
            var mappedEntity = _mapper.Map<Domain.Entities.Department>(args);
            var result = await _repository.AddAsync(mappedEntity, cancellationToken);
            return result;
        }

        public async Task<DepartmentDto> GetDepartment(GetDepartmentArgs args, CancellationToken cancellationToken = default)
        {
            var result = await _repository.SingleOrDefaultAsync(p => p.Id == args.Id);
            if (result is null)
            {
                return null;
            }

            var mappedResult = _mapper.Map<DepartmentDto>(result);
            return mappedResult;
        }

        public async Task<IList<DepartmentDto>> GetDepartments(GetDepartmentsArgs args, CancellationToken cancellationToken = default)
        {
            var searchTerms = new[]
                {
                    (  x => x.Name, args.Name),
                    (  x => x.CreatedBy, args.CreatedBy),
                    ( (Expression<Func<Domain.Entities.Department, string>>) (x => x.ModifiedBy), args.ModifiedBy)
                };
            var result = await _repository.SearchAsync(searchTerms);
            var mappedResult = _mapper.Map<IList<DepartmentDto>>(result);
            return mappedResult;
        }

        public async Task<int> UpdateAsync(UpdateDepartmenArgs args, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(args.Id);
            if (entity is null)
            {
                return 0;
            }
            entity.Name = args.Name;
            var result = await _repository.UpdateAsync(entity);
            return result;
        }
    }
}
