using ScoreTracker.ConsoleRunner.Common.Communication;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;

namespace ScoreTracker.ConsoleRunner.Exit;

public class Exit : ICommand
{
    private readonly ICommunicationHub _communicationHub;

    public Exit(ICommunicationHub communicationHub)
    {
        _communicationHub = communicationHub;
    }

    public Task Execute(CommandBody commandSegments)
    {
        _communicationHub.PublichMessage("Exiting...", MessageSeverity.Success);

        Environment.Exit(0);

        return Task.CompletedTask;
    }
}