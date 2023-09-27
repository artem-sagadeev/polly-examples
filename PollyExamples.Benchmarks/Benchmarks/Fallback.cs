using BenchmarkDotNet.Attributes;
using Polly;

namespace PollyExamples.Benchmarks.Benchmarks;

[Config(typeof(PollyConfig))]
public class Fallback
{
    private static readonly Policy<int> SyncPolicy =
        Policy<int>
            .Handle<InvalidOperationException>()
            .Fallback(0);

    private static readonly AsyncPolicy<int> AsyncPolicy =
        Policy<int>
            .Handle<InvalidOperationException>()
            .FallbackAsync(0);

    [Benchmark]
    public int Fallback_Sync_Succeeds()
    {
        return SyncPolicy.Execute(Workloads.Func<int>);
    }

    [Benchmark]
    public Task<int> Fallback_Async_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>);
    }

    [Benchmark]
    public int Fallback_Sync_Throws()
    {
        return SyncPolicy.Execute(Workloads.FuncThrows<int, InvalidOperationException>);
    }

    [Benchmark]
    public Task<int> Fallback_Async_Throws()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncThrowsAsync<int, InvalidOperationException>);
    }
}