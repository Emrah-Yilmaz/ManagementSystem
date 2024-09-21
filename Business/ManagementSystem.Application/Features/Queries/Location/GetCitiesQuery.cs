using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Location
{
    public class GetCitiesQuery : IRequest<List<CityDto>>
    {
    }
}