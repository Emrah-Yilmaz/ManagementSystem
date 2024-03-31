using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Domain.Entities
{
    public class Status : BaseEntity
    {
        [Key]
        public string Name { get; set; }
        public virtual Comment? Comment { get; set; }
        public virtual ICollection<WorkTask>? Tasks { get; set; }
    }
}
