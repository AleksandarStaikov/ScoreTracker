using BenchmarkDotNet.Running;
using ScoreTracker.ConsoleRunner;
using ScoreTracker.ConsoleRunner.Benchmarking;


var serviceProvider = DependencyInjectionProvider.GetServiceProvider();

//await serviceProvider
//     .GetRequiredService<IDataSeeder>()
//     .AddGames(10, "647b345e162514c8101d953b", "7iSDSd6liDqGtqwTRqG4f1qaM03XeLIR@clients");

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

var summary = BenchmarkRunner.Run<GetTeamBenchmarking>();

Console.ReadKey();
