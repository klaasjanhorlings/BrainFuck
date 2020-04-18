using System.IO;

namespace BrainFuck.Core
{
    public interface ICompiler
    {
        Program Compile(Stream input);
    }
}
