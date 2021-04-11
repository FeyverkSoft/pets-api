using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using Pets.Infrastructure.Markdown;
using Pets.Queries;
using Pets.Queries.News;
using Pets.Queries.Pets;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    /// <summary>
    /// Контроллер для генерации статики для поисковика Х.яндекса
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public sealed class YandexSeoController : ControllerBase
    {
        [HttpGet("/yandex/{organisationId:guid}/pets/{petId:guid}")]
        public async Task<IActionResult> GetPetSimple(
            [FromServices] IQueryProcessor processor,
            [FromServices] IMarkdown markdown,
            [FromRoute] Guid organisationId,
            [FromRoute] Guid petId,
            CancellationToken cancellationToken)
        {
            var result = await processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: organisationId,
                petId: petId,
                offset: 0,
                limit: 100,
                genders: new(),
                petStatuses: new()
            ), cancellationToken);
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
            [FromServices] IQueryProcessor processor,
            [FromServices] IMarkdown markdown,
            [FromRoute] Guid organisationId,
            [FromRoute] Guid newsId,
            CancellationToken cancellationToken)
        {
            var news = await processor.Process<GetSingleNewsQuery, NewsView?>(new GetSingleNewsQuery(
                organisationId: organisationId,
                newsId: newsId
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
}