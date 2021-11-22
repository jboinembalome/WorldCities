using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Common;
using System.Threading.Tasks;
using WorldCities.Application.Features.Countries.Queries.GetCountries;
using WorldCities.Application.Features.Countries.Queries.GetCountry;
using WorldCities.Application.Features.Countries.Commands.CreateCountry;
using WorldCities.Application.Features.Countries.Commands.UpdateCountry;
using WorldCities.Application.Features.Countries.Commands.DeleteCountry;
using WorldCities.Application.Features.Countries.Commands.IsDupeCountry;
using Microsoft.AspNetCore.Authorization;

namespace WorldCities.Web.Controllers
{
    //[Authorize]
    [ApiController]
    public class CountriesController : ApiControllerBase
    {
        public CountriesController()
        {
        }

        /// <summary>
        /// Get the countries.
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
            var dtos = await Mediator.Send(new GetCountriesQuery 
            { 
                PageIndex = page,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                FilterQuery = filterQuery
            });

            return Ok(dtos);
        }

        /// <summary>
        /// Get a country by id.
        /// </summary>
        /// <param name="id">Id of the country.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var dto = await Mediator.Send(new GetCountryQuery { Id = id });

            return Ok(dto);
        }

        /// <summary>
        /// Create a country.
        /// </summary>
        /// <param name="command">New country.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateCountryCommandResponse>> Create(CreateCountryCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = response.Country.Id }, response);
        }

        /// <summary>
        /// Update a country.
        /// </summary>
        /// <param name="id">Id of the country.</param>
        /// <param name="command">Country to be updated.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateCountryCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete a country.
        /// </summary>
        /// <param name="id">Id of the country.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCountryCommand { Id = id });

            return NoContent();
        }

        /// <summary>
        /// Check if a country field is duped.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("IsDupeField")]
        public async Task<ActionResult<IsDupeCountryCommandResponse>> IsDupeField(IsDupeCountryCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
