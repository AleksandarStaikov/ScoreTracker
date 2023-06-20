using ScoreTracker.Common.Models.Team;
using ScoreTracker.ConsoleRunner.Common.Communication;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Imports.Interfaces;
using ScoreTracker.Interfaces;

namespace ScoreTracker.ConsoleRunner.Imports.Team;

public class TeamImporter : IImporter
{
    private readonly ICommunicationHub _communicationHub;
    private readonly ITeamService _teamService;

    public TeamImporter(ICommunicationHub communicationHub,
        ITeamService teamService)
    {
        _communicationHub = communicationHub;
        _teamService = teamService;
    }

    public bool CanImport(CommandBody commandBody)
        => commandBody.HasPositionalArgument("team", 0);

    public async Task Import(CommandBody commandBody)
    {
        var dataSet = GetDataSet(commandBody);

        await ImportDataSet(dataSet);
    }

    private async Task ImportDataSet(IEnumerable<CreateTeamDto> dataSet)
    {
        foreach (var team in dataSet)
        {
            var createdTeamId = await _teamService.CreateTeam(team);

            _communicationHub.PublichMessage($"Created team with id '{createdTeamId}'", MessageSeverity.Success);
        }
    }

    private IEnumerable<CreateTeamDto> GetDataSet(CommandBody commandBody)
    {
        if (commandBody.PositionalArguments.Count >= 5)
        {
            return GenerateTeamFromArguments(commandBody);
        }

        _communicationHub.PublichMessage("Could not determine how to generate team", MessageSeverity.Error);
        return Enumerable.Empty<CreateTeamDto>();
    }

    private IEnumerable<CreateTeamDto> GenerateTeamFromArguments(CommandBody commandBody)
        => new[] {
            new CreateTeamDto
            {
                CurrentUserAuthId = commandBody.PositionalArguments.ElementAt(1),
                HeartsUsername = commandBody.PositionalArguments.ElementAt(2),
                DiamondsUsername = commandBody.PositionalArguments.ElementAt(3),
                SpadesUsername = commandBody.PositionalArguments.ElementAt(4),

            },
        };
}