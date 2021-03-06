using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Контроллер для генерации статики для поисковика Х.яндекса
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public sealed class YandexSeoController : ControllerBase
    {
        [HttpGet("/yandex/{organisationId}/pets/{petId}")]
        public async Task<IActionResult> GetPetRss(
            [FromServices] IQueryProcessor _processor,
            [FromServices] IMarkdown _markdown,
            [FromRoute] Guid organisationId,
            [FromRoute] Guid petId,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: organisationId,
                petId: petId,
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
            if (result.Total == 0)
                return NotFound();
            var pet = result.Items.First();
            var content = await _markdown.Parse(String.IsNullOrEmpty(pet.MdBody) ? pet.MdShortBody : pet.MdBody);
            var sb = new StringBuilder(@"<html lang=""ru""><head><meta charset=""utf-8""/>");
            sb.Append($"<title>Добродом - наши питомцы: {pet.Name}</title>");
            sb.Append($@"<meta name=""description"" content=""Добродом, помощь бездомным животным в Оренбурге, {pet.Name}"">");
            sb.Append($@"<meta name=""keywords"" content=""Добродом, Оренбург, помощь бездомным животным в Оренбурге, {pet.Name}, {await _markdown.Parse(pet.MdShortBody)}""/>");
            sb.Append("</head><body>");
            sb.Append(content);
            sb.Append("</body></html>");
            return Content(sb.ToString(), MediaTypeHeaderValue.Parse("text/html; charset=utf-8"));
        }
    }
}