namespace Pets.Queries.Organisation;

using System.Collections.Generic;

using Types;

public sealed class NeedView
{
    public NeedView(IEnumerable<String?> imgsLink, String? mdBody, NeedState state)
    {
        (ImgsLink, MdBody, State)
            = (imgsLink, mdBody, state);
    }

    /// <summary>
    ///     Ссылка на фотографию материала
    /// </summary>
    public IEnumerable<String?> ImgsLink { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; }

    /// <summary>
    ///     Видимость заказа
    /// </summary>
    public NeedState State { get; }
}