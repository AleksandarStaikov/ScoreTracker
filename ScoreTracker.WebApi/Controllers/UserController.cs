using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreTracker.Common.Models.User;
using ScoreTracker.Interfaces;

namespace ScoreTracker.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> UsernameAvailable(string username)
    {
        try
        {
            var isAvailable = _userService.IsUsernameAvailable(username);

            return Ok(await isAvailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while checking for username availability");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserData()
    {
        try
        {
            var user = await _userService.GetUserData(User.Identity.Name);

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while fethcing the username");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserFriends(int page = 1, int pagesize = 10)
    {
        var friends = await _userService.GetUserFriends(User.Identity.Name, page, pagesize);

        return Ok(friends);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTeams(int page = 1, int pagesize = 10)
    {
        var teams = await _userService.GetUserTeams(User.Identity.Name, page, pagesize);

        return Ok(teams);
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] string username)
    {
        try
        {
            await _userService.CreateUser(new CreateUserDto
            {
                AuthId = User.Identity.Name,
                Username = username
            });

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured when setting the username");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddFriend([FromBody] string username)
    {
        try
        {
            await _userService.AddFriend(User.Identity.Name, username);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while adding a friend");
            return BadRequest();
        }
    }
}
