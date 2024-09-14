using ManagementSystem.Domain.Models.Args.Comment;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.Comment
{
    public class CommentMappingProfile : AutoMapper.Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<CreateCommentArgs, Domain.Entities.Comment>();
            CreateMap<Entities.Comment, GetCommentDto>();
            CreateMap<UpdateCommentArgs, Entities.Comment>();
        }
    }
}