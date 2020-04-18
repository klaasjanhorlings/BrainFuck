using BrainFuck.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Runtime.Loader;
using System.Text;

namespace BrainFuck.Implementations.Klaasjan.Roslyn
{
    public class RoslynCompiler : ICompiler
    {
        public override string ToString() => "Roslyn";

        public const string File = @"
using System;
using System.IO;

namespace BrainFuck
{{
    public class RoslynProgram
    {{
        public static void Execute(Stream input, Stream output)
        {{
            var lint = new byte[1 << 15];
            var pointer = 0;
            var buffer = new byte[1];

            {0}
        }}
    }}
}}";

        public Program Compile(Stream input)
        {
            int bytesRead;
            var buffer = new byte[4096];
            var source = new StringBuilder();
            do
            {
                bytesRead = input.Read(buffer, 0, buffer.Length);
                for (var i = 0; i < bytesRead; i++)
                {
                    var next = buffer[i];

                    switch (next)
                    {
                        case (byte)'+':
                            source.AppendLine("lint[pointer]++;");
                            break;

                        case (byte)'-':
                            source.AppendLine("lint[pointer]--;");
                            break;

                        case (byte)'>':
                            source.AppendLine("pointer++;");
                            break;

                        case (byte)'<':
                            source.AppendLine("pointer--;");
                            break;

                        case (byte)'.':
                            source.AppendLine("buffer[0] = lint[pointer];");
                            source.AppendLine("output.Write(buffer, 0, 1);");
                            break;

                        case (byte)',':
                            source.AppendLine("input.Read(buffer, 0, 1);");
                            source.AppendLine("lint[pointer] = buffer[0];");
                            break;

                        case (byte)'[':
                            source.AppendLine("while(lint[pointer] != 0)");
                            source.AppendLine("{");
                            break;

                        case (byte)']':
                            source.AppendLine("}");
                            break;
                    }
                }
            } while (bytesRead > 0);

            var s = string.Format(File, source.ToString());

            var program = CSharpSyntaxTree.ParseText(s);
            var assemblyName = Guid.NewGuid().ToString();
            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { program },
                references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                stream.Position = 0;
                var assembly = AssemblyLoadContext.Default.LoadFromStream(stream);
                var type = assembly.GetType("BrainFuck.RoslynProgram");
                var method = type.GetMethod("Execute");
                var o = Activator.CreateInstance(type);

                return (Stream input, Stream output) => method.Invoke(o, new[] { input, output });
            }
        }
    }
}
