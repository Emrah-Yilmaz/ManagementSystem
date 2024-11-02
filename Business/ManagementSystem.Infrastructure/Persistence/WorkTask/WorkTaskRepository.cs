using ManagementSystem.Domain.Models.Args.WorkTask;
using ManagementSystem.Domain.Persistence.WorkTask;
using ManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence.WorkTask
{
    public class WorkTaskRepository : Repository<Domain.Entities.WorkTask>, IWorkTaskRepository
    {
        private readonly AppDbContext _context;
        public WorkTaskRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Domain.Entities.WorkTask> GetTasksWithUserAsync(GetWorkTasksArgs args, CancellationToken cancellationTokeni = default)
        {
            IQueryable<Domain.Entities.WorkTask> query = _context.Task
                .Include(task => task.AssignedUser)
                .Include(status => status.Status)
                .Include(department => department.Department)
                .Include(comments => comments.Comments);

            if (!string.IsNullOrEmpty(args.Title))
            {
                query = query.Where(t => t.Title.Contains(args.Title));
            }

            if (!string.IsNullOrEmpty(args.AssignedUser))
            {
                query = query.Where(t => t.AssignedUser.Name.Contains(args.AssignedUser));
            }

            if (!string.IsNullOrEmpty(args.Department))
            {
                query = query.Where(t => t.Department.Name.Contains(args.Department));
            }

            if (!string.IsNullOrEmpty(args.Status))
            {
                query = query.Where(t => t.Status.Contains(args.Status));
            }

            if (args.Deadline.HasValue)
            {
                query = query.Where(t => t.Deadline.Date == args.Deadline.Value.Date);
            }

            if (!string.IsNullOrEmpty(args.CreatedBy))
            {
                query = query.Where(t => t.CreatedBy.Contains(args.CreatedBy));
            }

            if (!string.IsNullOrEmpty(args.ModifiedBy))
            {
                query = query.Where(t => t.ModifiedBy.Contains(args.ModifiedBy));
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
