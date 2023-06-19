using ScoreTracker.ConsoleRunner.Common.Communication;

namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommand
{
    public void Execute(CommandBody commandSegments);
}