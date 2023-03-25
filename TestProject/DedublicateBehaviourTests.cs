namespace TestProject;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Pets.Infrastructure.Mediatr;

using Xunit;

public class DedublicateBehaviourTests
{
    [MediatRDedublicateExecution(KeyPropertyName = nameof(Cuid), ThrottlingTimeMs = 500)]
    public record PingSkip(Int32 Cuid) : IRequest<Int32>;

    private class PingSkipHandler : IRequestHandler<PingSkip, Int32>
    {
        public static Int32 Count;
        private readonly Object _l = new();

        public async Task<Int32> Handle(PingSkip request, CancellationToken cancellationToken)
        {
            lock (_l)
            {
                Count++;
            }

            await Task.Delay(100, cancellationToken);
            return Count;
        }
    }

    [MediatRDedublicateExecution(KeyPropertyName = nameof(Cuid), ThrottlingTimeMs = 50)]
    public record Ping(Int32 Cuid) : IRequest;

    private class PingHandler : IRequestHandler<Ping>
    {
        public static Int32 Count;
        private readonly Object _l = new();

        public async Task Handle(Ping request, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken);
            lock (_l)
            {
                Count++;
            }
        }
    }

    [MediatRDedublicateExecution(KeyPropertyNames = new[] { nameof(Cuid), nameof(Param) },
        ThrottlingTimeMs = 500)]
    public record PingTwo(Int32 Cuid, String Param) : IRequest;

    private class PingTwoHandler : IRequestHandler<PingTwo>
    {
        public static Int32 Count;
        private readonly Object _l = new();

        public async Task Handle(PingTwo request, CancellationToken cancellationToken)
        {
            lock (_l)
            {
                Count++;
            }

            await Task.Delay(100, cancellationToken);
        }
    }

    public record PingFree(Int32 Cuid, String Param) : IRequest;

    private class PingFreeHandler : IRequestHandler<PingFree>
    {
        public static Int32 Count;
        private readonly Object _l = new();

        public async Task Handle(PingFree request, CancellationToken cancellationToken)
        {
            lock (_l)
            {
                Count++;
            }

            await Task.Delay(10, cancellationToken);
        }
    }

    [Fact]
    public async Task TestMediatRDedublicateExecution()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddMemoryCache();
        services.AddMediatorCommandDedublicateBehaviour();
        services.AddMediatR(c => { c.RegisterServicesFromAssemblyContaining<Ping>(); });
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        await Task.WhenAll(Enumerable.Range(0, 100).Select(async _ =>
        {
            await mediator.Send(new Ping(121212));
            PingHandler.Count.Should().Be(1);
        })).ConfigureAwait(false);
        PingHandler.Count.Should().Be(1);
        await Task.Delay(58);
        await Task.WhenAll(Enumerable.Range(0, 100).Select(async _ =>
        {
            await mediator.Send(new Ping(121212));
            PingHandler.Count.Should().Be(2);
        })).ConfigureAwait(false);

        await Task.WhenAll(Enumerable.Range(0, 100).Select(_ => mediator.Send(new PingTwo(121212, $"sfdsdsd{_ % 2}"))))
            .ConfigureAwait(false);
        await Task.WhenAll(Enumerable.Range(0, 10).Select(_ => mediator.Send(new PingFree(121212, $"sfdsdsd{_ % 2}"))))
            .ConfigureAwait(false);
        await Task.WhenAll(Enumerable.Range(0, 10).Select(_ => mediator.Send(new PingSkip(121212))))
            .ConfigureAwait(false);

        PingHandler.Count.Should().Be(2);
        PingTwoHandler.Count.Should().Be(2);
        PingFreeHandler.Count.Should().Be(10);
        PingSkipHandler.Count.Should().Be(1);
    }
}