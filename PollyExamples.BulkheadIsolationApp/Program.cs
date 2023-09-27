using Polly;

var bulkheadPolicy = Policy.BulkheadAsync(5, 5);


var tasks = Enumerable
    .Range(1, 10)
    .Select(number => Task.Run(async () =>
        await bulkheadPolicy.ExecuteAsync(async () => await ActionAsync(number))))
    .ToArray();

await Task.WhenAll(tasks);

Console.WriteLine("All task completed.");

static async Task ActionAsync(int taskNumber)
{
    await Task.Delay(2000);

    Console.WriteLine($"{DateTime.UtcNow}: Task {taskNumber} ended.");
}
