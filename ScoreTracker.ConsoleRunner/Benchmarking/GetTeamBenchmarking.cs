using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Data.Interfaces;

namespace ScoreTracker.ConsoleRunner.Benchmarking;

[SimpleJob(iterationCount: 25, warmupCount: 2)]
[MemoryDiagnoser]
public class GetTeamBenchmarking
{
    private ITeamRepository _teamRepository;

    [GlobalSetup]
    public void Setup()
    {
        var serviceProvider = DependencyInjectionProvider.GetServiceProvider();
        _teamRepository = serviceProvider.GetRequiredService<ITeamRepository>();
    }

    [Benchmark]
    [Arguments("647b345e162514c8101d953b", 10)]
    [Arguments("647b345e162514c8101d953b", 50)]
    [Arguments("647b345e162514c8101d953b", 100)]
    [Arguments("647b345e162514c8101d953b", 500)]
    [Arguments("647b345e162514c8101d953b", 1140)]
    [Arguments("647b3430162514c8101d953a", 10)]
    [Arguments("647b3430162514c8101d953a", 50)]
    public async Task<Team>? GetTeamWithPredicateAndProjection(string teamId, int projectionSize)
        => await _teamRepository.GetTeamWithPredicateAndProjection(teamId, projectionSize);

    [Benchmark]
    [Arguments("647b345e162514c8101d953b")]
    [Arguments("647b345e162514c8101d953a")]
    public async Task<Team>? GetTeamWithPredicate(string teamId)
        => await _teamRepository.GetTeamWithPredicate(teamId);

    [Benchmark]
    [Arguments("647b345e162514c8101d953b", 10)]
    [Arguments("647b345e162514c8101d953b", 50)]
    [Arguments("647b345e162514c8101d953b", 100)]
    [Arguments("647b345e162514c8101d953b", 500)]
    [Arguments("647b345e162514c8101d953b", 1140)]
    [Arguments("647b3430162514c8101d953a", 10)]
    [Arguments("647b3430162514c8101d953a", 50)]
    public async Task<Team>? GetTeamWithFilterAndProjection(string teamId, int projectionSize)
        => await _teamRepository.GetTeamWithPredicateAndProjection(teamId, projectionSize);

    [Benchmark]
    [Arguments("647b345e162514c8101d953b")]
    [Arguments("647b345e162514c8101d953a")]
    public async Task<Team>? GetTeamWithFilter(string teamId)
        => await _teamRepository.GetTeamWithFilter(teamId);
}
