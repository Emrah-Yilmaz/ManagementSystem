namespace ManagementSystem.Domain.Models.Args.User
{
    public class AssignUserToProjectArgs
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
