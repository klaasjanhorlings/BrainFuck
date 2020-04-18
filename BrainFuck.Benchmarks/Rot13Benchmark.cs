using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BrainFuck.Core;
using System.IO;
using System.Text;

namespace BrainFuck.Benchmarks
{
    [MemoryDiagnoser]
    public class Rot13Benchmark : BrainFuckBenchmarkBase
    {
        private Core.Program program;
        private MemoryStream inputStream;
        private MemoryStream outputStream;

        [Params(10_000, 100_000)]
        public int N;


        [GlobalSetup]
        public void GlobalSetup()
        {
            using (var programStream = new MemoryStream(Encoding.ASCII.GetBytes(Programs.Rot13)))
            {
                program = Compiler.Compile(programStream);
            }

            var baseString = Encoding.ASCII.GetBytes("Brainfuck is awesome!");
            var input = new byte[N];
            for (var i = 0; i < N - 1; i++)
            {
                input[i] = baseString[i % baseString.Length];
            }
            inputStream = new MemoryStream(input);
            outputStream = new MemoryStream(N);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            inputStream.Position = 0;
            outputStream.Position = 0;
        }

        [Benchmark]
        public void Rot13()
        {
            program(inputStream, outputStream);
        }
    }
}
