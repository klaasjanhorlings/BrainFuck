using System.IO;

namespace BrainFuck.Implementations.Klaasjan.Interpreter
{
    internal class InterpretedProgram
    {
        private readonly string program;

        public InterpretedProgram(string program)
        {
            this.program = program;
        }

        public void Execute(Stream input, Stream output)
        {
            var programCounter = 0;
            var pointer = 0;
            var lint = new byte[1 << 15];
            var buffer = new byte[1];

            while (programCounter < program.Length)
            {
                var next = program[programCounter];
                switch (next)
                {
                    case '>':
                        pointer++;
                        break;

                    case '<':
                        pointer--;
                        break;

                    case '+':
                        lint[pointer]++;
                        break;

                    case '-':
                        lint[pointer]--;
                        break;

                    case '.':
                        buffer[0] = lint[pointer];
                        output.Write(buffer, 0, 1);
                        break;

                    case ',':
                        input.Read(buffer);
                        lint[pointer] = buffer[0];
                        break;

                    case '[':
                        if (lint[pointer] == 0)
                        {
                            var stackSize = 1;
                            var i = programCounter + 1;
                            for (; i < program.Length && stackSize > 0; i++)
                            {
                                if (program[i] == '[')
                                {
                                    stackSize++;
                                }

                                if (program[i] == ']')
                                {
                                    stackSize--;
                                }
                            }

                            if (stackSize == 0)
                            {
                                programCounter = i;
                            }
                        }
                        break;

                    case ']':
                        if (lint[pointer] != 0)
                        {
                            var stackSize = 1;
                            var i = programCounter - 1;
                            for (; i >= 0 && stackSize > 0; i--)
                            {
                                if (program[i] == ']')
                                {
                                    stackSize++;
                                }

                                if (program[i] == '[')
                                {
                                    stackSize--;
                                }
                            }

                            if (stackSize == 0)
                            {
                                programCounter = i;
                            }
                        }
                        break;

                    default:
                        continue;
                }

                programCounter++;
            }
        }
    }
}
