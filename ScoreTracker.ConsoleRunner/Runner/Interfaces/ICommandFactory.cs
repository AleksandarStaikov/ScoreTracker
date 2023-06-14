namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommandFactory
{
    ICommand? ResolveCommand(string commandName);
}