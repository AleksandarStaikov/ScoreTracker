namespace ScoreTracker.ConsoleRunner.Runner.Interfaces;

public interface IRunner
{
    IRunner SetupRunner();
    void Run(string[] initialCommand);
}