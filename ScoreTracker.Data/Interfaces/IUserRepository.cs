using AWSSDK.Runtime.Internal.Util;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Common.Models.User;

namespace ScoreTracker.Data.Interfaces;

/// <summary>
/// Hosts logic for quering on the users dataset
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets the user that matches the auth id, returns the first 10 friends and teams as well
    /// </summary>
    Task<User?> GetUserData(string authId);

    /// <summary>
    /// Creates a new user and adds it to the dataset
    /// </summary>
    Task CreateUser(User user);

    /// <summary>
    /// Check if a user with the given id exists
    /// </summary>
    Task<bool> IsUsernameAvailable(string username);

    /// <summary>
    /// Returns the paginated teams of an user
    /// </summary>
    Task<IEnumerable<TeamOverview>> GetUserTeams(string authId, int page, int pagesize);

    /// <summary>
    /// Returns the paginated friends of a user
    /// </summary>
    Task<IEnumerable<UserOverview>> GetUserFriends(string authId, int page, int pagesize);

    /// <summary>
    /// Gets the user overview for the user with the given username
    /// </summary>
    Task<UserOverview> GetUserOverviewByUsername(string username);

    /// <summary>
    /// Gets the user overview for the user with the given auth id
    /// </summary>
    Task<UserOverview> GetUserOverviewByAuthId(string authId);

    /// <summary>
    /// Adds the friend to the collection of friends of the first user
    /// </summary>
    Task AddFriend(UserOverview currentUser, UserOverview friend);

    /// <summary>
    /// Adds a team overview to all users in username collection
    /// </summary>
    Task AddTeamToUsers(TeamOverview teamOverview, string[] usernames);

    /// <summary>
    /// Increments the team wins in the team overview of the users in the team
    /// </summary>
    Task UpdateUsersTeamWinStats(Team team, GameOverview gameOverview);

    /// <summary>
    /// Checks if the user has a team with the specified team id
    /// </summary>
    Task<bool> UserHasTeam(string authId, string teamId);
}