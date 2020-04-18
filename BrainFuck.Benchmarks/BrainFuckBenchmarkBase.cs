using BenchmarkDotNet.Attributes;
using BrainFuck.Core;
using BrainFuck.Implementations.Klaasjan.Interpreter;
using BrainFuck.Implementations.Klaasjan.Roslyn;
using System.Collections.Generic;

namespace BrainFuck.Benchmarks
{
    public abstract class BrainFuckBenchmarkBase
    {
        [ParamsSource(nameof(Compilers))]
        public ICompiler Compiler;

        public IEnumerable<ICompiler> Compilers => new ICompiler[]
        {
            new Interpreter(),
            new RoslynCompiler()
        };
    }
}
