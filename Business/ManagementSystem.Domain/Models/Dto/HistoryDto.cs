namespace ManagementSystem.Domain.Models.Dto
{
    public class HistoryDto
    {
        public string TableName { get; set; }
        public int RecordId { get; set; }
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
