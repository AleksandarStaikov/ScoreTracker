using ScoreTracker.ConsoleRunner.Common.Communication;

namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommand
{
    Task Execute(CommandBody commandSegments);
}