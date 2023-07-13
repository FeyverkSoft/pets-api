namespace Pets.Api.Controllers.Public;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Infrastructure.Markdown;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using Queries.News;
using Queries.Pets;

using Types;

/// <summary>
///     Контроллер для генерации статики для поисковика Х.яндекса
/// </summary>
[ApiController]
[AllowAnonymous]
public sealed class YandexSeoController : ControllerBase
{
    [HttpGet("/yandex/{organisationId:guid}/pets/{petId:guid}")]
    public async Task<IActionResult> GetPetSimple(
        [FromServices] IMediator processor,
        [FromServices] IMarkdown markdown,
        [FromRoute] Guid organisationId,
        [FromRoute] Guid petId,
        CancellationToken cancellationToken)
    {
        var result = await processor.Send(new GetPetsQuery(
            organisationId,
            PetStatuses: new List<PetState>(), Genders: new List<PetGender>(), Offset: 0, Limit: 100, PetId: petId), cancellationToken);
        if (result.Total == 0)
            return NotFound();
        var pet = result.Items.First();
        var content = await markdown.Parse(String.IsNullOrEmpty(pet.MdBody) ? pet.MdShortBody : pet.MdBody);
        var sb = new StringBuilder(@"<html lang=""ru""><head><meta charset=""utf-8""/>");
        sb.Append($"<title>Добродом - наши питомцы: {pet.Name}</title>");
        sb.Append($@"<meta name=""description"" content=""Добродом, помощь бездомным животным в Оренбурге, {pet.Name}"">");
        sb.Append(
            $@"<meta name=""keywords"" content=""Добродом, Оренбург, помощь бездомным животным в Оренбурге, {pet.Name}, {await markdown.Parse(pet.MdShortBody)}""/>");
        sb.Append("</head><body>");
        sb.Append(content);
        sb.Append("</body></html>");
        return Content(sb.ToString(), MediaTypeHeaderValue.Parse("text/html; charset=utf-8"));
    }

    [HttpGet("/yandex/{organisationId:guid}/news/{newsId:guid}")]
    public async Task<IActionResult> GetNewsSimple(
        [FromServices] IMediator processor,
        [FromServices] IMarkdown markdown,
        [FromRoute] Guid organisationId,
        [FromRoute] Guid newsId,
        CancellationToken cancellationToken)
    {
        var news = await processor.Send(new GetSingleNewsQuery(
            organisationId,
            newsId
        ), cancellationToken);

        if (news is null)
            return NotFound();

        var content = await markdown.Parse(String.IsNullOrEmpty(news.MdBody) ? news.MdShortBody : news.MdBody);
        var sb = new StringBuilder(@"<html lang=""ru""><head><meta charset=""utf-8""/>");
        sb.Append($"<title>Добродом - Новости: {news.Title}</title>");
        sb.Append($@"<meta name=""description"" content=""Добродом, помощь бездомным животным в Оренбурге, {news.Title}"">");
        sb.Append(
            $@"<meta name=""keywords"" content=""Добродом, Оренбург, помощь бездомным животным в Оренбурге, {news.Title}, {String.Join(", ", news.Tags)}""/>");
        sb.Append("</head><body>");
        sb.Append(content);
        sb.Append("</body></html>");
        return Content(sb.ToString(), MediaTypeHeaderValue.Parse("text/html; charset=utf-8"));
    }
}