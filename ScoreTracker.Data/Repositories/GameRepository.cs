using AutoMapper;
using MongoDB.Driver;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Data.Entities.Game;
using ScoreTracker.Data.Interfaces;

namespace ScoreTracker.Data.Repositories;

public class GameRepository : IGameRepository
{
    private readonly IMongoCollection<GameEntity> _games;
    private readonly IMapper _mapper;

    public GameRepository(IMongoDatabase db, IMapper mapper)
    {
        _games = db.GetCollection<GameEntity>(nameof(GameEntity));
        _mapper = mapper;
    }

    public async Task<Game> CreateGame(Game game)
    {
        var gameEntity = _mapper.Map<GameEntity>(game);

        await _games.InsertOneAsync(gameEntity);

        game.GameId = gameEntity.GameId;

        return game;
    }

    public async Task<Game?> GetGameByGameId(string gameId)
    {
        var gameEntity = await _games
            .Find(x => x.GameId == gameId)
            .FirstOrDefaultAsync();

        return _mapper.Map<Game?>(gameEntity);
    }
}