using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Comment.Response;

namespace ManagementSystem.WebApi.MappingProfile.Comment
{
    public class CommentMappingProfile : AutoMapper.Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<GetCommentDto, GetCommentResponse>();
        }
    }
}
