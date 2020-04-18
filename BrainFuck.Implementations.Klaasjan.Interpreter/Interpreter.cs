using BrainFuck.Core;
using System.IO;

namespace BrainFuck.Implementations.Klaasjan.Interpreter
{
    public class Interpreter : ICompiler
    {
        public override string ToString() => "Interpreter";

        public Program Compile(Stream input)
        {
            var code = string.Empty;
            using (var reader = new StreamReader(input))
            {
                code = reader.ReadToEnd();
            }

            var program = new InterpretedProgram(code);
            return program.Execute;
        }

    }
}
