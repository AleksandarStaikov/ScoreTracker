using ScoreTracker.Common.Models.Game;

namespace ScoreTracker.Interfaces;

public interface IGameService
{
    Task<string> CreateGame(CreateGameDto model);
    Task<Game?> GetGameByGameId(string gameId, string userAuthId);
}