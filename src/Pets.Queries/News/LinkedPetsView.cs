namespace Pets.Queries.News;

/// <summary>
///     Список связанных с новостью животных
/// </summary>
public sealed class LinkedPetsView
{
    public LinkedPetsView(Guid id, String name)
    {
        (Id, Name)
            = (id, name);
    }

    /// <summary>
    ///     Идентификатор животного
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Имя животного
    /// </summary>
    public String Name { get; }
}