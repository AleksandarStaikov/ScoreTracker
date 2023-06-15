using ScoreTracker.ConsoleRunner.Common;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;
using System.Text;

namespace ScoreTracker.ConsoleRunner.Imports;

public class Import : ICommand
{
    private readonly ICommunicationHub _communicationHub;

    public Import(ICommunicationHub communicationHub)
    {
        _communicationHub = communicationHub;
    }

    public void Execute(CommandBody commandBody)
    {
        if (IsHelp(commandBody)) { Help(); }
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