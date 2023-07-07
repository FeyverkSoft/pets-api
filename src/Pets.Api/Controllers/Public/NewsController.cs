namespace Pets.Api.Controllers.Public;

using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Public.News;

using Queries;
using Queries.News;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public sealed class NewsController : ControllerBase
{
    /// <summary>
    ///     Get news list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<NewsView>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromQuery] GetNewsBinding binding,
        CancellationToken cancellationToken)
    {
        return Ok(await processor.Send(new GetNewsQuery(
            OrganisationId: binding.OrganisationId,
            Offset: binding.Offset,
            Limit: binding.Limit,
            Tag: binding.Tag,
            PetId: binding.PetId
        ), cancellationToken));
    }

    /// <summary>
    ///     Get news
    /// </summary>
    /// <param name="organisationId">идентификатор орагнизации</param>
    /// <param name="newsId">идентификатор конкретной новости</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{organisationId:guid}/{newsId:guid}")]
    [ProducesResponseType(typeof(NewsView), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromRoute] Guid organisationId,
        [FromRoute] Guid newsId,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetSingleNewsQuery(
            organisationId,
            newsId
        ), cancellationToken);

        if (result is null)
            return NotFound(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "news_not_found"
            });

        return Ok(result);
    }
}