namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Core.Mediatr;

/// <summary>
/// Запрос на получение списка стройматериалов
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат строй материалы</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId) },
    ThrottlingTimeMs = 2000)]
public sealed record GetBuildingQuery(Guid OrganisationId) : IRequest<IEnumerable<ResourceView>>;