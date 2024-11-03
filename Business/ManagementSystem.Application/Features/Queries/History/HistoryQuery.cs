using ManagementSystem.Domain.Models.Args.History;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.History
{
    public class HistoryQuery : HistoryArgs, IRequest<List<HistoryDto>>
    {
    }
}
