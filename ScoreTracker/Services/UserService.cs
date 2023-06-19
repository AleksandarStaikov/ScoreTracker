using Microsoft.Extensions.Logging;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Common.Models.User;
using ScoreTracker.Data.Interfaces;
using ScoreTracker.Interfaces;
using ZstdSharp.Unsafe;

namespace ScoreTracker.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserData(string authId)
        => await _userRepository.GetUserData(authId);

    public Task<bool> IsUsernameAvailable(string username)
        => _userRepository.IsUsernameAvailable(username);

    public async Task CreateUser(CreateUserDto newUser)
    {
        if (!await IsUsernameAvailable(newUser.Username))
        {
            _logger.LogWarning("Trying to set a taken username");
            throw new InvalidOperationException("Trying to set a taken username");
        }

        await _userRepository.CreateUser(new User
        {
            Username = newUser.Username,
            AuthId = newUser.AuthId
        });
    }

    public Task<IEnumerable<TeamOverview>> GetUserTeams(string authId, int page, int pagesize)
        => _userRepository.GetUserTeams(authId, page, pagesize);

    public Task<IEnumerable<UserOverview>> GetUserFriends(string authId, int page, int pagesize)
        => _userRepository.GetUserFriends(authId, page, pagesize);

    public async Task AddFriend(string authId, string username)
    {
        var currentUser = await _userRepository.GetUserOverviewByAuthId(authId);
        var otherUser = await _userRepository.GetUserOverviewByUsername(username);

        if (currentUser is null || otherUser is null) 
        {
            throw new NullReferenceException("User does not exist");
        }

        await _userRepository.AddFriend(currentUser, otherUser);
        await _userRepository.AddFriend(otherUser, currentUser);
        // TODO : Here it would be nice to send a notification to the second user there is a friend request
        // Also makes me think is friendships should me mirrored (if I have you as a friend, you will have me as well)
    }
}