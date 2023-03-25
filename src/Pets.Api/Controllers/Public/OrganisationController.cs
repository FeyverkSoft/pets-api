namespace Pets.Api.Controllers.Public;

using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Queries.Organisation;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
[ProducesResponseType(typeof(ProblemDetails), 400)]
public sealed class OrganisationController : ControllerBase
{
    /// <summary>
    ///     Get contact list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{organisationId:guid}/contacts")]
    [ProducesResponseType(typeof(IEnumerable<ContactView>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetContacts(
        [FromRoute] Guid organisationId,
        [FromServices] IMediator processor,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetContactsQuery(
            organisationId
        ), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Get res list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{organisationId:guid}/building")]
    [ProducesResponseType(typeof(IEnumerable<ResourceView>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetBuilding(
        [FromRoute] Guid organisationId,
        [FromServices] IMediator processor,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetBuildingQuery(
            organisationId
        ), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Get res list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{organisationId:guid}/needs")]
    [ProducesResponseType(typeof(IEnumerable<NeedView>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetNeeds(
        [FromRoute] Guid organisationId,
        [FromServices] IMediator processor,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetNeedQuery(
            organisationId
        ), cancellationToken);
        return Ok(result);
    }
}