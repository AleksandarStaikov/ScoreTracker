using ScoreTracker.ConsoleRunner.Common.Communication;

namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommandFactory
{
    ICommand? ResolveCommand(CommandBody commandBody);
}