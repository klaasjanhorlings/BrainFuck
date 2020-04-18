# BrainFuck

[BrainFuck](https://en.wikipedia.org/wiki/Brainfuck) is a very simple turing complete language supporting 8 characters, namely +-<>.,[]

This project is for exploring the many ways to implement a interpreter or compiler for a simple language like BrainFuck.

I challenge you to generate the fastest method of executing BrainFuck code.

## Creating your own compiler
Add a new Class Library project to the prefixed with _BrainFuck.Implementations.{Your name}._ You need to implement the __ICompiler__ interface in the _BrainFuck.Core_ project. To change the displayname in the benchmarks override __ToString()__.

## Testing
Create a new test class in the _BrainFuck.Tests_ project with the __[TestClass]__ attribute extending the __TestBase__ class. You only have to implement the __GetCompiler()__ method, the tests are inherited automatically.

## Benchmarking
Inside the _BrainFuck.Benchmarks_ project add an instance of your compiler/interpreter to the _Compilers_ enumerable inside the __BrainFuckBenchmarkBase__ class. 

## Ideas for optimization
- Replace consecutive +, -, < and > with a single add or subract
- Replace consecutive ] with single jump (tail call optimization)
- Interpreter only - cache block start and endings