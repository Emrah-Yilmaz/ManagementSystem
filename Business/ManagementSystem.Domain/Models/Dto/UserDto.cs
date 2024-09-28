﻿namespace ManagementSystem.Domain.Models.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
