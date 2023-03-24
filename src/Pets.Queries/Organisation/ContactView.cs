namespace Pets.Queries.Organisation;

using Types;

public sealed class ContactView
{
    public ContactView(String? imgLink, String? mdBody, ContactType contactType)
    {
        (ImgLink, MdBody, ContactType)
            = (imgLink, mdBody, contactType);
    }

    /// <summary>
    ///     Ссылка на значок
    /// </summary>
    public String? ImgLink { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; }

    /// <summary>
    ///     Тип контакта
    /// </summary>
    public ContactType ContactType { get; }
}