using ScoreTracker.ConsoleRunner.Runner.Interfaces;

namespace ScoreTracker.ConsoleRunner.Runner;

public class Runner : IRunner
{
    private readonly ICommandFactory _commandFactory;

    public Runner(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
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
        var commandName = commandSegments[0].ToLower();
        var commandParams = commandSegments[1..];

        _commandFactory
                .ResolveCommand(commandName)?
                .Execute(commandParams);
    }
}