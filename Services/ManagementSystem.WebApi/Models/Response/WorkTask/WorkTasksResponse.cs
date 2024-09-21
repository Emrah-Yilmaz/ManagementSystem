using ManagementSystem.WebApi.Models.Response.Comment;

namespace ManagementSystem.WebApi.Models.Response.WorkTask
{
    public class WorkTasksResponse : BaseResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Status { get; set; }
        public string AssignedUser { get; set; }
        public ICollection<GetCommentResponse> Comments { get; set; }
    }
}
