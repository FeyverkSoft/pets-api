namespace Pets.Queries.Pages.Admin;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Id</param>
/// <param name="OrganisationId">Id организации</param>
/// <param name="ImgLink">Ссылка на фотку шапки новости</param>
/// <param name="MdBody">Тело в markdown</param>
/// <param name="UpdateDate">Дата последнего изменения</param>
public sealed record AdminPageView(String Id, Guid OrganisationId, String ImgLink, String? MdBody, DateTime UpdateDate);