namespace Pets.Infrastructure.Mediatr;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

public interface IMediatorThrottlingService
{
    /// <summary>
    /// Создать Тротлинг блокировку
    /// не допускать повторных вызовов обработчика.
    /// Но и просто скипать мы их не можем, так тогда отальные параллельные функции получат не верный ответ.
    /// так что все следубщие запросы к обработчику, ожидают ответ первого запроса
    /// </summary>
    /// <param name="key"></param>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    bool ReleaseLock<TResponse>(string key, RequestHandlerDelegate<TResponse> next, out Task<TResponse> task, TimeSpan timeSpan);
}

public sealed class MediatorThrottlingService : IMediatorThrottlingService
{
    private readonly IMemoryCache _locks;

    public MediatorThrottlingService(IMemoryCache locks)
    {
        _locks = locks;
    }

    /// <summary>
    /// Создать Тротлинг блокировку
    /// не допускать повторных вызовов обработчика.
    /// Но и просто скипать мы их не можем, так тогда отальные параллельные функции получат не верный ответ.
    /// так что все следубщие запросы к обработчику, ожидают ответ первого запроса
    /// </summary>
    /// <param name="key"></param>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public bool ReleaseLock<TResponse>(string key, RequestHandlerDelegate<TResponse> next, out Task<TResponse> task, TimeSpan timeSpan)
    {
        lock (_locks)
        {
            if (_locks.TryGetValue(key, out Task<TResponse> value))
            {
                task = value;
                // если запись есть, то значит этот обработчик для этого пользовыателя вызвали параллельно несколько точек.
                return false;
            }

            task = next();
            _locks.Set(key, task, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.Add(timeSpan)
            });
        }

        return true;
    }
}