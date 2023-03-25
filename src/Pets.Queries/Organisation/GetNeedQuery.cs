namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Core.Mediatr;

/// <summary>
///     Запрос на получение списка нужд
/// </summary>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId) },
    ThrottlingTimeMs = 2000)]
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