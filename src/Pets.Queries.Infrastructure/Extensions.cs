namespace Pets.Queries.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Pets;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection registry)
    {
        registry.AddMediatR(configuration => { configuration.RegisterServicesFromAssemblyContaining<PetsQueryHandler>(); });
        return registry;
    }
}