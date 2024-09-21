using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.Comment;

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
