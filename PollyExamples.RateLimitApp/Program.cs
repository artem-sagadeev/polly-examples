using Polly;
using Polly.RateLimit;

// 5 запросов в 1 секунду
var rateLimitPolicy = Policy
    .RateLimitAsync(5, TimeSpan.FromSeconds(1));

for (var i = 0; i < 5; i++)
{
    try
    {
        await rateLimitPolicy.ExecuteAsync(async () =>
        {
            await Task.Delay(10); // 10 миллисекунд
            Console.WriteLine($"Request {i + 1}: OK");
        });
    }
    catch (RateLimitRejectedException ex)
    {
        Console.WriteLine($"Request {i + 1}: RateLimitRejectedException - Retry after: {ex.RetryAfter}");
    }
}