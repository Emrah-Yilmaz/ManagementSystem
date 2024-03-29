namespace ManagementSystem.Domain.Models.Args
{
    public class CreateUserArgs
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
