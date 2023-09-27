using BenchmarkDotNet.Attributes;
using Polly;

namespace PollyExamples.Benchmarks.Benchmarks;

[Config(typeof(PollyConfig))]
public class CircuitBreaker
{
    private static readonly Policy SyncPolicy =
        Policy
            .Handle<InvalidOperationException>()
            .CircuitBreaker(2, TimeSpan.FromMinutes(1));

    private static readonly AsyncPolicy AsyncPolicy =
        Policy
            .Handle<InvalidOperationException>()
            .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

    [Benchmark]
    public void CircuitBreaker_Sync_Succeeds()
    {
        SyncPolicy.Execute(Workloads.Action);
    }

    [Benchmark]
    public Task CircuitBreaker_Async_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync, CancellationToken.None);
    }

    [Benchmark]
    public int CircuitBreaker_Sync_With_Result_Succeeds()
    {
        return SyncPolicy.Execute(Workloads.Func<int>);
    }

    [Benchmark]
    public Task<int> CircuitBreaker_Async_With_Result_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>, CancellationToken.None);
    }
}