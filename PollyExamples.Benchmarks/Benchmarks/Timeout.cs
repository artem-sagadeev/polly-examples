using BenchmarkDotNet.Attributes;
using Polly;

namespace PollyExamples.Benchmarks.Benchmarks;

[Config(typeof(PollyConfig))]
public class Timeout
{
    private static readonly Policy SyncPolicy =
        Policy
            .Timeout(TimeSpan.FromMilliseconds(1));

    private static readonly AsyncPolicy AsyncPolicy =
        Policy
            .TimeoutAsync(TimeSpan.FromMilliseconds(1));

    [Benchmark]
    public void Timeout_Sync_Succeeds()
    {
        SyncPolicy.Execute(Workloads.Action);
    }

    [Benchmark]
    public Task Timeout_Async_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync);
    }

    [Benchmark]
    public Task Timeout_Async_Succeeds_With_CancellationToken()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync, CancellationToken.None);
    }

    [Benchmark]
    public int Timeout_Sync_With_Result_Succeeds()
    {
        return SyncPolicy.Execute(Workloads.Func<int>);
    }

    [Benchmark]
    public Task<int> Timeout_Async_With_Result_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>);
    }

    [Benchmark]
    public Task<int> Timeout_Async_With_Result_Succeeds_With_CancellationToken()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>, CancellationToken.None);
    }

    [Benchmark]
    public Task Timeout_Async_Times_Out_Optimistic()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionInfiniteAsync, CancellationToken.None);
    }
}