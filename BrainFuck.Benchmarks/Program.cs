using BenchmarkDotNet.Running;

namespace BrainFuck.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ReverseBenchmark>();
            BenchmarkRunner.Run<Rot13Benchmark>();
        }
    }
}
