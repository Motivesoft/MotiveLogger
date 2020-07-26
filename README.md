# MotiveLogger
MotiveLogger is a simple C# logging component as '[shared project](https://docs.microsoft.com/en-us/xamarin/cross-platform/app-fundamentals/shared-projects)' to add into your Visual Studio solution.

## Design goals
1. The primary purpose of the logger is to be simple to deploy.
1. The logger aims to provide a set of common logging requirements in a non-complicated manner, rather than trying to be an answer to every situation.
1. The logger is not an attempt to replicate any other logging library, although it will undoubtedly look similar to some.
1. The logger will target at least console and file logging outputs.
1. The user is allowed to copy this code and deploy it however they want, but this repo is intended to remain as-is to satisfy its primary design goal
1. The repo shall be made available as a [solution](https://docs.microsoft.com/en-us/visualstudio/ide/solutions-and-projects-in-visual-studio) along with a console executable project to demonstrate some of the uses of the MotiveLogger.

## Implementation details
1. The code shall be written in C#
1. This repo provides the code as a 'shared project'. The use of a 'shared project' means that the code is [included directly](https://docs.microsoft.com/en-us/visualstudio/ide/managing-references-in-a-project) into any project that uses it, and therefore doesn't need to be separately managed and deployed as a DLL or assembly.
1. The repo is delivered as a solution with a console executable project to demonstrate some of the uses of the MotiveLogger.
1. The general idea is to clone the repo, examin the TestLoggerCL test harness project for implementation details and then start to implement your own projects directly into the same solution - or you can simply copy the MotiveLogger shared project into a pre-existing solution.

## Loggers
The main point of interaction is the `Logger` object. Code that wants to perform logging will obtain a `Logger` from the `LogManager` and use its methods to output log statements.

### Output types
Concrete implementations of `Logger` are available for all of the possible output types (console, text file, etc.) and more can be provided. 

An `DefaultLogger` base class can be used as the basis to make new Output types, for example to write to an XML file.

# Status
The library is not in a state I would describe as ready for use yet as I'd like to tackle the following, but it is usable in its current form:
1. Configuration through the use of a config file and through the API
1. Rename `DefaultLogger` to `AbstractLogger`
1. Make `ConsoleLogger` output more fields
1. Complete the `FileLogger`
1. Make a choice, and implement/document whether the callstack is dumped only if the last `arg` is an `Exception`, or if any one is (probably just the last one)
1. Make `TestLoggerCL` a bit more useful and documented
1. Decide whether to continue to provide the code as a full solution, or whether to make it just deliver the shared project and provide the rest only as part of a release - base this decision on which is easiest to deploy into a new solution.