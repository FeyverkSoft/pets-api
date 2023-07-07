namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Core.Mediatr;

/// <summary>
/// Запрос на получение списка контактной информации
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат реквезиты</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId) },
    ThrottlingTimeMs = 2000)]
public sealed record GetContactsQuery(Guid OrganisationId) : IRequest<IEnumerable<ContactView>>;