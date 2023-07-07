namespace Pets.Queries.Search;

using Types;

/// <summary>
///     Сводка о найденном объекте
/// </summary>
/// <param name="Id">идентификатор найденного объекта</param>
/// <param name="Type">Тип найденного объекта.</param>
/// <param name="Img">Картинка превью к объекту</param>
/// <param name="Title">Заголовок</param>
/// <param name="ShortText">Краткое описание</param>
public sealed record SearchView(String Id, SearchObjectType Type, String Img, String Title, String ShortText);