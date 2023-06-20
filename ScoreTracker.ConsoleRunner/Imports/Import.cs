using ScoreTracker.ConsoleRunner.Common.Communication;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Imports.Interfaces;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;
using System.Text;

namespace ScoreTracker.ConsoleRunner.Imports;

public class Import : ICommand
{
    private readonly IEnumerable<IImporter> _importers;
    private readonly ICommunicationHub _communicationHub;

    public Import(IEnumerable<IImporter> importers, 
        ICommunicationHub communicationHub)
    {
        _importers = importers;
        _communicationHub = communicationHub;
    }

    public async Task Execute(CommandBody commandBody)
    {
        if (IsHelp(commandBody)) { Help(); return; }

        var importer = _importers.Single(x => x.CanImport(commandBody));

        await importer.Import(commandBody);
    }

    private void Help()
    {
        var sb = new StringBuilder();

        sb.AppendLine("import <entityType> <number-of-entities>");
        sb.AppendLine();
        sb.AppendLine("<entityType> could be : user | team | game");

        _communicationHub.PublichMessage(sb.ToString()); 
    }

    private bool IsHelp(CommandBody commandBody)
        => commandBody.PositionalArguments.Any() &&
            commandBody.Arguments.First().ToLower() == nameof(Help).ToLower();
}