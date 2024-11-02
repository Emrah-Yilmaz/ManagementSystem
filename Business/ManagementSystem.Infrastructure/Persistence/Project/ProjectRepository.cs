using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Infrastructure.Persistence.Project
{
    public class ProjectRepository : Repository<Domain.Entities.Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
