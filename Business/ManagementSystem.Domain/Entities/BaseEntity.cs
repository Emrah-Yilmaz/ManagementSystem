namespace ManagementSystem.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
