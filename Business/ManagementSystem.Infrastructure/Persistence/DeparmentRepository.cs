using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class DeparmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DeparmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
