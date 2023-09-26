using Polly;
using Polly.Timeout;

//await GetWithoutTimeout();

await GetWithTimeout();


async Task GetWithoutTimeout()
{
    Console.WriteLine("-------- Start - without timeout --------");

    await ApiCall();

    Console.WriteLine("-------- End --------");
}

async Task GetWithTimeout()
{
    Console.WriteLine("-------- Start - with timeout --------");
    
    var timeoutPolicy = Policy.TimeoutAsync(3, TimeoutStrategy.Pessimistic, (_, timespan, task) => 
    {
        if (!task.IsCompleted)
        {
            Console.WriteLine($"Request cancelled after waiting {timespan.Seconds}.{timespan.Milliseconds} seconds");
        }

        return Task.CompletedTask;
    });

    try
    {
        await timeoutPolicy.ExecuteAsync(ApiCall);
    }
    catch (TimeoutRejectedException)
    {
    }

    Console.WriteLine("-------- End --------");
}

async Task ApiCall()
{
    var httpClient = new HttpClient();
    var responseTask = httpClient.GetAsync("http://localhost:5075/WeatherForecast");

    while (!responseTask.IsCompleted)
    {
        Console.WriteLine("Waiting...");
        await Task.Delay(1000); //ждём 1 секунду
    }

    var result = await responseTask.Result.Content.ReadAsStringAsync();
    Console.WriteLine(result);
}
