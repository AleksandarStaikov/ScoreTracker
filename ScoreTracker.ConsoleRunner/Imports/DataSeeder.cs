using ScoreTracker.Common;
using ScoreTracker.Common.Extensions;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Data.Entities.Game;
using ScoreTracker.Data.Interfaces;
using ScoreTracker.Interfaces;

namespace ScoreTracker.ConsoleRunner.Imports;

public class DataSeeder : IDataSeeder
{
    private readonly IGameService _gameService;

    public DataSeeder(ITeamRepository teamRepository, IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task AddGames(int n, string teamId, string userAuthId)
    {
        for (int i = 0; i < n; i++)
        {
            var game = GenerateRangomGame(teamId, userAuthId);
            await _gameService.CreateGame(game);
        }
    }

    private CreateGameDto GenerateRangomGame(string teamId, string userAuthId)
    {
        var blackTeamPoints = new List<int>();
        var redTeamPoints = new List<int>();

        do
        {
            blackTeamPoints.Add(Random.Shared.Next(30));
            redTeamPoints.Add(Random.Shared.Next(30));
        }
        while (blackTeamPoints.Sum() <= GameConstants.GameWinningPoints &&
            redTeamPoints.Sum() <= GameConstants.GameWinningPoints);

        return new CreateGameDto()
        {
            TeamId = teamId,
            RedPoints = redTeamPoints,
            BlackPoints = blackTeamPoints,
            CurrentUserAuthId = userAuthId
        };
    }
}
