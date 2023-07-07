namespace Pets.Queries;

public abstract record PageQuery<T>(Int32 Limit, Int32 Offset) : IRequest<Page<T>> where T : class;