using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ScoreTracker.Common.Extensions;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Common.Models.User;
using ScoreTracker.Data.Entities.Team;
using ScoreTracker.Data.Entities.User;
using ScoreTracker.Data.Interfaces;

namespace ScoreTracker.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _users;
        private readonly IMapper _mapper;

        /// <summary>
        /// Projection used to get only the username and auth id, everythin else will not be loaded in memory
        /// </summary>
        private static ProjectionDefinition<UserEntity> OverviewProjection
            => new ProjectionDefinitionBuilder<UserEntity>()
                .Include(u => u.Username)
                .Include(u => u.AuthId);

        public UserRepository(IMongoDatabase db, IMapper mapper)
        {
            _users = db.GetCollection<UserEntity>(nameof(UserEntity));
            _mapper = mapper;
        }

        public async Task<bool> IsUsernameAvailable(string username)
        {
            var result = await _users.FindAsync(x => x.Username == username);
            return !result.Any();
        }

        public Task<User?> GetUserData(string authId)
        {
            var projection = new ProjectionDefinitionBuilder<UserEntity>()
                .Include(u => u.Username)
                .Include(u => u.AuthId)
                .Include(u => u.Friends)
                .Include(u => u.Teams)
                .Slice(u => u.Friends, 0, 10)
                .Slice(u => u.Teams, 0, 10);

            return GetUserByAuthIdWithProjection(authId, projection);
        }

        public async Task CreateUser(User user)
        {
            if (await GetUserData(user.AuthId) is not null)
            {
                throw new InvalidOperationException($"User for the current {nameof(user.AuthId)} alredy exists");
            }

            var userEntity = _mapper.Map<UserEntity>(user);

            await _users.InsertOneAsync(userEntity);
        }

        public async Task<IEnumerable<TeamOverview>> GetUserTeams(string authId, int page, int pagesize)
        {
            // TODO : Order teams by most games played
            var projection = new ProjectionDefinitionBuilder<UserEntity>()
                .Include(u => u.Teams)
                .Slice(x => x.Teams, (page - 1) * pagesize, pagesize);

            var user = await GetUserByAuthIdWithProjection(authId, projection);

            return user?.Teams ?? Enumerable.Empty<TeamOverview>();
        }

        public async Task<IEnumerable<UserOverview>> GetUserFriends(string authId, int page, int pagesize)
        {
            var projection = new ProjectionDefinitionBuilder<UserEntity>()
                .Include(u => u.Friends)
                .Slice(x => x.Friends, (page - 1) * pagesize, pagesize);

            var user = await GetUserByAuthIdWithProjection(authId, projection);

            return user?.Friends ?? Enumerable.Empty<UserOverview>();
        }

        public async Task<UserOverview> GetUserOverviewByUsername(string username)
        {
            var userEntity = await _users
                .Find(u => u.Username == username)
                .Project<UserEntity>(OverviewProjection)
                .FirstOrDefaultAsync();

            var user = _mapper.Map<UserOverview>(userEntity);

            return user;
        }

        public async Task<UserOverview> GetUserOverviewByAuthId(string authId)
        {
            var userEntity = await _users
                .Find(u => u.AuthId == authId)
                .Project<UserEntity>(OverviewProjection)
                .FirstOrDefaultAsync();

            var user = _mapper.Map<UserOverview>(userEntity);

            return user;
        }

        public Task AddFriend(UserOverview currentUser, UserOverview friend)
        {
            var userOverviewEntity = _mapper.Map<UserOverviewEntity>(friend);

            return _users.FindOneAndUpdateAsync(
                x => x.AuthId == currentUser.AuthId,
                new UpdateDefinitionBuilder<UserEntity>()
                    .AddToSet(e => e.Friends, userOverviewEntity));
        }

        private async Task<User?> GetUserByAuthIdWithProjection(string authId, ProjectionDefinition<UserEntity> projection)
        {
            var userEntity = await _users
                .Find(x => x.AuthId == authId)
                .Project<UserEntity>(projection)
                .FirstOrDefaultAsync();

            return _mapper.Map<User>(userEntity);
        }

        public Task AddTeamToUsers(TeamOverview teamOverview, string[] usernames)
        {
            var teamOverviewEntity = _mapper.Map<TeamOverviewEntity>(teamOverview);

            return _users
                .UpdateManyAsync(
                    u => usernames.Contains(u.Username),
                    new UpdateDefinitionBuilder<UserEntity>()
                        .AddToSet(p => p.Teams, teamOverviewEntity));
        }

        public Task UpdateUsersTeamWinStats(Team team, GameOverview gameOverview)
        {
            //A bit of a red flag need to digure out a way to get rid of the magic strings 
            var usersToUpdate = team.GetUsernames();
            var nameOfFieldToUpdate = gameOverview.BlackTeamWins() ? "BlackWins" : "RedWins";

            var filter = Builders<UserEntity>.Filter.In(x => x.Username, usersToUpdate);
            var update = Builders<UserEntity>.Update.Inc($"Teams.$[f].{nameOfFieldToUpdate}", 1u);

            var arrayFilters = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(
                    new BsonDocument("f.TeamId",
                            new BsonDocument("$eq", team.TeamId))),
            };

            return _users.UpdateManyAsync(filter, update, new UpdateOptions { ArrayFilters = arrayFilters });
        }

        public Task<bool> UserHasTeam(string authId, string teamId)
        {
            var filter = Builders<UserEntity>.Filter.And(
                Builders<UserEntity>.Filter.Eq(x => x.AuthId, authId),
                Builders<UserEntity>.Filter.ElemMatch(x => x.Teams, t => t.TeamId == teamId));

            return _users.Find(filter).AnyAsync();
        }
    }
}