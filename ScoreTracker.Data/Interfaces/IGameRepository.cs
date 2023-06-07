using ScoreTracker.Common.Models.Game;

namespace ScoreTracker.Data.Interfaces;

public interface IGameRepository
{
    Task<Game> CreateGame(Game game);
    Task<Game?> GetGameByGameId(string gameId);
}