using ManagementSystem.Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Domain.Models.Args.User
{
    public class ChangeStatusArgs
    {
        public int Id { get; set; }
        public StatusType Status { get; set; }
    }
}
