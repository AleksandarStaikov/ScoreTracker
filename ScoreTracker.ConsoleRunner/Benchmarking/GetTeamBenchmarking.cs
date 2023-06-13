using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Interfaces;

namespace ScoreTracker.ConsoleRunner.Benchmarking;

[SimpleJob(iterationCount: 10, warmupCount: 2)]
[MemoryDiagnoser]
public class GetTeamBenchmarking
{

    private ITeamService _teamService;

    [GlobalSetup]
    public void Setup()
    {
        var serviceProvider = DependencyInjectionProvider.GetServiceProvider();
        _teamService = serviceProvider.GetRequiredService<ITeamService>();
    }

    [Benchmark]
    [Arguments("647b345e162514c8101d953b", "7iSDSd6liDqGtqwTRqG4f1qaM03XeLIR@clients")]
    [Arguments("647b3430162514c8101d953a", "7iSDSd6liDqGtqwTRqG4f1qaM03XeLIR@clients")]
    public async Task<Team> GetTeam(string teamId, string userAuthId) => await _teamService.GetTeamByTeamId(teamId, userAuthId);
}
