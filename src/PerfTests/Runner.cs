using BenchmarkDotNet.Running;

namespace PerfTests
{
    public class Runner
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(BenchmarkConverter.TypeToBenchmarks(typeof(ApplicationWorkflows)));
            BenchmarkRunner.Run(BenchmarkConverter.TypeToBenchmarks(typeof(OccurencePerfTests)));
            BenchmarkRunner.Run(BenchmarkConverter.TypeToBenchmarks(typeof(CalDateTimePerfTests)));
            BenchmarkRunner.Run(BenchmarkConverter.TypeToBenchmarks(typeof(SerializationPerfTests)));
            BenchmarkRunner.Run(BenchmarkConverter.TypeToBenchmarks(typeof(ThroughputTests)));
        }
    }
}
