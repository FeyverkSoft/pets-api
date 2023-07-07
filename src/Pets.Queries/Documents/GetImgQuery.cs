namespace Pets.Queries.Documents;

/// <summary>
///     Запрос на получение картинки по id картинки
/// </summary>
/// <param name="ImageId">Картинка</param>
public sealed record GetImgQuery(Guid ImageId) : IRequest<DocumentInfo?>;