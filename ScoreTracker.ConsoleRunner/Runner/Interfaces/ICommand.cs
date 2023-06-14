namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface ICommand
{
    public void Execute(string[] commandSegments);
}