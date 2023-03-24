namespace Pets.Queries.Organisation;

using System.Collections.Generic;

/// <summary>
///     Запрос на получение списка нужд
/// </summary>
public sealed class GetNeedQuery : IRequest<IEnumerable<NeedView>>
{
    public GetNeedQuery(Guid organisationId)
    {
        OrganisationId
            = organisationId;
    }

    /// <summary>
    ///     Организация
    /// </summary>
    public Guid OrganisationId { get; }
}