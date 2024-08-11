using CommonLibrary.Features.Paginations;
using CommonLibrary.Resources;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using MediatR;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDeparmentsQuery : GetDepartmentsArgs, IRequest<PagedViewModel<DepartmentDto>>, ICachableRequest
    {
        public string CacheKey => string.Format(Constants.Caches.Department.CacheKey, Page, PageSize);
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
        public string? CacheGroupKey => Constants.Caches.Department.CachceGroupKey;
    }
}
