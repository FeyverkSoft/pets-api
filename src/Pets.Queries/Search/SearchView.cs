namespace Pets.Queries.Search;

using Types;

/// <summary>
///     Сводка о найденном объекте
/// </summary>
public sealed class SearchView
{
    public SearchView(String id, SearchObjectType type, String img, String title, String shortText)
    {
        (Id, Type, Img, Title, ShortText)
            = (id, type, img, title, shortText);
    }

    /// <summary>
    ///     идентификатор найденного объекта
    /// </summary>
    public String Id { get; }

    /// <summary>
    ///     Тип найденного объекта.
    /// </summary>
    public SearchObjectType Type { get; }

    /// <summary>
    ///     Картинка превью к объекту
    /// </summary>
    public String Img { get; }

    /// <summary>
    ///     Заголовок
    /// </summary>
    public String Title { get; }

    /// <summary>
    ///     Краткое описание
    /// </summary>
    public String ShortText { get; }
}