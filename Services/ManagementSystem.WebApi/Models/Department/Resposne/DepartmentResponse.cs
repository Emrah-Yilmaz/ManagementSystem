namespace ManagementSystem.WebApi.Models.Department.Resposne
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedBy { get; set; }
        public int? ModifiedById { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
