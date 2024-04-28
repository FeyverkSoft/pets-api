namespace Pets.Domain.News;

using System.Collections.Generic;
using System.Linq;

using Core;

using Entity;

using Exceptions;

using Organisation;

using Specs;

using Types.Exceptions;

using ValueTypes;

public sealed class NewsService : INewsCreateService
{
    private readonly IOrganisationGetter _organisationGetter;
    private readonly IPetGetter _petGetter;
    private readonly INewsRepository _newsRepository;
    private readonly IDateTimeGetter _dateTimeGetter;

    public NewsService(IOrganisationGetter organisationGetter, INewsRepository newsRepository, IPetGetter petGetter, IDateTimeGetter dateTimeGetter)
    {
        _organisationGetter = organisationGetter;
        _petGetter = petGetter;
        _newsRepository = newsRepository;
        _dateTimeGetter = dateTimeGetter;

    }

    /// <summary>
    ///     Создать новость
    /// </summary>
    /// <param name="organisation">Идентификатор организации</param>
    /// <param name="id">Идентификатор новости</param>
    /// <param name="request">Параметры создаваемой новости</param>
    /// <param name="cancellationToken">Токен признака отмены запроса</param>
    /// <exception cref="IdempotencyCheckException"></exception>
    /// <exception cref="OrganisationNotFoundException"></exception>
    /// <exception cref="NewsAlreadyExistsException"></exception>
    /// <returns></returns>
    public async Task<Guid> Create(Organisation organisation, Guid id, INewsCreateService.CreateNews request, CancellationToken cancellationToken)
    {
        var petsIds = request.LinkedPets?.Distinct() ?? new List<Guid>();
        var (org, existsNews) = await (
            _organisationGetter.GetAsync(organisation, cancellationToken),
            _newsRepository.GetAsync(NewSpecs.IsSatisfiedById(id, organisation), cancellationToken));
        var pets = await _petGetter.GetAsync(organisation, petsIds, cancellationToken);
        
        if (org is null)
        {
            throw new OrganisationNotFoundException(organisation);
        }

        if (pets.Count() != petsIds.Count())
        {
            var exPId = petsIds.Except(pets.Select(_ => _.Id));
            throw new LinkedPetsNotFoundException(exPId);
        }
        var newNews = News.Create(
            id: id,
            organisation: organisation,
            title: request.Title,
            mdBody: request.MdBody,
            mdShortBody: request.MdShortBody,
            imgLink: request.ImgLink,
            createDate: _dateTimeGetter.Get());
        
        if (existsNews is not null)
        {
            if (!existsNews.IsIdempotence().IsSatisfiedBy(newNews))
            {
                throw new NewsAlreadyExistsException(id);
            }

            return id;
        }

        newNews.AddTags(request.Tags, _dateTimeGetter.Get());
        newNews.AddPets(pets, _dateTimeGetter.Get());
        await _newsRepository.SaveAsync(newNews, cancellationToken);
        return id;
    }
}