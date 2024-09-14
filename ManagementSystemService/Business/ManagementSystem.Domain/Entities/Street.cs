namespace ManagementSystem.Domain.Entities
{
    public class Street : BaseEntity
    {
        public string Name { get; set; }
        public required int QuarterId { get; set; }
        public virtual Quarter Quarter { get; set; }
    }
}