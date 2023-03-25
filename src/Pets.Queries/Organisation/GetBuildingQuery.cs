namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Core.Mediatr;

/// <summary>
///     Запрос на получение списка стройматериалов
/// </summary>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId) },
    ThrottlingTimeMs = 2000)]
public sealed class GetBuildingQuery : IRequest<IEnumerable<ResourceView>>
{
    public GetBuildingQuery(Guid organisationId)
    {
        OrganisationId
            = organisationId;
    }

    /// <summary>
    ///     Организация которой принадлежат строй материалы
    /// </summary>
    public Guid OrganisationId { get; }
}