namespace Pets.Infrastructure.Mediatr;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DiExtensions
{
    public static IServiceCollection AddMediatorCommandDedublicateBehaviour(this IServiceCollection services)
    {
        services.TryAddSingleton<IMediatorThrottlingService, MediatorThrottlingService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandDedublicateBehaviour<,>));
        return services;
    }
}