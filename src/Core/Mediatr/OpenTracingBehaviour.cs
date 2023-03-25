namespace Core.Mediatr;

/*
public class OpenTracingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ITracer _tracer;

    public OpenTracingBehaviour(ITracer tracer)
    {
        _tracer = tracer;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var type = typeof(TRequest).Name;
        var scope = CreateTracingScope(type);
        scope?.Span.SetTag(Tags.Component, $"{type}Handler");
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            scope?.Span.SetTag(Tags.Error.Key, e.Message);
            throw;
        }
        finally
        {
            scope?.Span.Finish();
            scope?.Dispose();
        }
    }

    private IScope? CreateTracingScope(string name)
    {
        try
        {
            return _tracer?.BuildSpan($"{name} call").StartActive(finishSpanOnDispose: false);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
*/