namespace Pets.Infrastructure.Mediatr;

using System.Reflection;
using System.Text;
using System.Threading;

using MediatR;

public sealed class CommandDedublicateBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IMediatorThrottlingService _mediatorThrottlingService;

    public CommandDedublicateBehaviour(IMediatorThrottlingService mediatorThrottlingService)
    {
        _mediatorThrottlingService = mediatorThrottlingService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var type = typeof(TRequest);
        var attr = type.GetCustomAttribute<MediatRDedublicateExecutionAttribute>();

        if (attr is null) return await next();

        var sb = new StringBuilder(type.FullName);
        if (!String.IsNullOrEmpty(attr.KeyPropertyName))
        {
            sb.Append(GetValue(type, attr.KeyPropertyName, request));
        }
        else
        {
            if (attr.KeyPropertyNames is not null)
                foreach (var propertyName in attr.KeyPropertyNames)
                    sb.Append(GetValue(type, propertyName, request));
        }

        if (_mediatorThrottlingService.ReleaseLock(sb.ToString(), next, out var task, TimeSpan.FromMilliseconds(attr.ThrottlingTimeMs))) return await task;

        return await task;
    }

    private static Object? GetValue<T>(Type type, String keyPropertyName, T request)
    {
        return type.GetProperty(keyPropertyName)?.GetValue(request) ??
               type.GetField(keyPropertyName)?.GetValue(request);
    }
}