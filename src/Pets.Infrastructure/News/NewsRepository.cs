namespace Pets.Infrastructure.News;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Core;

using Domain.News;
using Domain.News.Entity;
using Domain.ValueTypes;

using Microsoft.EntityFrameworkCore;

public sealed class NewsRepository : INewsRepository, IPetGetter
{
    private readonly NewsDbContext _context;

    public NewsRepository(NewsDbContext context)
    {
        _context = context;
    }


    /// <summary>
    ///     Получить новость
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<News?> GetAsync(Specification<News> specification, CancellationToken cancellationToken) =>
        await _context.News
            .SingleOrDefaultAsync(specification, cancellationToken);


    /// <summary>
    ///     Сохранить новость
    /// </summary>
    /// <param name="news"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SaveAsync(News news, CancellationToken cancellationToken)
    {
        var entry = _context.Entry(news);
        if (entry.State == EntityState.Detached)
            await _context.News.AddAsync(news, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// get Pets by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Pet?>> GetAsync(Organisation organisation, IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
        => await _context.Pets.AsNoTracking()
            .Where(_ => ids.Contains(_.Id))
            .ToListAsync(cancellationToken);
}