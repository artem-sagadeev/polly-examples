using BenchmarkDotNet.Attributes;
using Polly;

namespace PollyExamples.Benchmarks.Benchmarks;

[Config(typeof(PollyConfig))]
public class RateLimit
{
    private static readonly Policy SyncPolicy =
        Policy
            .RateLimit(20, TimeSpan.FromSeconds(1), int.MaxValue);

    private static readonly AsyncPolicy AsyncPolicy =
        Policy
            .RateLimitAsync(20, TimeSpan.FromSeconds(1), int.MaxValue);

    [Benchmark]
    public void RateLimit_Sync_Succeeds()
    {
        SyncPolicy.Execute(Workloads.Action);
    }

    [Benchmark]
    public Task RateLimit_Async_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync);
    }

    [Benchmark]
    public int RateLimit_Sync_With_Result_Succeeds()
    {
        return SyncPolicy.Execute(Workloads.Func<int>);
    }

    [Benchmark]
    public Task<int> RateLimit_Async_With_Result_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>);
    }
}