namespace Pets.Api.Controllers.Public;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Public.Search;

using Queries;
using Queries.Search;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public sealed class SearchController : ControllerBase
{
    /// <summary>
    ///     Search documents and pets
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{OrganisationId}")]
    [ProducesResponseType(typeof(Page<SearchView>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromQuery] SearchBinding binding,
        CancellationToken cancellationToken)
    {
        return Ok(await processor.Send(new SearchQuery(
            binding.OrganisationId,
            binding.Query,
            limit: binding.Limit,
            offset: binding.Offset
        ), cancellationToken));
    }
}