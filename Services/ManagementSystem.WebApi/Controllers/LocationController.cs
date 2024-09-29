using AutoMapper;
using ManagementSystem.Application.Features.Commands.City;
using ManagementSystem.Application.Features.Queries.Location;
using ManagementSystem.WebApi.Models.Response.Location;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public LocationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost("city")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCitiesAsync([FromBody]SyncLocationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpGet("cities")]
        [ProducesResponseType(typeof(List<CitiesResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCities (CancellationToken cancellationToken = default)
        {
            var query = new GetCitiesQuery();
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null || result.Count == 0)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<List<CitiesResponse>>(result);
            return Ok(mappedResponse);

        }

        [HttpGet("districts/{cityId}")]
        [ProducesResponseType(typeof(List<DistrictsResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDistricts([FromRoute]int cityId, CancellationToken cancellationToken = default)
        {
            var query = new GetDistrictsQuery();
            query.CityId = cityId;
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null || result.Count == 0)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<List<DistrictsResponse>>(result);
            return Ok(mappedResponse);

        }

        [HttpGet("quarters/{districtId}")]
        [ProducesResponseType(typeof(List<QuartersResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuarters(int districtId, CancellationToken cancellationToken = default)
        {
            var query = new GetQuartersQuery();
            query.DistrictId = districtId;
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null || result.Count == 0)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<List<QuartersResponse>>(result);
            return Ok(mappedResponse);

        }
    }
}
