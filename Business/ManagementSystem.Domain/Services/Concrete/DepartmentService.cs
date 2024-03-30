using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.Domain.TokenHandler;
using System.Linq.Expressions;

namespace ManagementSystem.Domain.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDomainPrincipal _domainPrincipal;


        public DepartmentService(IDepartmentRepository repository, IMapper mapper, IDomainPrincipal domainPrincipal)
        {
            _repository = repository;
            _mapper = mapper;
            _domainPrincipal = domainPrincipal;
        }

        public async Task<int> CreateAsync(CreateDepartmentArgs args, CancellationToken cancellationToken = default)
        {
            var mappedEntity = _mapper.Map<Department>(args);
            mappedEntity.CreatedById = _domainPrincipal.GetClaims().Id;
            mappedEntity.CreatedBy = string.Concat(_domainPrincipal.GetClaims().Name + " " + _domainPrincipal.GetClaims().LastName);

            var result = await _repository.AddAsync(mappedEntity);

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
                    ( (Expression<Func<Department, string>>) (x => x.Name), args.Name),
                    ( (Expression<Func<Department, string>>) (x => x.CreatedBy), args.CreatedBy),
                    ( (Expression<Func<Department, string>>) (x => x.ModifiedBy), args.ModifiedBy)
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
            entity.ModifiedById = _domainPrincipal.GetClaims().Id;
            entity.ModifiedBy = string.Concat(_domainPrincipal.GetClaims().Name + " " + _domainPrincipal.GetClaims().LastName);

            var result = await _repository.UpdateAsync(entity);

            return result;
        }
    }
}
