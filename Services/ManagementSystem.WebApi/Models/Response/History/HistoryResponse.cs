namespace ManagementSystem.WebApi.Models.Response.History
{
    public class HistoryResponse
    {
        public string Entity { get; set; }
        public List<LogResponse> Logs { get; set; }
    }
}
