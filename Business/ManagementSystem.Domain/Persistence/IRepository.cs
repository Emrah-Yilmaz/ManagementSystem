using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Packages.Repositories.EfCore;
using Packages.Repositories.EfCore.Entity;
using System;

namespace ManagementSystem.Domain.Persistence
{
    public interface IRepository<TEntity> : IGenericRepository<TEntity, DbContext> where TEntity : BaseEntity
    {
    }
}
