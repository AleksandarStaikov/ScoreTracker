using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.ConsoleRunner;
using ScoreTracker.ConsoleRunner.Benchmarking;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;

var initialCommand = new[] { "help" }; //args;

DependencyInjectionProvider
    .GetServiceProvider()
    .GetRequiredService<IRunner>()
    .Run(initialCommand);

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

//var summary = BenchmarkRunner.Run<GetTeamBenchmarking>();

Console.ReadKey();
