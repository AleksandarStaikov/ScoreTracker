using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Interfaces;
using ScoreTracker.WebApi.Models.Team;

namespace ScoreTracker.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamController> _logger;

    public TeamController(ITeamService teamService, IMapper mapper, ILogger<TeamController> logger)
    {
        _teamService = teamService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamModel model)
    {
        try
        {
            var createTeamDto = _mapper.Map<CreateTeamDto>(model);

            createTeamDto.CurrentUserAuthId = User.Identity.Name;

            var teamId = await _teamService.CreateTeam(createTeamDto);

            return Ok(teamId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating a team");
            return StatusCode(500);
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetTeamByTeamId(string teamId)
    {
        try
        {
            var team = await _teamService.GetTeamByTeamId(teamId, User.Identity.Name);

            return Ok(team);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while trying to get a team by id");
            return StatusCode(500);
        }
    }
}
