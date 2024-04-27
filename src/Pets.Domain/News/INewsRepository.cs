namespace Pets.Domain.News;

using Core;

using Entity;

public interface INewsRepository
{
    /// <summary>
    ///     Получить новость
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<News?> GetAsync(Specification<News> specification, CancellationToken cancellationToken);

    /// <summary>
    ///     Сохранить новость
    /// </summary>
    /// <param name="news"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveAsync(News news, CancellationToken cancellationToken);
}