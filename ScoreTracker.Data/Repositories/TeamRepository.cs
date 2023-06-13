using AutoMapper;
using MongoDB.Driver;
using ScoreTracker.Common.Extensions;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Data.Entities.Game;
using ScoreTracker.Data.Entities.Team;
using ScoreTracker.Data.Entities.User;
using ScoreTracker.Data.Interfaces;

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

    public async Task<Team?> GetTeamByTeamId(string teamId)
    {
        // TODO : Projection to be added
        //var projection = new ProjectionDefinitionBuilder<TeamEntity>()
        //        .Include(t => t.RedWins)
        //        .Include(t => t.BlackWins)
        //        .Include(t => t.ClubsUsername)
        //        .Include(t => t.HeartsUsername)
        //        .Include(t => t.DiamodsUsername)
        //        .Include(t => t.SpadesUsername)
        //        .Include(t => t.Games)
        //        .Slice(u => u.Games, 0, 10);

        var teamEntity = await _teams
            .Find(x => x.TeamId == teamId)
            .FirstOrDefaultAsync();

        return _mapper.Map<Team?>(teamEntity);
    }

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
}