namespace PollyExamples.Benchmarks;

public static class Workloads
{
    public static void Action() { }

    public static Task ActionAsync() 
        => Task.CompletedTask;

    public static Task ActionAsync(CancellationToken cancellationToken) 
        => Task.CompletedTask;

    public static async Task ActionInfiniteAsync()
    {
        while (true)
        {
            await Task.Yield();
        }
    }

    public static async Task ActionInfiniteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Yield();
        }
    }

    public static T Func<T>() where T : struct
        => default;

    public static Task<T> FuncAsync<T>() where T : struct
        => Task.FromResult<T>(default);

    public static Task<T> FuncAsync<T>(CancellationToken cancellationToken) where T : struct
        => Task.FromResult<T>(default);

    public static TResult FuncThrows<TResult, TException>() where TException : Exception, new()
        => throw new TException();

    public static Task<TResult> FuncThrowsAsync<TResult, TException>() where TException : Exception, new()
        => throw new TException();
}