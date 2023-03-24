namespace Pets.Api.Controllers.Public;

using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Public.Page;

using Queries.Pages;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
[ProducesResponseType(typeof(ProblemDetails), 400)]
public sealed class PageController : ControllerBase
{
    /// <summary>
    ///     Get page
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PageView), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromQuery] GetPageBinding binding,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetPageQuery(
            binding.OrganisationId,
            binding.Page
        ), cancellationToken);
        if (result == null)
            return NotFound(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "page_not_found"
            });
        return Ok(result);
    }
}