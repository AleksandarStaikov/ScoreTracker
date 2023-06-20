using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.ConsoleRunner;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;

var initialCommand = new[] { "help" }; //args;

await DependencyInjectionProvider
        .GetServiceProvider()
        .GetRequiredService<IRunner>()
        .SetupRunner()
        .Run(initialCommand);

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

//var summary = BenchmarkRunner.Run<GetTeamBenchmarking>();

Console.ReadKey();