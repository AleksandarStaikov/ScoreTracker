using ScoreTracker.ConsoleRunner.Common;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;

namespace ScoreTracker.ConsoleRunner.Runner;

public class Runner : IRunner
{
    private readonly ICommandFactory _commandFactory;
    private readonly ICommunicationHub _communicationHub;
    private readonly IConsoleWritter _consoleWritter;

    public Runner(ICommandFactory commandFactory,
        ICommunicationHub communicationHub,
        IConsoleWritter consoleWritter)
    {
        _commandFactory = commandFactory;
        _communicationHub = communicationHub;
        _consoleWritter = consoleWritter;
    }

    public IRunner SetupRunner()
    {
        // TODO : This does not seem appropriate
        _communicationHub.Subscribe(_consoleWritter.Write);

        return this;
    }

    public void Run(string[] commandSegments)
    {
        while (commandSegments.Any())
        {
            ProcessCommand(commandSegments);

            commandSegments = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    private void ProcessCommand(string[] commandSegments)
    {
        var commandBody = new CommandBody(commandSegments);

        _commandFactory
                .ResolveCommand(commandBody)?
                .Execute(commandBody);
    }
}