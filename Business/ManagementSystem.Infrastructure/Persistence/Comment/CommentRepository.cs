using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Comment
{
    public class CommentRepository : GenericRepository<Domain.Entities.Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
