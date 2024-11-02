using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ManagementSystem.Domain.Entities
{
    public class StatusChangeLog : BaseEntity
    {
        public string TableName { get; set; }
        public int RecordId { get; set; }
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }

        [NotMapped]
        public ChangedByInfo ChangedByInfo
        {
            get => string.IsNullOrWhiteSpace(ChangedBy) ? null : JsonSerializer.Deserialize<ChangedByInfo>(ChangedBy);
            set => ChangedBy = JsonSerializer.Serialize(value);
        }
    }
}
