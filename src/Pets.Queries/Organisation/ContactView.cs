namespace Pets.Queries.Organisation;

using Types;

/// <summary>
/// 
/// </summary>
/// <param name="ImgLink">Ссылка на значок</param>
/// <param name="MdBody">Тело в markdown</param>
/// <param name="ContactType">Тип контакта</param>
public sealed record class ContactView(String? ImgLink, String? MdBody, ContactType ContactType);