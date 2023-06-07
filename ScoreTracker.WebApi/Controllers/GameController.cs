using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Interfaces;
using ScoreTracker.WebApi.Models.Game;

namespace ScoreTracker.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;
    private readonly ILogger<GameController> _logger;

    public GameController(IGameService gameService, IMapper mapper, ILogger<GameController> logger)
    {
        _gameService = gameService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(CreateGameModel model)
    {
        try
        {
            var createGameDto = _mapper.Map<CreateGameDto>(model);
            createGameDto.CurrentUserAuthId = User.Identity.Name;

            var gameId = await _gameService.CreateGame(createGameDto);

            return Ok(gameId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while creating a game");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetGameByGameId(string gameId)
    {
        try
        {
            var game = await _gameService.GetGameByGameId(gameId, User.Identity.Name);

            return Ok(game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error accured while fetching the game");
            return StatusCode(500);
        }
    }
}
