using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Models.Public.Pets;
using Pets.Queries;
using Pets.Queries.Pets;
using Pets.Types;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
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
            [FromServices] IQueryProcessor processor,
            [FromQuery] GetPetsBinding binding,
            CancellationToken cancellationToken)
        {
            return Ok(await processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: binding.OrganisationId,
                offset: binding.Offset,
                limit: binding.Limit,
                filter: binding.Text,
                genders: binding.Genders.Any() 
                    ? binding.Genders 
                    : new(),
                petStatuses: binding.PetStatuses.Any()
                    ? binding.PetStatuses
                    : new()
                    {
                        PetState.Adopted,
                        PetState.Alive,
                        PetState.Critical,
                        PetState.Death,
                        PetState.Wanted
                    }
            ), cancellationToken));
        }

        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="organisationId">идентификатор орагнизации</param>
        /// <param name="petId">идентификатор конкретного животного</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:guid}/{petId:guid}")]
        [ProducesResponseType(typeof(PetView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor processor,
            [FromRoute] Guid organisationId,
            [FromRoute] Guid petId,
            CancellationToken cancellationToken)
        {
            var result = await processor.Process<GetPetQuery, PetView?>(new GetPetQuery(
                organisationId: organisationId,
                petId: petId
            ), cancellationToken);

            if (result is null)
                return NotFound(new ProblemDetails
                {
                    Status = (Int32)HttpStatusCode.NotFound,
                    Type = "pet_not_found"
                });

            return Ok(result);
        }
    }
}