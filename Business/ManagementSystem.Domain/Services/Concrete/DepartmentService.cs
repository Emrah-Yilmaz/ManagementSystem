using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Extensions;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.Domain.TokenHandler;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
    }
}
