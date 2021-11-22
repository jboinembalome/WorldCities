using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Common;
using System.Threading.Tasks;
using WorldCities.Application.Features.Cities.Commands.CreateCity;
using WorldCities.Application.Features.Cities.Commands.DeleteCity;
using WorldCities.Application.Features.Cities.Commands.UpdateCity;
using WorldCities.Application.Features.Cities.Queries.GetCities;
using WorldCities.Application.Features.Cities.Commands.IsDupeCity;
using WorldCities.Application.Features.Cities.Queries.GetCity;
using Microsoft.AspNetCore.Authorization;

namespace WorldCities.Web.Controllers
{
    //[Authorize]
    [ApiController]
    public class CitiesController : ApiControllerBase
    {
        public CitiesController()
        {
        }

        /// <summary>
        /// Get the cities.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="filterQuery"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IPagedList<CityDto>>> Get(
            int page = 0,
            int pageSize = 10,
            string sortColumn = null,
            string sortOrder = null,
            string filterQuery = "")
        {
            var dtos = await Mediator.Send(new GetCitiesQuery 
            { 
                Page = page,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                FilterQuery = filterQuery
            });

            return Ok(dtos);
        }

        /// <summary>
        /// Get a city by id.
        /// </summary>
        /// <param name="id">Id of the city.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var dto = await Mediator.Send(new GetCityQuery { CityId = id });

            return Ok(dto);
        }

        /// <summary>
        /// Create a city.
        /// </summary>
        /// <param name="command">New city.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateCityCommandResponse>> Create(CreateCityCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = response.City.Id }, response);
        }

        /// <summary>
        /// Update a city.
        /// </summary>
        /// <param name="id">Id of the city.</param>
        /// <param name="command">City to be updated.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateCityCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete a city.
        /// </summary>
        /// <param name="id">Id of the city.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCityCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        [Route("IsDupeCity")]
        public async Task<ActionResult<IsDupeCityCommandResponse>> IsDupeCity(IsDupeCityCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
