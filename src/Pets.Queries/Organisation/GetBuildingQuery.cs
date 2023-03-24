namespace Pets.Queries.Organisation;

using System.Collections.Generic;

/// <summary>
///     Запрос на получение списка стройматериалов
/// </summary>
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