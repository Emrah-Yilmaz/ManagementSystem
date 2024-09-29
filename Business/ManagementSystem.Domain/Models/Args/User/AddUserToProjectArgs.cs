namespace ManagementSystem.Domain.Models.Args.User
{
    public class AddUserToProjectArgs
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
