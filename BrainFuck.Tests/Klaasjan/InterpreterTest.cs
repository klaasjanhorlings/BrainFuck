using BrainFuck.Core;
using BrainFuck.Implementations.Klaasjan.Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrainFuck.Tests.Klaasjan
{
    [TestClass]
    public class InterpreterTest : TestBase
    {
        protected override ICompiler GetCompiler() => new Interpreter();
    }
}
