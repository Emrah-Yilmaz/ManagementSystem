using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.Models.Args.History
{
    public class HistoryArgs
    {
        public int Id { get; set; }
        public ModulesType Modules { get; set; }
    }
}
