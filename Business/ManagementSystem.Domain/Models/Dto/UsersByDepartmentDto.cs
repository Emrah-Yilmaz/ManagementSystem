namespace ManagementSystem.Domain.Models.Dto
{
    public class UsersByDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int WorkersCount { get; set; }
        public int MyProperty { get; set; }
        public IList<UserDto> Users { get; set; }
    }
}
