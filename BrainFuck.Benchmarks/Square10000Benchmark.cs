using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BrainFuck.Core;
using System.IO;
using System.Text;

namespace BrainFuck.Benchmarks
{
    [MemoryDiagnoser]
    public class Square10000Benchmark : BrainFuckBenchmarkBase
    {
        private Core.Program program;
        private MemoryStream inputStream;
        private MemoryStream outputStream;

        [GlobalSetup]
        public void GlobalSetup()
        {
            using (var programStream = new MemoryStream(Encoding.ASCII.GetBytes(Programs.Square10000)))
            {
                program = Compiler.Compile(programStream);
            }

            inputStream = new MemoryStream();
            outputStream = new MemoryStream();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            inputStream.Position = 0;
            outputStream.Position = 0;
        }

        [Benchmark]
        public void Square10000()
        {
            program(inputStream, outputStream);
        }
    }
}
