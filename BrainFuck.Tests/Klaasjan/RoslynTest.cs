using BrainFuck.Core;
using BrainFuck.Implementations.Klaasjan.Roslyn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrainFuck.Tests.Klaasjan
{
    [TestClass]
    public class RoslynTest : TestBase
    {
        protected override ICompiler GetCompiler() => new RoslynCompiler();
    }
}
