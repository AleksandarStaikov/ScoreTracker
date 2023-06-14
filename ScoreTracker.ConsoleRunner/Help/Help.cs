using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;

namespace ScoreTracker.ConsoleRunner.Help;

public class Help : ICommand
{
    private readonly ICommunicationHub _communication;

    public Help(ICommunicationHub communication)
    {
        _communication = communication;
    }

    public void Execute(string[] commandSegments)
    {
        _communication.PublichMessage("This is the help message :P, go read the code and figure it out on yourself, I ain`t helping you :)");
    }
}
