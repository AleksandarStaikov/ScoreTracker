using ScoreTracker.ConsoleRunner.Common;

namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommand
{
    public void Execute(CommandBody commandSegments);
}