namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Core.Mediatr;


/// <summary>
///     Запрос на получение списка нужд
/// </summary>
/// <param name="OrganisationId">Организация</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId) },
    ThrottlingTimeMs = 2000)]
public sealed record GetNeedQuery(Guid OrganisationId) : IRequest<IEnumerable<NeedView>>;