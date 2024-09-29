using ManagementSystem.Domain.Models.Args.Comment;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.Comment;

public interface ICommentService : IDomainService
{
    public Task<int> CreateAsync(CreateCommentArgs args, CancellationToken cancellationToken = default);
    public Task<int> UpdateAsync(UpdateCommentArgs args, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(GetCommentArgs args, CancellationToken cancellationToken = default);
    public Task<GetCommentDto> GetAsync(GetCommentArgs args, CancellationToken cancellationToken = default);
}
