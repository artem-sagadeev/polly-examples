using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace PollyExamples.Benchmarks;

public class PollyConfig : ManualConfig
{
    public PollyConfig()
    {
        AddDiagnoser(BenchmarkDotNet.Diagnosers.MemoryDiagnoser.Default);
        //AddJob(Job.Default.WithNuGet("Polly", "7.2.4"));
    }
}