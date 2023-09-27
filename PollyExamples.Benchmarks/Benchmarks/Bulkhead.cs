using BenchmarkDotNet.Attributes;
using Polly;

namespace PollyExamples.Benchmarks.Benchmarks;

[Config(typeof(PollyConfig))]
public class Bulkhead
{
    private static readonly Policy SyncPolicy =
        Policy
            .Bulkhead(2);

    private static readonly AsyncPolicy AsyncPolicy =
        Policy
            .BulkheadAsync(2);

    [Benchmark]
    public void Bulkhead_Sync()
    {
        SyncPolicy.Execute(Workloads.Action);
    }

    [Benchmark]
    public Task Bulkhead_Async()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync);
    }

    [Benchmark]
    public Task Bulkhead_Async_With_CancellationToken()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync, CancellationToken.None);
    }

    [Benchmark]
    public int Bulkhead_Sync_With_Result()
    {
        return SyncPolicy.Execute(Workloads.Func<int>);
    }

    [Benchmark]
    public Task<int> Bulkhead_Async_With_Result()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>);
    }

    [Benchmark]
    public Task<int> Bulkhead_Async_With_Result_With_CancellationToken()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>, CancellationToken.None);
    }
}