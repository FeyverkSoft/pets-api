namespace Pets.Queries.Organisation;

using Types;

/// <summary>
/// 
/// </summary>
/// <param name="ImgLink">Ссылка на фотографию материала</param>
/// <param name="Title">Заголовок в markdown</param>
/// <param name="MdBody">Тело в markdown</param>
/// <param name="State">Состояние сбора</param>
public sealed record ResourceView(String? ImgLink, String? Title, String? MdBody, ResourceState State);