namespace Pets.Queries.Documents;

/// <summary>
///     Запрос на получение картинки по id картинки
/// </summary>
public sealed class GetImgQuery : IRequest<DocumentInfo?>
{
    public GetImgQuery(Guid imageId)
    {
        ImageId
            = imageId;
    }

    /// <summary>
    ///     Картинка
    /// </summary>
    public Guid ImageId { get; }
}