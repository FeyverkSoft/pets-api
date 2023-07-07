namespace Pets.Api.Controllers.Public;

using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Public.Pets;

using Queries;
using Queries.Pets;

using Types;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public sealed class PetsController : ControllerBase
{
    /// <summary>
    ///     Get pet list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<PetView>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromQuery] GetPetsBinding binding,
        CancellationToken cancellationToken)
    {
        return Ok(await processor.Send(new GetPetsQuery(
            binding.OrganisationId,
            Offset: binding.Offset,
            Limit: binding.Limit,
            Filter: binding.Text,
            Genders: binding.Genders.Any()
                ? binding.Genders
                : new List<PetGender>(),
            PetStatuses: binding.PetStatuses.Any()
                ? binding.PetStatuses
                : new List<PetState>
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
    ///     Get pet by id
    /// </summary>
    /// <param name="organisationId">идентификатор орагнизации</param>
    /// <param name="petId">идентификатор конкретного животного</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{organisationId:guid}/{petId:guid}")]
    [ProducesResponseType(typeof(PetView), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromRoute] Guid organisationId,
        [FromRoute] Guid petId,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetPetQuery(
            organisationId,
            petId
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