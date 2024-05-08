namespace Pets.Domain.News.Specs;

using Core;

using Entity;

using ValueTypes;

public static class NewSpecs
{
    public static Specification<News> IsSatisfiedById(Guid id, Organisation organisation) => new(x => x.Id == id && x.Organisation == organisation);

    public static Specification<News> IsIdempotence(this News news) => new(
        x =>
            (x.ImgLink != null && x.ImgLink.IgnoreCaseEquals(news.ImgLink)) &&
            x.Organisation.Equals(news.Organisation) &&
            x.Id == news.Id &&
            (x.Title != null && x.Title.IgnoreCaseEquals(news.Title)) &&
            (x.MdShortBody != null && x.MdShortBody.IgnoreCaseEquals(news.MdShortBody)) &&
            (x.MdBody != null && x.MdBody.IgnoreCaseEquals(news.MdBody)));
    
}