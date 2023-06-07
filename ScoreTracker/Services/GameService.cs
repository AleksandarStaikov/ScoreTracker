using AutoMapper;
using ScoreTracker.Common.Extensions;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Data.Interfaces;
using ScoreTracker.Interfaces;

namespace ScoreTracker.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GameService(IGameRepository gameRepository,
        ITeamRepository teamRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _gameRepository = gameRepository;
        _teamRepository = teamRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }


    public async Task<Game?> GetGameByGameId(string gameId, string userAuthId)
    {
        var game = await _gameRepository.GetGameByGameId(gameId);

        if (game == null) { return null; }

        if (!await _userRepository.UserHasTeam(userAuthId, game.TeamId))
        {
            throw new InvalidOperationException("User can not fetch games that they have not played in");
        }   

        return game;
    }

    public async Task<string> CreateGame(CreateGameDto model)
    {
        var team = await _teamRepository.GetTeamByTeamId(model.TeamId);
        var currentUser = await _userRepository.GetUserOverviewByAuthId(model.CurrentUserAuthId);

        EnsureTeamIsValid(team, currentUser.Username);

        var gameToCreate = new Game()
        {
            TeamId = model.TeamId,
            RedPoints = model.RedPoints,
            BlackPoints = model.BlackPoints,
        };
        gameToCreate.ComputeTotals();

        EnsureGameHasAWinner(gameToCreate);

        var createdGame = await _gameRepository.CreateGame(gameToCreate);

        await UpdateRelatedDocumentForGameCreated(createdGame, team);

        return createdGame.GameId;
    }

    public async Task UpdateRelatedDocumentForGameCreated(Game createdGame, Team team)
    {
        var gameOverview = _mapper.Map<GameOverview>(createdGame);

        await _teamRepository.AddGameToTeam(team.TeamId, gameOverview);

        await _userRepository.UpdateUsersTeamWinStats(team, gameOverview);
    }

    public void EnsureTeamIsValid(Team? team, string username)
    {
        if (team is null)
        {
            throw new ArgumentNullException("Team with the specified id does not exist");
        }

        if (!team.ContainsUser(username))
        {
            throw new InvalidOperationException("The current user is not part of the team that is requested");
        }
    }

    public void EnsureGameHasAWinner(Game game)
    {
        if (!game.HasWinner())
        {
            throw new InvalidOperationException("Can not create game without a winner");
        }
    }
}