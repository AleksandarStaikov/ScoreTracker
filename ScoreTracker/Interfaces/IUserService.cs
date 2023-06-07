using ScoreTracker.Common.Models.Team;
using ScoreTracker.Common.Models.User;

namespace ScoreTracker.Interfaces;

/// <summary>
/// Provides functionality to manage users
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    Task CreateUser(CreateUserDto newUser);
    
    /// <summary>
    /// Gets the user data of a given user, also fetches the first 10 teams and firends
    /// </summary>
    Task<User?> GetUserData(string authId);

    /// <summary>
    /// Retrieves the paginated teams of a user
    /// </summary>
    Task<IEnumerable<TeamOverview>> GetUserTeams(string authId, int page, int pagesize);

    /// <summary>
    /// Returns the paginated friends of a user
    /// </summary>
    Task<IEnumerable<UserOverview>> GetUserFriends(string authId, int page, int pagesize);

    /// <summary>
    /// Checks if a given username is available
    /// </summary>
    Task<bool> IsUsernameAvailable(string username);

    /// <summary>
    /// Add a friend to the user
    /// </summary>
    Task AddFriend(string authId, string username);
}