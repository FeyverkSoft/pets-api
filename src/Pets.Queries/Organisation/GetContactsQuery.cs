namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Core.Mediatr;

/// <summary>
///     Запрос на получение списка контактной информации
/// </summary>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId) },
    ThrottlingTimeMs = 2000)]
public sealed class GetContactsQuery : IRequest<IEnumerable<ContactView>>
{
    public GetContactsQuery(Guid organisationId)
    {
        OrganisationId
            = organisationId;
    }

    /// <summary>
    ///     Организация которой принадлежат реквезиты
    /// </summary>
    public Guid OrganisationId { get; }
}