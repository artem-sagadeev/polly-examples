using Polly;

var random = new Random(DateTime.UtcNow.Millisecond);
var retryPolicy = Policy
    .Handle<Exception>()
    .Retry(10);

var waitAndRetryPolicy = Policy
    .Handle<Exception>()
    .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 2));

var foreverRetryPolicy = Policy
    .Handle<Exception>()
    .RetryForever();

retryPolicy.Execute(Action);
waitAndRetryPolicy.Execute(Action);
foreverRetryPolicy.Execute(Action);


void Action()
{
    if (random.Next(100) <= 60)
    {
        Console.WriteLine($"Failed.");
        throw new Exception();
    }

    Console.WriteLine("Ok.");
}
