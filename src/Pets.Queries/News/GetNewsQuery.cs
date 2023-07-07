namespace Pets.Queries.News;

/// <summary>
/// 
/// </summary>
/// <param name="OrganisationId">Организация к которой относятся новости</param>
/// <param name="Offset"></param>
/// <param name="Limit"></param>
/// <param name="PetId">Идентификатор животного по которому выбираются новости</param>
/// <param name="Tag">Тег для которого выбирается новость</param>
/// <param name="NewsId">идентификатор конкретной новости</param>
public sealed record GetNewsQuery
    (Guid OrganisationId, Guid? PetId = null, String? Tag = null, Guid? NewsId = null, Int32 Limit = 8, Int32 Offset = 0) : PageQuery<NewsView>(Limit: Limit, Offset: Offset);