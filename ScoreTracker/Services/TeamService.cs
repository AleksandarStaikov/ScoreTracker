using AutoMapper;
using ScoreTracker.Common.Extensions;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Data.Interfaces;
using ScoreTracker.Interfaces;

namespace ScoreTracker.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TeamService(ITeamRepository teamRepository, 
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _teamRepository = teamRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<string> CreateTeam(CreateTeamDto createTeamDto)
    {
        var currentUser = await _userRepository
            .GetUserOverviewByAuthId(createTeamDto.CurrentUserAuthId);

        var teamToCreate = new Team()
        {
            ClubsUsername = currentUser.Username,
            DiamodsUsername = createTeamDto.DiamondsUsername,
            HeartsUsername = createTeamDto.HeartsUsername,
            SpadesUsername = createTeamDto.SpadesUsername,
        };

        var matchingTeam = await _teamRepository.GetMatchingTeam(teamToCreate);
        if (matchingTeam is not null)
        {
            return matchingTeam.TeamId;
        }

        var createdTeam = await _teamRepository.CreateTeam(teamToCreate);

        var teamOverview = _mapper.Map<TeamOverview>(createdTeam);

        // TODO : Send a notification to the rest of the users about the new team
        await _userRepository
            .AddTeamToUsers(
                teamOverview,
                createdTeam.GetUsernames());

        return createdTeam.TeamId;
    }

    public async Task<Team?> GetTeamByTeamId(string teamId, string userAuthId)
    {
        var currentUser = await _userRepository.GetUserOverviewByAuthId(userAuthId);

        var team = await _teamRepository.GetTeamByTeamId(teamId);

        if (team is null)
        {
            return null;
        }

        if (!team.ContainsUser(currentUser.Username))
        {
            throw new InvalidOperationException("The current user is not part of the team that is requested");
        }

        return team;
    }
}
