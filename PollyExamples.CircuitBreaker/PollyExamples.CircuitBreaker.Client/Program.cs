using Polly;
using Polly.CircuitBreaker;

var withoutBreaker = WithoutBreaker();
var withBreaker = WithBreaker();

await Task.WhenAll(withoutBreaker, withBreaker);

var withoutBreakerResult = withoutBreaker.Result;
var withBreakerResult = withBreaker.Result;
Console.WriteLine("         Without breaker        With breaker");
Console.WriteLine($"All:          {withoutBreakerResult.requests}                 {withBreakerResult.requests}");
Console.WriteLine($"Succeed:      {withoutBreakerResult.succeed}                 {withBreakerResult.succeed}");
Console.WriteLine($"Stopped:      {withoutBreakerResult.stopped}                    {withBreakerResult.stopped}");
Console.WriteLine($"Failed:       {withoutBreakerResult.failed}                 {withBreakerResult.failed}");

async Task<(int requests, int succeed, int stopped, int failed)> WithoutBreaker()
{
    var requests = 0;
    var succeedRequests = 0;
    var stoppedRequests = 0;
    var failedRequests = 0;

    var httpClient = new HttpClient();

    var end = DateTime.UtcNow.AddSeconds(6);

    while (DateTime.UtcNow < end)
    {
        requests++;

        try
        {
            var result = await httpClient.GetAsync("http://localhost:5199/WeatherForecast");
            result.EnsureSuccessStatusCode();

            succeedRequests++;
        }
        catch (BrokenCircuitException)
        {
            stoppedRequests++;
        }
        catch (Exception)
        {
            failedRequests++;
        }
    }

    return (requests, succeedRequests, stoppedRequests, failedRequests);
}

async Task<(int requests, int succeed, int stopped, int failed)> WithBreaker()
{
    var requests = 0;
    var succeedRequests = 0;
    var failedRequests = 0;
    var stoppedRequests = 0;

    var breaker = Policy
        .Handle<HttpRequestException>()
        .CircuitBreakerAsync(
            exceptionsAllowedBeforeBreaking: 2, 
            durationOfBreak: TimeSpan.FromMilliseconds(100)
        );

    var httpClient = new HttpClient();

    var end = DateTime.UtcNow.AddSeconds(6);

    while (DateTime.UtcNow < end)
    {
        requests++;

        try
        {
            await breaker.ExecuteAsync(async () =>
            {
                var result = await httpClient.GetAsync("http://localhost:5199/WeatherForecast");
                result.EnsureSuccessStatusCode();
            });

            succeedRequests++;
        }
        catch (BrokenCircuitException)
        {
            stoppedRequests++;
        }
        catch (Exception)
        {
            failedRequests++;
        }
    }

    return (requests, succeedRequests, stoppedRequests, failedRequests);
}

