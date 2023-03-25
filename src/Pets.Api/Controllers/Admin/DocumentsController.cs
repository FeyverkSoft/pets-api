namespace Pets.Api.Controllers.Admin;

using Domain.Documents;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Admin.Documents;
using Models.Public.Img;

/// <summary>
/// </summary>
[Authorize(Policy = "admin")]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 404)]
[ProducesResponseType(typeof(ProblemDetails), 400)]
[ProducesResponseType(typeof(ProblemDetails), 401)]
[Route("admin/[controller]")]
public sealed class DocumentsController : ControllerBase
{
    /// <summary>
    ///     save image
    /// </summary>
    /// <param name="fileStoreService"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="file">model</param>
    /// <code>validation error, see <see href="https://tools.ietf.org/html/rfc7807#section-3">rfc7807#section-3</see></code>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(UploadFileView), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> PutImage(
        [FromForm] UploadFileBinding file,
        [FromServices] IDocumentRepository fileStoreService,
        CancellationToken cancellationToken)
    {
        var fileId = await fileStoreService.SaveFileAsync(file.File, cancellationToken);
        return Ok(new UploadFileView(fileId));
    }
}