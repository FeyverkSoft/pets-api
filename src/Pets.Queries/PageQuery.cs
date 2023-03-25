namespace Pets.Queries;

public abstract class PageQuery<T> : IRequest<Page<T>> where T : class
{
    protected PageQuery(Int32 offset, Int32 limit)
    {
        Offset = offset;
        Limit = limit;
    }

    public Int32 Limit { get; }

    public Int32 Offset { get; }
}