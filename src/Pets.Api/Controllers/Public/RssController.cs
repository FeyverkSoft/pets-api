using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using Pets.Infrastructure.Markdown;
using Pets.Queries;
using Pets.Queries.Pets;
using Pets.Types;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    [ApiController]
    [AllowAnonymous]
    public sealed class RssController : ControllerBase
    {
        [HttpGet("/rss/{organisationId}/pets")]
        public async Task<IActionResult> GetPetRss(
            [FromServices] IQueryProcessor _processor,
            [FromServices] IMarkdown _markdown,
            [FromRoute] Guid organisationId,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: organisationId,
                offset: 0,
                limit: 100,
                petStatuses: new List<PetState>
                {
                    PetState.Adopted,
                    PetState.Alive,
                    PetState.Critical,
                    PetState.Death,
                    PetState.Wanted,
                    PetState.OurPets
                }
            ), cancellationToken);
            var sb = new StringBuilder(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<rss version=""2.0"" xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:turbo=""http://turbo.yandex.ru"">
<channel>
<title>Жители добродома</title>
<link>https://dobrodom.online/pets</link>
<description><![CDATA[Список питомцев добродома]]></description>
<language>ru</language>
<generator>dobrodom.online</generator>");
            sb.Append($"<pubDate>{DateTime.UtcNow:u}</pubDate>");

            foreach (var petView in result.Items)
            {
                var content = await _markdown.Parse(String.IsNullOrEmpty(petView.MdBody) ? petView.MdShortBody : petView.MdBody);
                sb.Append(@$"<item turbo=""true"">
                    <title>{petView.Name}</title>
                    <guid isPermaLink=""true"">https://dobrodom.online/pets/{petView.Id}</guid>
                    <link>https://dobrodom.online/pets/{petView.Id}</link>
                    <description><![CDATA[<img src=""https://dobrodom.online{petView.AfterPhotoLink ?? petView.BeforePhotoLink}""></img><br>
{content}]]></description>
                    <pubDate>{petView.UpdateDate:u}</pubDate>
                    <turbo:content><![CDATA[{content}]]>
                    </turbo:content>
                    </item>"
                );
            }

            sb.Append(@"</channel></rss>");
            return Content(sb.ToString(), MediaTypeHeaderValue.Parse("text/xml; charset=utf-8"));
        }
    }
}