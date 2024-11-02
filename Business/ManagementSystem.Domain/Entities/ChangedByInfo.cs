using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Domain.Entities
{
    [NotMapped]
    public class ChangedByInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
