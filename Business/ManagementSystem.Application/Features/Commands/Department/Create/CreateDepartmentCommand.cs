﻿using CommonLibrary.Resources;
using ManagementSystem.Domain.Models.Args.Department;
using MediatR;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Commands.Department.Create
{
    public class CreateDepartmentCommand : CreateDepartmentArgs, IRequest<int>, ICacheRemoverRequest
    {
        public string? CacheKey => default;

        public bool BypassCache => default;

        public string? CacheGroupKey => Constants.Caches.Department.CachceGroupKey;
    }
}