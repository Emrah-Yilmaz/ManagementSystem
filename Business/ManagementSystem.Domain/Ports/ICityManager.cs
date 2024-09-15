using ManagementSystem.Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Domain.Ports
{
    public interface ICityManager
    {
        Task<string> GetCitiesAsync(CancellationToken cancellationToken = default);
    }
}
