namespace Pets.Api.Controllers.Admin;

using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Admin.Page;

using Queries;
using Queries.Pages.Admin;

[Authorize(Policy = "admin")]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 404)]
[ProducesResponseType(typeof(ProblemDetails), 400)]
[ProducesResponseType(typeof(ProblemDetails), 401)]
[Route("admin/[controller]")]
public sealed class PageController : ControllerBase
{
    /// <summary>
    ///     Get page
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{OrganisationId}/{Page}")]
    [ProducesResponseType(typeof(AdminPageView), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromQuery] GetPageBinding binding,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetAdminPageQuery(
            OrganisationId: binding.OrganisationId,
            Page: binding.Page
        ), cancellationToken);
        if (result == null)
            return NotFound(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "page_not_found"
            });
        return Ok(result);
    }

    /// <summary>
    ///     Get page list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{OrganisationId}")]
    [ProducesResponseType(typeof(Page<AdminPageView>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetList(
        [FromServices] IMediator processor,
        [FromQuery] GetPagesBinding binding,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetAdminPagesQuery(
            OrganisationId: binding.OrganisationId,
            Limit: binding.Limit,
            Offset: binding.Offset
        ), cancellationToken);
        return Ok(result);
    }
}