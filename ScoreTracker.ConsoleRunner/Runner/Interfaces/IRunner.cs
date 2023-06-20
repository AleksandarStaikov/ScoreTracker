namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface IRunner
{
    IRunner SetupRunner();
    Task Run(string[] initialCommand);
}