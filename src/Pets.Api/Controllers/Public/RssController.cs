namespace Pets.Api.Controllers.Public;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Infrastructure.Markdown;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

using Queries.News;
using Queries.Pets;

using Types;

[ApiController]
[AllowAnonymous]
public sealed class RssController : ControllerBase
{
    [HttpGet("/rss/{organisationId}/pets")]
    public async Task<IActionResult> GetPetRss(
        [FromServices] IMediator processor,
        [FromServices] IConfiguration config,
        [FromServices] IMarkdown markdown,
        [FromRoute] Guid organisationId,
        CancellationToken cancellationToken)
    {
        var domain = config["Domain"];
        var result = await processor.Send(new GetPetsQuery(
            organisationId,
            Offset: 0,
            Limit: 100,
            Genders: new List<PetGender>(),
            PetStatuses: new List<PetState>()
        ), cancellationToken);
        var sb = new StringBuilder(@$"<?xml version=""1.0"" encoding=""UTF-8""?>
<rss version=""2.0"" xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:turbo=""http://turbo.yandex.ru"">
<channel>
<title>Жители добродома</title>
<link>{domain}/pets</link>
<description><![CDATA[Список питомцев добродома]]></description>
<language>ru</language>
<generator>{domain}</generator>");
        sb.Append($"<pubDate>{result.Items.FirstOrDefault()?.UpdateDate ?? DateTime.UtcNow:u}</pubDate>");

        foreach (var petView in result.Items)
        {
            var content = await markdown.Parse(String.IsNullOrEmpty(petView.MdBody) ? petView.MdShortBody : petView.MdBody);
            sb.Append(@$"<item turbo=""true"">
                    <title>{petView.Name}</title>
                    <guid isPermaLink=""true"">{domain}/pets/{petView.Id}</guid>
                    <link>{domain}/pets/{petView.Id}</link>
                    <description><![CDATA[<img src=""{domain}{petView.AfterPhotoLink ?? petView.BeforePhotoLink}""></img><br>
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

    [HttpGet("/rss/{organisationId}/news")]
    public async Task<IActionResult> GetNewsRss(
        [FromServices] IMediator processor,
        [FromServices] IConfiguration config,
        [FromServices] IMarkdown markdown,
        [FromRoute] Guid organisationId,
        CancellationToken cancellationToken)
    {
        var domain = config["Domain"];
        var result = await processor.Send(new GetNewsQuery(
            OrganisationId: organisationId,
            Offset: 0,
            Limit: 100
        ), cancellationToken);

        var sb = new StringBuilder(@$"<?xml version=""1.0"" encoding=""UTF-8""?>
<rss version=""2.0"" xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:turbo=""http://turbo.yandex.ru"">
<channel>
<title>Новости</title>
<link>{domain}/pets</link>
<description><![CDATA[Новости]]></description>
<language>ru</language>
<generator>{domain}</generator>");
        sb.Append($"<pubDate>{result.Items.FirstOrDefault()?.CreateDate ?? DateTime.UtcNow:u}</pubDate>");

        foreach (var newsView in result.Items)
        {
            var content = await markdown.Parse(String.IsNullOrEmpty(newsView.MdBody) ? newsView.MdShortBody : newsView.MdBody);
            sb.Append(@$"<item turbo=""true"">
                    <title>{newsView.Title}</title>
                    <guid isPermaLink=""true"">{domain}/news/{newsView.Id}</guid>
                    <link>{domain}/news/{newsView.Id}</link>
                    <description><![CDATA[<img src=""{domain}{newsView.ImgLink}""></img><br>
{content}]]></description>
                    <pubDate>{newsView.CreateDate:u}</pubDate>
                    <turbo:content><![CDATA[{content}]]>
                    </turbo:content>
                    </item>"
            );
        }

        sb.Append(@"</channel></rss>");
        return Content(sb.ToString(), MediaTypeHeaderValue.Parse("text/xml; charset=utf-8"));
    }
}