namespace Pets.Queries.Organisation;

using System.Collections.Generic;

/// <summary>
///     Запрос на получение списка контактной информации
/// </summary>
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