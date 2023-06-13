using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.Common.Models.User;
using ScoreTracker.Interfaces;

namespace ScoreTracker.ConsoleRunner.Benchmarking;

[SimpleJob(iterationCount: 100, warmupCount: 2)]
[MemoryDiagnoser]
public class SearchBenchmark
{

    private IUserService _userService;

    [GlobalSetup]
    public void Setup()
    {
        var serviceProvider = DependencyInjectionProvider.GetServiceProvider();
        _userService = serviceProvider.GetRequiredService<IUserService>();
    }


    [Benchmark]
    [Arguments("7iSDSd6liDqGtqwTRqG4f1qaM03XeLIR@clients")]
    [Arguments("8iSDSd6liDqGtqwTRqG4f1qaM03XeLIR@clients")]
    public async Task<User> GetuUserDataBytAuth(string authId) => await _userService.GetUserData(authId);
}
