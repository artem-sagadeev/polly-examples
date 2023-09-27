using BenchmarkDotNet.Attributes;
using Polly;

namespace PollyExamples.Benchmarks.Benchmarks;

[Config(typeof(PollyConfig))]
public class Retry
{
    private static readonly Policy SyncPolicy =
        Policy
            .Handle<InvalidOperationException>()
            .Retry();

    private static readonly AsyncPolicy AsyncPolicy =
        Policy
            .Handle<InvalidOperationException>()
            .RetryAsync();

    [Benchmark]
    public void Retry_Sync_Succeeds()
    {
        SyncPolicy.Execute(Workloads.Action);
    }

    [Benchmark]
    public Task Retry_Async_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync);
    }

    [Benchmark]
    public Task Retry_Async_Succeeds_With_CancellationToken()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.ActionAsync, CancellationToken.None);
    }

    [Benchmark]
    public int Retry_Sync_With_Result_Succeeds()
    {
        return SyncPolicy.Execute(Workloads.Func<int>);
    }

    [Benchmark]
    public Task<int> Retry_Async_With_Result_Succeeds()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>);
    }

    [Benchmark]
    public Task<int> Retry_Async_With_Result_Succeeds_With_CancellationToken()
    {
        return AsyncPolicy.ExecuteAsync(Workloads.FuncAsync<int>, CancellationToken.None);
    }

    [Benchmark]
    public void Retry_Sync_Throws_Then_Succeeds()
    {
        var count = 0;

        SyncPolicy.Execute(() =>
        {
            if (count++ % 2 == 0)
            {
                throw new InvalidOperationException();
            }
        });
    }

    [Benchmark]
    public Task Retry_Async_Throws_Then_Succeeds()
    {
        var count = 0;

        return AsyncPolicy.ExecuteAsync(() =>
        {
            if (count++ % 2 == 0)
            {
                throw new InvalidOperationException();
            }

            return Task.CompletedTask;
        });
    }
}