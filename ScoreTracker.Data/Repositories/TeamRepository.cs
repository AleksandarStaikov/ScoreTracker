using AutoMapper;
using MongoDB.Driver;
using ScoreTracker.Common.Extensions;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Data.Entities.Game;
using ScoreTracker.Data.Entities.Team;
using ScoreTracker.Data.Interfaces;
using System.Linq.Expressions;

namespace ScoreTracker.Data.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly IMongoCollection<TeamEntity> _teams;
    private readonly IMapper _mapper;

    public TeamRepository(IMongoDatabase mongoDatabase, IMapper mapper)
    {
        _teams = mongoDatabase.GetCollection<TeamEntity>(nameof(TeamEntity));
        _mapper = mapper;
    }

    public async Task<Team?> GetMatchingTeam(Team team)
    {
        var subteamOne = new StringOrRegularExpression[] { team.ClubsUsername, team.SpadesUsername };
        var subteamTwo = new StringOrRegularExpression[] { team.HeartsUsername, team.DiamodsUsername };

        var blackIsInTeamOne = new FilterDefinitionBuilder<TeamEntity>()
            .And(new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.SpadesUsername, subteamOne),
                 new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.ClubsUsername, subteamOne));

        var redIsInTeamTwo = new FilterDefinitionBuilder<TeamEntity>()
            .And(new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.HeartsUsername, subteamTwo),
                new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.DiamodsUsername, subteamTwo));


        var redIsInTeamOne = new FilterDefinitionBuilder<TeamEntity>()
            .And(new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.HeartsUsername, subteamOne),
                new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.DiamodsUsername, subteamOne));

        var blackIsInteamTwo = new FilterDefinitionBuilder<TeamEntity>()
            .And(new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.SpadesUsername, subteamTwo),
                 new FilterDefinitionBuilder<TeamEntity>().StringIn(x => x.ClubsUsername, subteamTwo));


        var predicate = new FilterDefinitionBuilder<TeamEntity>()
            .Or(new FilterDefinitionBuilder<TeamEntity>().And(blackIsInTeamOne, redIsInTeamTwo),
                new FilterDefinitionBuilder<TeamEntity>().And(blackIsInteamTwo, redIsInTeamOne));

        var matchingTeamEntity = await _teams.Find(predicate).FirstOrDefaultAsync();

        var matchingTeam = _mapper.Map<Team?>(matchingTeamEntity);

        return matchingTeam;
    }

    public async Task<Team> CreateTeam(Team team)
    {
        var teamEntity = _mapper.Map<TeamEntity>(team);

        await _teams.InsertOneAsync(teamEntity);

        team.TeamId = teamEntity.TeamId;

        return team;
    }

    public async Task<Team?> GetTeamByTeamId(string teamId) => await GetTeam(x => x.TeamId == teamId);

    public Task AddGameToTeam(string teamId, GameOverview gameOverview)
    {
        var gameOverviewEntity = _mapper.Map<GameOverviewEntity>(gameOverview);

        var blackTeamWins = gameOverview.BlackTeamWins();

        var propertyToUpdate = gameOverview.BlackTeamWins() ? "BlackWins" : "RedWins";

        return _teams.UpdateOneAsync(x =>
                    x.TeamId == teamId,
                    new UpdateDefinitionBuilder<TeamEntity>()
                        .Push(p => p.Games, gameOverviewEntity)
                        .Inc(propertyToUpdate, 1u));
    }


    #region PerformanceTesting

    public async Task<Team>? GetTeamWithPredicateAndProjection(string teamId, int projectionSize)
        => await GetTeam(x => x.TeamId == teamId, TeamProjection(projectionSize));

    public async Task<Team>? GetTeamWithPredicate(string teamId)
        => await GetTeam(x => x.TeamId == teamId);

    public async Task<Team>? GetTeamWithFilterAndProjection(string teamId, int projectionSize)
        => await GetTeam(new FilterDefinitionBuilder<TeamEntity>().Eq(x => x.TeamId, teamId), TeamProjection(projectionSize));

    public async Task<Team>? GetTeamWithFilter(string teamId)
        => await GetTeam(new FilterDefinitionBuilder<TeamEntity>().Eq(x => x.TeamId, teamId));

    #endregion

    private async Task<Team?> GetTeam(Expression<Func<TeamEntity, bool>> predicate, ProjectionDefinition<TeamEntity>? projection = null)
    {
        var query = _teams.Find(predicate);

        if (projection != null)
        {
            query = query.Project<TeamEntity>(projection);
        }

        var teamEntity = await query.FirstOrDefaultAsync();

        return _mapper.Map<Team?>(teamEntity);
    }

    private async Task<Team>? GetTeam(FilterDefinition<TeamEntity> filter, ProjectionDefinition<TeamEntity>? projection = null)
    {
        var query = _teams.Find(filter);

        if (projection != null)
        {
            query = query.Project<TeamEntity>(projection);
        }

        var teamEntity = await query.FirstOrDefaultAsync();

        return _mapper.Map<Team?>(teamEntity);
    }


    public ProjectionDefinition<TeamEntity> TeamProjection(int size) =>
        new ProjectionDefinitionBuilder<TeamEntity>()
            .Include(t => t.RedWins)
            .Include(t => t.BlackWins)
            .Include(t => t.ClubsUsername)
            .Include(t => t.HeartsUsername)
            .Include(t => t.DiamodsUsername)
            .Include(t => t.SpadesUsername)
            .Include(t => t.Games)
            .Slice(u => u.Games, 0, size);

}