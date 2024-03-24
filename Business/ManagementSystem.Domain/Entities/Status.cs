namespace ManagementSystem.Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<WorkTask> Tasks { get; set; }
    }
}
