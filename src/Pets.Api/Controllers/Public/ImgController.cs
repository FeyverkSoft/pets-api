namespace Pets.Api.Controllers.Public;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Queries.Documents;

/// <summary>
/// </summary>
[Route("[controller]")]
[ApiController]
[AllowAnonymous]
[ProducesResponseType(typeof(ProblemDetails), 404)]
[ProducesResponseType(typeof(ProblemDetails), 400)]
public sealed class ImgController : ControllerBase
{
    /// <summary>
    ///     Get image by id
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="id">image id</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Stream), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetImage(
        [FromRoute] Guid id,
        [FromServices] IMediator processor,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetImgQuery(
            id
        ), cancellationToken);
        if (result == null)
            return NotFound();
        result.Stream.Position = 0;
        Response.Headers.Add("Cache-Control", "public,max-age=864000");
        return File(result.Stream, result.ContentType);
    }
}