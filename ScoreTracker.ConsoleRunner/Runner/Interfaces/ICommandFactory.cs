using ScoreTracker.ConsoleRunner.Common;

namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommandFactory
{
    ICommand? ResolveCommand(CommandBody commandBody);
}