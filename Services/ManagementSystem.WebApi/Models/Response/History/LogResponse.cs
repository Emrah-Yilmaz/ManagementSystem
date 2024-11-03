namespace ManagementSystem.WebApi.Models.Response.History
{
    public class LogResponse
    {
        public int RecordId { get; set; }
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangedDate { get; set; }
        public ChangedByInfo ChangedBy { get; set; }
    }
}
