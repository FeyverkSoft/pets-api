using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Pets.Api.Models.Pets;
using Pets.Queries;
using Pets.Queries.Pets;

using Query.Core;

namespace Pets.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class PetsController : ControllerBase
    {
        /// <summary>
        /// Get pet list
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Page<PetView>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor _processor,
            [FromQuery] GetPetsBinding binding,
            CancellationToken cancellationToken)
        {
            return Ok(await _processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: binding.OrganisationId,
                offset: binding.Offset,
                limit: binding.Limit,
                filter: binding.Filter
            ), cancellationToken));
        }
        
        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="organisationId">идентификатор орагнизации</param>
        /// <param name="petId">идентификатор конкретного животного</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/{petId}")]
        [ProducesResponseType(typeof(PetView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor _processor,
            [FromRoute] Guid organisationId,
            [FromRoute] Guid petId,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: organisationId,
                offset: 0,
                limit: 1,
                petId: petId
            ), cancellationToken);

            if (!result.Items.Any())
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Type = "pet_not_found"
                });

            return Ok(result.Items.First());
        }
    }
}