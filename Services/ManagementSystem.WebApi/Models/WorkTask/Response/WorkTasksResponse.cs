using ManagementSystem.WebApi.Models.Comment;

namespace ManagementSystem.WebApi.Models.WorkTask.Response
{
    public class WorkTasksResponse : BaseResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Status { get; set; }
        public string AssignedUser { get; set; }
        public ICollection<CommentResponse> Comments { get; set; }
    }
}
