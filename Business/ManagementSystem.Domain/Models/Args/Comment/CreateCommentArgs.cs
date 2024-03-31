namespace ManagementSystem.Domain.Models.Args.Comment
{
    public class CreateCommentArgs
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
