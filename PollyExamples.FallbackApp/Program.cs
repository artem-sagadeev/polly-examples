using Polly;

namespace PollyExamples.FallbackApp;

public class Program
{
    public static void Main(string[] args)
    {
        var fallbackPolicy = Policy<string>
            .Handle<Exception>()
            .Fallback(BackupServer.GetDefaultValue,
                onFallback: _ => Console.WriteLine("Warning: Main Server doesn't feel well"));

        var result = fallbackPolicy.Execute(MainServer.GetImportantValue);
        Console.WriteLine(result);
    }
}

public class MainServer
{
    public static string GetImportantValue() => throw new Exception();
}

public class BackupServer
{
    public static string GetDefaultValue() => "Default value";
}