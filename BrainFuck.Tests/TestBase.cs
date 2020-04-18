using BrainFuck.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace BrainFuck.Tests
{
    public abstract class TestBase
    {
        private MemoryStream InputStream;
        private MemoryStream OutputStream;

        protected abstract ICompiler GetCompiler();

        [TestInitialize]
        public void TestInitialize()
        {
            InputStream = new MemoryStream();
            OutputStream = new MemoryStream();
        }

        [TestMethod]
        public void HelloWorld()
        {
            var program = CreateProgram(Programs.HelloWorld);

            program(InputStream, OutputStream);

            var output = Encoding.ASCII.GetString(OutputStream.ToArray());
            Assert.AreEqual("Hello World!\n", output);
        }

        [TestMethod]
        [DataRow("Brainfuck\0", "Oenvashpx")]
        [DataRow("Awesome\0", "Njrfbzr")]
        [DataRow("hallo wereld\0", "unyyb jreryq")]
        public void Rot13(string input, string expectedOutput)
        {
            var program = CreateProgram(Programs.Rot13);

            InputStream.Write(Encoding.ASCII.GetBytes(input));
            InputStream.Position = 0;

            program(InputStream, OutputStream);

            var output = Encoding.ASCII.GetString(OutputStream.ToArray());
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        [DataRow("Hello World!\0", "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.------------------------------------------------------------------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.-----------------------------------------------------------------------------------------------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.------------------------------------------------------------------------------------------------------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.------------------------------------------------------------------------------------------------------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.---------------------------------------------------------------------------------------------------------------++++++++++++++++++++++++++++++++.--------------------------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.---------------------------------------------------------------------------------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.---------------------------------------------------------------------------------------------------------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.------------------------------------------------------------------------------------------------------------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.------------------------------------------------------------------------------------------------------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.----------------------------------------------------------------------------------------------------+++++++++++++++++++++++++++++++++.---------------------------------")]
        public void StringToBrainFuck(string input, string expectedOutput)
        {
            var program = CreateProgram(Programs.StringToBrainFuck);

            InputStream.Write(Encoding.ASCII.GetBytes(input));
            InputStream.Position = 0;

            program(InputStream, OutputStream);

            var output = Encoding.ASCII.GetString(OutputStream.ToArray());
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        [DataRow("Brainfuck\0", "kcufniarB")]
        [DataRow("Awesome\0", "emosewA")]
        [DataRow("hallo wereld\0", "dlerew ollah")]
        public void Reverse(string input, string expectedOutput)
        {
            var program = CreateProgram(Programs.Reverse);

            InputStream.Write(Encoding.ASCII.GetBytes(input));
            InputStream.Position = 0;

            program(InputStream, OutputStream);

            var output = Encoding.ASCII.GetString(OutputStream.ToArray());
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void Square100000()
        {
            var program = CreateProgram(Programs.Square10000);

            program(InputStream, OutputStream);
            var output = Encoding.ASCII.GetString(OutputStream.ToArray());
        }

        private Program CreateProgram(string code)
        {
            var programStream = new MemoryStream(Encoding.ASCII.GetBytes(code));
            var compiler = GetCompiler();
            var program = compiler.Compile(programStream);
            return program;
        }
    }
}
