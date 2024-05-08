namespace Pets.Api.Controllers.Admin;

using System.Net;

using Authorization;

using Domain.News;
using Domain.ValueTypes;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Admin.News;

using Queries;
using Queries.News.Admin;

using Types.Exceptions;

/// <summary>
/// </summary>
[Authorize(Policy = "admin")]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 404)]
[ProducesResponseType(typeof(ProblemDetails), 400)]
[ProducesResponseType(typeof(ProblemDetails), 401)]
[Route("admin/[controller]")]
public sealed class NewsController : ControllerBase
{
    /// <summary>
    ///     Get news list
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<AdminNewsView>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromQuery] GetAdminNewsBinding binding,
        CancellationToken cancellationToken)
    {
        return Ok(await processor.Send(new GetAdminNewsQuery(
            OrganisationId: User.GetOrganisationId(),
            NewsId: binding.NewsId,
            Offset: binding.Offset,
            Limit: binding.Limit,
            Tag: binding.Tag,
            PetId: binding.PetId,
            Filter: binding.Filter,
            NewsStatuses: binding.NewsStatuses
        ), cancellationToken));
    }

    /// <summary>
    ///     Get new by id
    /// </summary>
    /// <param name="organisationId">идентификатор орагнизации</param>
    /// <param name="newsId">идентификатор конкретной новости</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{organisationId:guid}/{newsId:guid}")]
    [ProducesResponseType(typeof(AdminNewsView), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator processor,
        [FromRoute] Guid organisationId,
        [FromRoute] Guid newsId,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetAdminSingleNewsQuery(
            OrganisationId: User?.GetOrganisationId() ?? organisationId,
            NewsId: newsId
        ), cancellationToken);

        if (result is null)
            return NotFound(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "news_not_found"
            });
        return Ok(result);
    }

    /// <summary>
    ///     Create new news
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AdminNewsView), 200)]
    public async Task<IActionResult> Create(
        [FromServices] IMediator processor,
        [FromServices] INewsCreateService newsCreateService,
        [FromBody] CreateNewsBinding binding,
        CancellationToken cancellationToken)
    {
        try
        {
            await newsCreateService.Create(
                organisationId: (Organisation)HttpContext.GetOrganisationId(),
                id: binding.NewsId,
                request: new INewsCreateService.CreateNews(
                    Title: binding.Title,
                    ImgLink: binding.ImgLink,
                    MdShortBody: binding.MdShortBody,
                    MdBody: binding.MdBody,
                    LinkedPets: binding.LinkedPets,
                    Tags: binding.Tags),
                cancellationToken);
        }
        catch (IdempotencyCheckException e)
        {
            return Conflict(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.Conflict,
                Type = "news_already_exists",
                Detail = e.Message
            });
        }

        return Ok(await processor.Send(new GetAdminSingleNewsQuery(
            OrganisationId: User.GetOrganisationId(),
            NewsId: binding.NewsId
        ), cancellationToken));
    }


    /// <summary>
    ///     Update news state
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="newsId"></param>
    /// <returns></returns>
    /* [HttpPatch("{newsId:guid}/state")]
     [ProducesResponseType(typeof(PetView), 200)]
     public async Task<IActionResult> UpdateStatus(
         [FromRoute] Guid newsId,
         [FromServices] IMediator processor,
         [FromServices] INewsUpdateService newsUpdateService,
         [FromBody] UpdateStatusNewsBinding binding,
         CancellationToken cancellationToken)
     {
         try
         {
             await newsUpdateService.SetStatus(
                 newsId,
                 User.GetOrganisationId(),
                 binding.State,
                 cancellationToken);
         }
         catch (NotFoundException e)
         {
             return Conflict(new ProblemDetails
             {
                 Status = (Int32)HttpStatusCode.NotFound,
                 Type = "news_not_found",
                 Detail = e.Message
             });
         }
 
         return Ok(await processor.Send(new GetAdminNewsQuery(
             User.GetOrganisationId(),
             newsId
         ), cancellationToken));
     }*/

    /// <summary>
    ///     Update news type
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="newsId"></param>
    /// <returns></returns>
    /* [HttpPatch("{newsId:guid}/type")]
     [ProducesResponseType(typeof(PetView), 200)]
     public async Task<IActionResult> UpdateType(
         [FromRoute] Guid newsId,
         [FromServices] IMediator processor,
         [FromServices] INewsUpdateService newsUpdateService,
         [FromBody] UpdateTypeNewsBinding binding,
         CancellationToken cancellationToken)
     {
         try
         {
             await newsUpdateService.SetType(
                 newsId,
                 User.GetOrganisationId(),
                 binding.Type,
                 cancellationToken);
         }
         catch (NotFoundException e)
         {
             return Conflict(new ProblemDetails
             {
                 Status = (Int32)HttpStatusCode.NotFound,
                 Type = "pet_not_found",
                 Detail = e.Message
             });
         }
 
         return Ok(await processor.Send(new GetPetQuery(
             User.GetOrganisationId(),
             newsId
         ), cancellationToken));
     }*/
}