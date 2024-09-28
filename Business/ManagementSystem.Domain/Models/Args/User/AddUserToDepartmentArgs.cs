namespace ManagementSystem.Domain.Models.Args.User
{
    public class AddUserToDepartmentArgs
    {
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
    }
}
