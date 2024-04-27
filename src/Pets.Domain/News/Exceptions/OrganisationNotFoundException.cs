namespace Pets.Domain.News.Exceptions;

using Types.Exceptions;

/// <summary>
///     Указанной организации не существует в системе
/// </summary>
public sealed class OrganisationNotFoundException : NotFoundException
{
    public OrganisationNotFoundException(Guid organisationId) : base($"organisation id: {organisationId} not found;")
    {
    }
}