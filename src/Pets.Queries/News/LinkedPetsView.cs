namespace Pets.Queries.News;

/// <summary>
///     Список связанных с новостью животных
/// </summary>
/// <param name="Id">Идентификатор животного</param>
/// <param name="Name">Имя животного</param>
public sealed record LinkedPetsView(Guid Id, String Name);